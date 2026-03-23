using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.skud;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;

namespace ApiAll.Controllers.turniket
{
    [ApiExplorerSettings(GroupName = "v11")]
    [Route("api/[controller]")]
    [ApiController]
    public class SkudDoorCheckinoutsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudDoorCheckinoutsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudDoorCheckinouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudDoorCheckinout>>> GetSkudDoorCheckinout()
        {
            return await _context.SkudDoorCheckinout.ToListAsync();
        }

        [HttpGet("getLast24Hours")]
        public async Task<ActionResult<TexPaginationModel>> getLast24Hours([FromQuery] int page, [FromQuery] int size)
        {
            var since = DateTime.Now.AddHours(-24);
            var sinceDate = since.Date;
            var sinceTime = since.TimeOfDay;

            var baseQuery = _context.SkudDoorCheckinout.Where(p =>
                p.sana > sinceDate || (p.sana == sinceDate && p.checktime >= sinceTime));

            var dataQuery = from log in baseQuery
                            join user in _context.SkudMyUserinfo on log.userid equals user.userid into userJoin
                            from user in userJoin.DefaultIfEmpty()
                            orderby log.sana descending, log.checktime descending
                            select new
                            {
                                log.code,
                                log.userid,
                                user_name = user != null ? user.ism : "",
                                user_image = user != null ? user.image_url : "",
                                log.door_name,
                                check_date = log.sana,
                                check_time = log.checktime,
                                log.checktype,
                                log.begona
                            };

            TexPaginationModel paginationModel = new TexPaginationModel();
            paginationModel.items_count = await dataQuery.CountAsync();
            var items = await dataQuery.Skip(page * size).Take(size).ToListAsync();
            paginationModel.items_list = JArray.FromObject(items);
            paginationModel.current_item_count = items.Count;
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getFaceLogsByDate")]
        public async Task<ActionResult<JArray>> getFaceLogsByDate([FromQuery] string start_date, [FromQuery] string end_date)
        {
            if (!DateTime.TryParse(start_date, out var startDate) || !DateTime.TryParse(end_date, out var endDate))
            {
                return BadRequest("Invalid date");
            }

            var startTime = startDate.Date;
            var endTime = endDate.Date.AddDays(1).AddSeconds(-1);
            Console.WriteLine($"[FACE-API] getFaceLogsByDate start={startTime:yyyy-MM-dd HH:mm:ss} end={endTime:yyyy-MM-dd HH:mm:ss}");

            var doors = await _context.SkudDoors.ToListAsync();
            Console.WriteLine($"[FACE-API] doors={doors.Count}");
            var results = new List<FaceLogItem>();
            var newResults = new List<FaceLogItem>();
            var toInsert = new List<SkudMyCheckinout>();
            var insertKeys = new HashSet<string>();
            var minorCandidates = new[] { 75, 76, 164, 80, 90, 104, 116, 117, 130, 132, 142, 151, 152, 155, 193, 208, 1, 2, 3, 4, 5 };

            foreach (var door in doors)
            {
                if (string.IsNullOrWhiteSpace(door.device) || string.IsNullOrWhiteSpace(door.login))
                {
                    Console.WriteLine($"[FACE-API] Door {door.id} skip: device/login yo'q");
                    continue;
                }

                var baseHost = door.device.Trim();
                var baseUrl = baseHost.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                              baseHost.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
                    ? baseHost
                    : "http://" + baseHost;
                var fullUrl = baseUrl + "/ISAPI/AccessControl/AcsEvent?format=json";
                Console.WriteLine($"[FACE-API] Door {door.id} url={fullUrl}");

                var credCache = new CredentialCache();
                credCache.Add(new Uri(fullUrl), "Digest", new NetworkCredential(door.login, door.password));

                using (var httpClient = new HttpClient(new HttpClientHandler
                {
                    Credentials = credCache,
                    PreAuthenticate = true,
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                }))
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(15);

                    int? selectedMinor = null;
                    foreach (var minor in minorCandidates)
                    {
                        var probe = await FetchEventsPage(httpClient, fullUrl, door, startTime, endTime, 0, 30, minor);
                        if (probe.events != null && probe.events.Count > 0)
                        {
                            selectedMinor = minor;
                            break;
                        }

                        var totalMatches = (int?)probe.acsEvent?["totalMatches"] ?? 0;
                        if (totalMatches > 0)
                        {
                            selectedMinor = minor;
                            break;
                        }
                    }

                    if (!selectedMinor.HasValue)
                    {
                        Console.WriteLine($"[FACE-API] Door {door.id} minor topilmadi");
                        continue;
                    }
                    Console.WriteLine($"[FACE-API] Door {door.id} selected minor={selectedMinor.Value}");

                    int searchPosition = 0;
                    const int maxResults = 30;
                    while (true)
                    {
                        var page = await FetchEventsPage(httpClient, fullUrl, door, startTime, endTime, searchPosition, maxResults, selectedMinor.Value);
                        if (page.acsEvent == null)
                        {
                            Console.WriteLine($"[FACE-API] Door {door.id} acsEvent null");
                            break;
                        }

                        var events = page.events;
                        if (events == null || events.Count == 0)
                        {
                            var totalMatches = (int?)page.acsEvent["totalMatches"] ?? 0;
                            var numMatchesLocal = (int?)page.acsEvent["numOfMatches"] ?? 0;
                            Console.WriteLine($"[FACE-API] Door {door.id} events=0 total={totalMatches} num={numMatchesLocal}");
                            break;
                        }

                        Console.WriteLine($"[FACE-API] Door {door.id} events count={events.Count}");
                        foreach (var item in events)
                        {
                            if (!TryBuildFaceLog(item, door, out var log))
                            {
                                continue;
                            }

                            results.Add(log);

                            if (log.entity != null)
                            {
                                var key = $"{log.entity.userid}|{log.entity.sana:yyyy-MM-dd}|{log.entity.checktime}";
                                if (!insertKeys.Contains(key))
                                {
                                    bool exists = await _context.SkudMyCheckinout.AnyAsync(p =>
                                        p.userid == log.entity.userid &&
                                        p.sana == log.entity.sana &&
                                        p.checktime == log.entity.checktime);
                                    if (!exists)
                                    {
                                        insertKeys.Add(key);
                                        toInsert.Add(log.entity);
                                        newResults.Add(log);
                                    }
                                }
                            }
                        }

                        var numMatches = (int?)page.acsEvent["numOfMatches"] ?? 0;
                        if (numMatches < maxResults)
                        {
                            break;
                        }

                        searchPosition += numMatches;
                    }
                }
            }

            if (toInsert.Count > 0)
            {
                _context.SkudMyCheckinout.AddRange(toInsert);
                await _context.SaveChangesAsync();
            }
            Console.WriteLine($"[FACE-API] new logs saved={toInsert.Count}");

            var responseItems = newResults.Select(item => new
            {
                item.userid,
                item.user_name,
                item.door_name,
                item.check_date,
                item.check_time,
                item.checktype,
                item.image_url
            });

            return JArray.FromObject(responseItems);
        }

        // GET: api/SkudDoorCheckinouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudDoorCheckinout>> GetSkudDoorCheckinout(long id)
        {
            var skudDoorCheckinout = await _context.SkudDoorCheckinout.FindAsync(id);

            if (skudDoorCheckinout == null)
            {
                return NotFound();
            }

            return skudDoorCheckinout;
        }

        // PUT: api/SkudDoorCheckinouts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudDoorCheckinout(long id, SkudDoorCheckinout skudDoorCheckinout)
        {
            if (id != skudDoorCheckinout.code)
            {
                return BadRequest();
            }

            _context.Entry(skudDoorCheckinout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudDoorCheckinoutExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SkudDoorCheckinouts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudDoorCheckinout>> PostSkudDoorCheckinout(SkudDoorCheckinout skudDoorCheckinout)
        {
            _context.SkudDoorCheckinout.Update(skudDoorCheckinout);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudDoorCheckinout", new { id = skudDoorCheckinout.code }, skudDoorCheckinout);
        }

        // DELETE: api/SkudDoorCheckinouts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudDoorCheckinout>> DeleteSkudDoorCheckinout(long id)
        {
            var skudDoorCheckinout = await _context.SkudDoorCheckinout.FindAsync(id);
            if (skudDoorCheckinout == null)
            {
                return NotFound();
            }

            _context.SkudDoorCheckinout.Remove(skudDoorCheckinout);
            await _context.SaveChangesAsync();

            return skudDoorCheckinout;
        }

        private bool SkudDoorCheckinoutExists(long id)
        {
            return _context.SkudDoorCheckinout.Any(e => e.code == id);
        }

        private async Task<(JObject acsEvent, JArray events)> FetchEventsPage(
            HttpClient httpClient,
            string fullUrl,
            SkudDoors door,
            DateTime? startTime,
            DateTime? endTime,
            int searchPosition,
            int maxResults,
            int minor)
        {
            string[] timeFormats = {
                "yyyy-MM-ddTHH:mm:ss",
                "yyyy-MM-ddTHH:mm:sszzz",
                "yyyy-MM-dd HH:mm:ss"
            };

            foreach (var fmt in timeFormats)
            {
                try
                {
                    var cond = new JObject(
                        new JProperty("searchID", "1"),
                        new JProperty("searchResultPosition", searchPosition),
                        new JProperty("maxResults", maxResults),
                        new JProperty("major", 5),
                        new JProperty("minor", minor),
                        new JProperty("timeReverseOrder", true)
                    );

                    if (startTime.HasValue && endTime.HasValue)
                    {
                        cond.Add(new JProperty("startTime", FormatDeviceTime(startTime.Value, fmt)));
                        cond.Add(new JProperty("endTime", FormatDeviceTime(endTime.Value, fmt)));
                    }

                    var payload = new JObject(new JProperty("AcsEventCond", cond));
                    var content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
                    var response = await PostWithRetry(httpClient, new Uri(fullUrl), content);
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorBody = await response.Content.ReadAsStringAsync();
                        if (errorBody != null && errorBody.Contains("\"startTime\"", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        Console.WriteLine($"[FACE-API] Door {door.id} status={response.StatusCode} minor={minor} body={errorBody}");
                        return (null, null);
                    }

                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject json;
                    try
                    {
                        json = JObject.Parse(responseString);
                    }
                    catch
                    {
                        return (null, null);
                    }

                    var acsEvent = json["AcsEvent"] as JObject ?? json["AcsEventSearchResult"] as JObject;
                    if (acsEvent == null)
                    {
                        return (null, null);
                    }

                    JArray events = acsEvent["AcsEventInfo"] as JArray;
                    if (events == null)
                    {
                        var infoListToken = acsEvent["InfoList"];
                        if (infoListToken is JArray infoArray)
                        {
                            events = infoArray;
                        }
                        else if (infoListToken is JObject infoObj)
                        {
                            events = infoObj["AcsEventInfo"] as JArray
                                ?? infoObj["Info"] as JArray;
                        }
                    }

                    return (acsEvent, events);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"[FACE-API] Door {door.id} request error minor={minor}: {ex.Message}");
                    return (null, null);
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine($"[FACE-API] Door {door.id} request timeout minor={minor}");
                    return (null, null);
                }
            }

            return (null, null);
        }

        private async Task<HttpResponseMessage> PostWithRetry(HttpClient httpClient, Uri uri, HttpContent content)
        {
            const int maxRetries = 2;
            for (int attempt = 0; attempt <= maxRetries; attempt++)
            {
                try
                {
                    return await httpClient.PostAsync(uri, content);
                }
                catch (HttpRequestException) when (attempt < maxRetries)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(300 * (attempt + 1)));
                }
                catch (TaskCanceledException) when (attempt < maxRetries)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(300 * (attempt + 1)));
                }
            }

            return await httpClient.PostAsync(uri, content);
        }

        private string FormatDeviceTime(DateTime value, string format)
        {
            if (format.Contains("zzz"))
            {
                var dto = new DateTimeOffset(value, TimeZoneInfo.Local.GetUtcOffset(value));
                return dto.ToString(format);
            }
            return value.ToString(format);
        }

        private class FaceLogItem
        {
            public long userid { get; set; }
            public string user_name { get; set; }
            public string door_name { get; set; }
            public string check_date { get; set; }
            public string check_time { get; set; }
            public string checktype { get; set; }
            public string image_url { get; set; }
            public SkudMyCheckinout entity { get; set; }
        }

        private bool TryBuildFaceLog(JToken item, SkudDoors door, out FaceLogItem log)
        {
            log = null;
            var timeToken = item["time"] ?? item["dateTime"] ?? item["accessTime"];
            if (!TryParseEventTime(timeToken, out var eventTime))
            {
                return false;
            }

            var employeeStr = (string)item["employeeNoString"] ?? (string)item["employeeNo"] ?? "";
            var trimmed = employeeStr.TrimStart('0');
            if (string.IsNullOrWhiteSpace(trimmed))
            {
                trimmed = "0";
            }
            long.TryParse(trimmed, out var employeeId);
            var name = (string)item["name"] ?? "";
            var label = (string)item["label"] ?? "";
            var pictureUrl = (string)item["pictureURL"] ?? "";
            var attendance = (string)item["attendanceStatus"] ?? "";

            var doorName = !string.IsNullOrWhiteSpace(door.acc_name) ? door.acc_name :
                (!string.IsNullOrWhiteSpace(door.dbname) ? door.dbname : label);
            var checktype = !string.IsNullOrWhiteSpace(door.inout) ? door.inout : MapCheckType(attendance);

            log = new FaceLogItem
            {
                userid = employeeId,
                user_name = name,
                door_name = doorName,
                check_date = eventTime.ToString("yyyy-MM-dd"),
                check_time = eventTime.ToString("HH:mm:ss"),
                checktype = checktype,
                image_url = pictureUrl,
                entity = new SkudMyCheckinout
                {
                    userid = employeeId,
                    sana = eventTime.Date,
                    checktime = eventTime.TimeOfDay,
                    checktype = checktype,
                    door_name = doorName
                }
            };
            return true;
        }

        private string MapCheckType(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }

            var v = value.ToLowerInvariant();
            if (v.Contains("checkin") || v.Contains("in"))
            {
                return "K";
            }
            if (v.Contains("checkout") || v.Contains("out"))
            {
                return "C";
            }

            return value;
        }

        private bool TryParseEventTime(JToken timeToken, out DateTime eventTime)
        {
            eventTime = default;
            if (timeToken == null)
            {
                return false;
            }

            if (timeToken.Type == JTokenType.Date)
            {
                eventTime = timeToken.ToObject<DateTime>();
                return true;
            }

            var timeStr = timeToken.ToString();
            if (string.IsNullOrWhiteSpace(timeStr))
            {
                return false;
            }

            if (DateTimeOffset.TryParse(
                    timeStr,
                    CultureInfo.CurrentCulture,
                    DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal,
                    out var dtoCurrent))
            {
                eventTime = dtoCurrent.LocalDateTime;
                return true;
            }

            if (DateTimeOffset.TryParse(
                    timeStr,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal,
                    out var dtoInvariant))
            {
                eventTime = dtoInvariant.LocalDateTime;
                return true;
            }

            string[] formats = {
                "yyyy-MM-ddTHH:mm:ssK",
                "yyyy-MM-ddTHH:mm:sszzz",
                "yyyy-MM-ddTHH:mm:ss",
                "MM/dd/yyyy HH:mm:ss",
                "dd.MM.yyyy HH:mm:ss"
            };

            if (DateTime.TryParseExact(timeStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var parsed))
            {
                eventTime = parsed;
                return true;
            }

            return DateTime.TryParse(timeStr, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out eventTime);
        }
    }
}
