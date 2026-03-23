using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiAll.Contex;
using ApiAll.Model.skud;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;

public class SkudDoorEventSnapshotService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public SkudDoorEventSnapshotService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("▶️ SkudDoorEventSnapshotService start (polling)");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                    var doors = await context.SkudDoors.ToListAsync(stoppingToken);
                    if (doors == null || doors.Count == 0)
                    {
                        Console.WriteLine("⚠️ SkudDoors topilmadi");
                        await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
                        continue;
                    }

                    var lastTime = await GetLastMyCheckinoutTime(context, stoppingToken);
                    if (lastTime.HasValue)
                    {
                        Console.WriteLine($"🕒 Last my_checkinout: {lastTime:yyyy-MM-dd HH:mm:ss}");
                    }
                    else
                    {
                        Console.WriteLine("🕒 Last my_checkinout: none");
                    }
                    var startTime = lastTime.HasValue ? lastTime.Value.AddMinutes(-5) : DateTime.Now.AddHours(-24);
                    var endTime = DateTime.Now;

                    foreach (var door in doors)
                    {
                        if (stoppingToken.IsCancellationRequested)
                        {
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(door.device) || string.IsNullOrWhiteSpace(door.login))
                        {
                            Console.WriteLine($"⚠️ Door skip: id={door.id} ip/login yo'q");
                            continue;
                        }

                        Console.WriteLine($"🔎 Door {door.id} ({door.device}) loglar olinmoqda...");
                        await FetchAndSaveDoorEvents(context, door, startTime, endTime, lastTime, stoppingToken);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Skud door snapshot xatolik: {ex.Message}");
            }

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
            catch (TaskCanceledException)
            {
                break;
            }
        }
    }

    private async Task<DateTime?> GetLastMyCheckinoutTime(ApplicationContext context, CancellationToken token)
    {
        var last = await context.SkudMyCheckinout
            .OrderByDescending(c => c.sana)
            .ThenByDescending(c => c.checktime)
            .Select(c => new { c.sana, c.checktime })
            .FirstOrDefaultAsync(token);
        if (last == null)
        {
            return null;
        }
        return last.sana.Add(last.checktime);
    }

    private async Task FetchAndSaveDoorEvents(
        ApplicationContext context,
        SkudDoors door,
        DateTime startTime,
        DateTime endTime,
        DateTime? lastTime,
        CancellationToken token)
    {
        var baseHost = door.device?.Trim() ?? "";
        var baseUrl = baseHost.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                      baseHost.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
            ? baseHost
            : "http://" + baseHost;
        var api = "/ISAPI/AccessControl/AcsEvent?format=json";
        var fullUrl = baseUrl + api;
        var capabilityUrl = baseUrl + "/ISAPI/AccessControl/AcsEvent/capabilities?format=json";

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

            var deviceNow = await GetDeviceTime(httpClient, baseUrl, door, token);
            if (deviceNow.HasValue)
            {
                endTime = deviceNow.Value;
                Console.WriteLine($"⏱️ Door {door.id} device time: {endTime:yyyy-MM-dd HH:mm:ss}");
            }
            if (lastTime.HasValue)
            {
                startTime = lastTime.Value.AddSeconds(1);
            }
            if (startTime >= endTime)
            {
                Console.WriteLine($"ℹ️ Door {door.id} skip (start >= end)");
                return;
            }

            int searchPosition = 0;
            const int maxResults = 30;
            var minorCandidates = new[] { 75, 76, 164, 80, 90, 104, 116, 117, 130, 132, 142, 151, 152, 155, 193, 208, 1, 2, 3, 4, 5 };
            int? selectedMinor = null;

            foreach (var minor in minorCandidates)
            {
                var probe = await FetchEventsPage(httpClient, fullUrl, door, startTime, endTime, 0, maxResults, minor, token, logInfo: false, logErrors: false);
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
                return;
            }

            var toInsert = new List<SkudMyCheckinout>();
            var insertKeys = new HashSet<string>();
            while (!token.IsCancellationRequested)
            {
                var page = await FetchEventsPage(httpClient, fullUrl, door, startTime, endTime, searchPosition, maxResults, selectedMinor.Value, token, logInfo: false, logErrors: true);
                var events = page.events;
                var acsEvent = page.acsEvent;

                if (acsEvent == null)
                {
                    Console.WriteLine($"⚠️ Door {door.id} response format noto'g'ri");
                    break;
                }

                if (events == null || events.Count == 0)
                {
                    break;
                }
                bool loggedSample = false;
                int parsedCount = 0;
                foreach (var item in events)
                {
                    var log = BuildDoorLog(item, door);
                    if (log == null)
                    {
                        if (!loggedSample)
                        {
                            var timeStr = (string)item["time"] ?? (string)item["dateTime"] ?? (string)item["accessTime"];
                            Console.WriteLine($"⚠️ Door {door.id} parse failed: time='{timeStr}', raw={item}");
                            loggedSample = true;
                        }
                        continue;
                    }

                    var logTime = log.sana.Add(log.checktime);
                    if (lastTime.HasValue && logTime <= lastTime.Value)
                    {
                        continue;
                    }

                    var key = $"{log.userid}|{log.sana:yyyy-MM-dd}|{log.checktime}";
                    if (!insertKeys.Contains(key))
                    {
                        bool exists = await context.SkudMyCheckinout.AnyAsync(p =>
                            p.userid == log.userid &&
                            p.sana == log.sana &&
                            p.checktime == log.checktime, token);
                        if (!exists)
                        {
                            insertKeys.Add(key);
                            toInsert.Add(log);
                            Console.WriteLine($"[SKUD-NEW] {log.sana:yyyy-MM-dd} {log.checktime} | user={log.userid} | door={log.door_name} | type={log.checktype}");
                            parsedCount++;
                        }
                    }
                }
                Console.WriteLine($"ℹ️ Door {door.id} new logs={parsedCount}");

                var numMatches = (int?)acsEvent["numOfMatches"] ?? 0;
                if (numMatches < maxResults)
                {
                    break;
                }

                searchPosition += numMatches;
            }

            if (toInsert.Count > 0)
            {
                context.SkudMyCheckinout.AddRange(toInsert);
                await context.SaveChangesAsync(token);
            }
        }
    }

    private SkudMyCheckinout BuildDoorLog(JToken item, SkudDoors door)
    {
        var timeToken = item["time"] ?? item["dateTime"] ?? item["accessTime"];
        if (!TryParseEventTime(timeToken, out var eventTime))
        {
            return null;
        }

        var employeeStr = (string)item["employeeNoString"] ?? (string)item["employeeNo"] ?? "";
        var trimmed = employeeStr.TrimStart('0');
        if (string.IsNullOrWhiteSpace(trimmed))
        {
            trimmed = "0";
        }
        long.TryParse(trimmed, out var employeeNo);

        var inOutType = (string)item["inAndOutType"] ?? (string)item["attendanceStatus"] ?? "";
        var checktype = !string.IsNullOrWhiteSpace(door.inout) ? door.inout : MapCheckType(inOutType);
        var doorName = (string)item["doorName"] ?? door.acc_name ?? door.dbname ?? "";

        return new SkudMyCheckinout
        {
            userid = employeeNo,
            sana = eventTime.Date,
            checktime = eventTime.TimeOfDay,
            checktype = checktype,
            door_name = doorName
        };
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

    private async Task<(JObject acsEvent, JArray events, string responseString)> FetchEventsPage(
        HttpClient httpClient,
        string fullUrl,
        SkudDoors door,
        DateTime? startTime,
        DateTime? endTime,
        int searchPosition,
        int maxResults,
        int minor,
        CancellationToken token,
        bool logInfo = true,
        bool logErrors = true)
    {
        string[] timeFormats = {
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-MM-ddTHH:mm:sszzz",
            "yyyy-MM-dd HH:mm:ss"
        };

        foreach (var fmt in timeFormats)
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
            var response = await httpClient.PostAsync(new Uri(fullUrl), content, token);
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                if (errorBody != null && errorBody.Contains("\"startTime\"", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                var shouldLog = logErrors;
                if (!logErrors && errorBody != null && errorBody.Contains("\"errorMsg\":", StringComparison.OrdinalIgnoreCase))
                {
                    if (errorBody.IndexOf("\"errorMsg\"", StringComparison.OrdinalIgnoreCase) >= 0 &&
                        errorBody.IndexOf("minor", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        shouldLog = false;
                    }
                }
                if (shouldLog)
                {
                    Console.WriteLine($"❌ Door {door.id} status={response.StatusCode} body={errorBody}");
                }
                return (null, null, errorBody);
            }

            var responseString = await response.Content.ReadAsStringAsync();
            JObject json;
            try
            {
                json = JObject.Parse(responseString);
            }
            catch
            {
                return (null, null, responseString);
            }

            var acsEvent = json["AcsEvent"] as JObject ?? json["AcsEventSearchResult"] as JObject;
            if (acsEvent == null)
            {
                return (null, null, responseString);
            }

            var totalMatches = (int?)acsEvent["totalMatches"] ?? 0;
            var numMatches = (int?)acsEvent["numOfMatches"] ?? 0;
            if (logInfo)
            {
                Console.WriteLine($"ℹ️ Door {door.id} minor={minor} total={totalMatches} num={numMatches}");
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
            if (logInfo && (events == null || events.Count == 0))
            {
                LogResponseSnippet(door.id, responseString);
            }
            return (acsEvent, events, responseString);
        }

        return (null, null, null);
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

    private void LogResponseSnippet(long doorId, string responseString)
    {
        if (string.IsNullOrWhiteSpace(responseString))
        {
            return;
        }

        var snippet = responseString.Length > 800
            ? responseString.Substring(0, 800) + "..."
            : responseString;
        Console.WriteLine($"ℹ️ Door {doorId} response: {snippet}");
    }

    private async Task<DateTime?> GetDeviceTime(HttpClient httpClient, string baseUrl, SkudDoors door, CancellationToken token)
    {
        try
        {
            var timeUrl = baseUrl + "/ISAPI/System/time?format=json";
            var response = await httpClient.GetAsync(new Uri(timeUrl), token);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var body = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(body);
            var timeStr = (string)json["Time"]?["localTime"]
                       ?? (string)json["Time"]?["time"]
                       ?? (string)json["localTime"]
                       ?? (string)json["time"];

            if (DateTime.TryParse(timeStr, out var deviceTime))
            {
                return deviceTime;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Door {door.id} time error: {ex.Message}");
        }

        return null;
    }

    private string MapCheckType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "";
        }

        var v = value.ToLowerInvariant();
        if (v.Contains("in") || v.Contains("entry") || v == "1")
        {
            return "K";
        }
        if (v.Contains("out") || v.Contains("exit") || v == "2")
        {
            return "C";
        }

        return "";
    }
}
