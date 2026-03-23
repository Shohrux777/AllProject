using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.skud;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Text;

namespace ApiAll.Controllers.turniket
{
    [ApiExplorerSettings(GroupName = "v11")]
    [Route("api/[controller]")]
    [ApiController]
    public class SkudDoorsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudDoorsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SkudDoors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudDoors>>> GetSkudDoors()
        {
            return await _context.SkudDoors.ToListAsync();
        }

        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudDoors> itemList = await _context.SkudDoors
                .OrderByDescending(p => p.id)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudDoors>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudDoors.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        // GET: api/SkudDoors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudDoors>> GetSkudDoors(long id)
        {
            var skudDoors = await _context.SkudDoors.FindAsync(id);

            if (skudDoors == null)
            {
                return NotFound();
            }

            return skudDoors;
        }

        // PUT: api/SkudDoors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudDoors(long id, SkudDoors skudDoors)
        {
            if (id != skudDoors.id)
            {
                return BadRequest();
            }

            _context.Entry(skudDoors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudDoorsExists(id))
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

        // POST: api/SkudDoors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkudDoors>> PostSkudDoors(SkudDoors skudDoors)
        {
            _context.SkudDoors.Update(skudDoors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkudDoors", new { id = skudDoors.id }, skudDoors);
        }

        // DELETE: api/SkudDoors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkudDoors>> DeleteSkudDoors(long id)
        {
            var skudDoors = await _context.SkudDoors.FindAsync(id);
            if (skudDoors == null)
            {
                return NotFound();
            }

            _context.SkudDoors.Remove(skudDoors);
            await _context.SaveChangesAsync();

            return skudDoors;
        }

        private bool SkudDoorsExists(long id)
        {
            return _context.SkudDoors.Any(e => e.id == id);
        }

        // GET: api/SkudDoors/checkStatus/{id}
        [HttpGet("checkStatus/{id}")]
        public async Task<ActionResult<object>> CheckDeviceStatus(long id)
        {
            var door = await _context.SkudDoors.FindAsync(id);
            if (door == null)
            {
                return NotFound(new { status = false, message = "Door not found" });
            }

            if (string.IsNullOrEmpty(door.device))
            {
                return Ok(new { status = false, message = "Device IP not configured" });
            }

            bool isOnline = await CheckDeviceConnection(door.device);
            
            return Ok(new { 
                id = door.id, 
                device = door.device,
                status = isOnline ? "online" : "offline",
                isOnline = isOnline
            });
        }

        // GET: api/SkudDoors/checkAllStatus
        [HttpGet("checkAllStatus")]
        public async Task<ActionResult<object>> CheckAllDevicesStatus()
        {
            var doors = await _context.SkudDoors.ToListAsync();
            var statusList = new List<object>();

            foreach (var door in doors)
            {
                if (string.IsNullOrEmpty(door.device))
                {
                    statusList.Add(new { 
                        id = door.id, 
                        device = door.device ?? "N/A",
                        status = "unknown",
                        isOnline = false,
                        message = "Device IP not configured"
                    });
                    continue;
                }

                bool isOnline = await CheckDeviceConnection(door.device);
                
                statusList.Add(new { 
                    id = door.id, 
                    device = door.device,
                    status = isOnline ? "online" : "offline",
                    isOnline = isOnline
                });
            }

            return Ok(statusList);
        }

        private async Task<bool> CheckDeviceConnection(string deviceIp)
        {
            try
            {
                // Ping orqali tekshirish
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(deviceIp, 3000); // 3 soniya timeout
                    if (reply.Status == IPStatus.Success)
                    {
                        // Ping muvaffaqiyatli bo'lsa, HTTP request yuborish (Hikvision device uchun)
                        try
                        {
                            using (var httpClient = new HttpClient())
                            {
                                httpClient.Timeout = TimeSpan.FromSeconds(3);
                                // Hikvision device'ning asosiy endpoint'iga so'rov yuborish
                                var response = await httpClient.GetAsync($"http://{deviceIp}");
                                return response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.Unauthorized;
                            }
                        }
                        catch
                        {
                            // HTTP so'rov muvaffaqiyatsiz bo'lsa ham, ping muvaffaqiyatli bo'lgani uchun true qaytaramiz
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // POST: api/SkudDoors/blockAllUsers/{doorId}
        [HttpPost("blockAllUsers/{doorId}")]
        public async Task<ActionResult<object>> BlockAllUsersOnDevice(long doorId)
        {
            try
            {
                var door = await _context.SkudDoors.FindAsync(doorId);
                if (door == null)
                {
                    return NotFound(new { success = false, message = "Door not found" });
                }

                if (string.IsNullOrEmpty(door.device) || string.IsNullOrEmpty(door.login) || string.IsNullOrEmpty(door.password))
                {
                    return BadRequest(new { success = false, message = "Device IP, login yoki password konfiguratsiya qilinmagan" });
                }

                // Agar door.nomer == 1 bo'lsa, unblock qilish, aks holda block qilish
                bool shouldUnblock = door.nomer == 1;

                // Barcha xodimlarni olish
                var allUsers = await _context.SkudMyUserinfo.ToListAsync();
                Console.WriteLine($"\n📋 ========== Barcha xodimlarni {(shouldUnblock ? "blocklist'dan chiqarish" : "blocklist'ga qo'shish")} ==========");
                Console.WriteLine($"Device: {door.device} (ID: {door.id}, Name: {door.acc_name})");
                Console.WriteLine($"Jami xodimlar soni: {allUsers.Count}");
                Console.WriteLine($"Amal: {(shouldUnblock ? "Unblock" : "Block")}");

                int successCount = 0;
                int errorCount = 0;
                var errors = new List<string>();

                // Parallel processing - bir vaqtda faqat 5 ta so'rov (server yukini kamaytirish uchun)
                // Bu boshqa kompyuterlardan ishlashga ta'sir qilmaydi
                var semaphore = new SemaphoreSlim(5, 5);
                var tasks = allUsers.Select(async user =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        // Server yukini kamaytirish uchun har bir so'rov oldidan kichik delay
                        await Task.Delay(50); // 50ms delay - server yukini kamaytiradi
                        
                        // Retry mexanizmi - 3 marta urinib ko'ramiz
                        bool success = false;
                        int maxRetries = 3;
                        for (int retry = 0; retry < maxRetries; retry++)
                        {
                            try
                            {
                                if (shouldUnblock)
                                {
                                    success = await UnblockUserOnDeviceAsync(
                                        door.device,
                                        door.login,
                                        door.password,
                                        user.badgenumber,
                                        user.ism ?? "",
                                        user.familiya ?? ""
                                    );
                                }
                                else
                                {
                                    success = await BlockUserOnDeviceAsync(
                                        door.device,
                                        door.login,
                                        door.password,
                                        user.badgenumber,
                                        user.ism ?? "",
                                        user.familiya ?? ""
                                    );
                                }
                                
                                if (success)
                                {
                                    break; // Muvaffaqiyatli bo'lsa, retry'ni to'xtatamiz
                                }
                            }
                            catch (TaskCanceledException)
                            {
                                if (retry < maxRetries - 1)
                                {
                                    await Task.Delay(1000 * (retry + 1)); // Retry orasida kutamiz (1s, 2s, 3s)
                                }
                                else
                                {
                                    throw; // Oxirgi retry ham muvaffaqiyatsiz bo'lsa, exception'ni tashlaymiz
                                }
                            }
                        }

                        if (success)
                        {
                            Interlocked.Increment(ref successCount);
                        }
                        else
                        {
                            Interlocked.Increment(ref errorCount);
                            String errorMsg = $"Xodim {(shouldUnblock ? "blocklist'dan chiqarilmadi" : "blocklist'ga qo'shilmadi")}: ID={user.badgenumber}, Ism={user.ism}";
                            errors.Add(errorMsg);
                        }
                    }
                    catch (Exception ex)
                    {
                        Interlocked.Increment(ref errorCount);
                        String errorMsg = $"Xodim {(shouldUnblock ? "blocklist'dan chiqarishda" : "blocklist'ga qo'shishda")} xatolik: ID={user.badgenumber}, Ism={user.ism}, Xatolik: {ex.Message}";
                        errors.Add(errorMsg);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                await Task.WhenAll(tasks);

                Console.WriteLine($"\n📊 ========== {(shouldUnblock ? "Blocklist'dan chiqarish" : "Blocklist'ga qo'shish")} natijasi ==========");
                Console.WriteLine($"Muvaffaqiyatli: {successCount}");
                Console.WriteLine($"Xatoliklar: {errorCount}");
                Console.WriteLine($"Jami: {allUsers.Count}");

                // Agar bitta yoki undan ko'p xodim muvaffaqiyatli o'zgartirilgan bo'lsa, door.nomer ni yangilash
                if (successCount > 0)
                {
                    if (shouldUnblock)
                    {
                        door.nomer = 0; // Unblock qilingan bo'lsa, nomer = 0
                        Console.WriteLine($"✅ Device ID={door.id} ({door.acc_name ?? door.device}) - nomer = 0 qilib saqlandi (unblock)");
                    }
                    else
                    {
                        door.nomer = 1; // Block qilingan bo'lsa, nomer = 1
                        Console.WriteLine($"✅ Device ID={door.id} ({door.acc_name ?? door.device}) - nomer = 1 qilib saqlandi (block)");
                    }
                    await _context.SaveChangesAsync();
                }

                return Ok(new
                {
                    success = true,
                    totalUsers = allUsers.Count,
                    blockedCount = shouldUnblock ? 0 : successCount,
                    unblockedCount = shouldUnblock ? successCount : 0,
                    failedCount = errorCount,
                    message = shouldUnblock 
                        ? $"Jami {allUsers.Count} xodimdan {successCount} tasi muvaffaqiyatli blocklist'dan chiqarildi, {errorCount} tasida xatolik yuz berdi"
                        : $"Jami {allUsers.Count} xodimdan {successCount} tasi muvaffaqiyatli blocklist'ga qo'shildi, {errorCount} tasida xatolik yuz berdi",
                    errors = errors.Take(10).ToList() // Faqat birinchi 10 ta xatolik
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ BlockAllUsersOnDevice xatolik: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return BadRequest(new { success = false, message = $"Xatolik: {ex.Message}" });
            }
        }

        private async Task<bool> BlockUserOnDeviceAsync(string deviceIp, string user, string password, long employeeNo, string ism, string familiya)
        {
            try
            {
                // EmployeeNo ni 8 xonali formatga o'tkazish (oldiga 0 qo'shib)
                String employeeNoFormatted = employeeNo.ToString("D8"); // Masalan: 145 -> "00000145", 146 -> "00000146"

                String beginDateStr = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                String endDateStr = DateTime.Now.AddYears(2).ToString("yyyy-MM-ddTHH:mm:ss");
                String empName = !string.IsNullOrEmpty(ism) ? ism : "";
                if (!string.IsNullOrEmpty(familiya))
                {
                    empName += " " + familiya;
                }
                empName = empName.Trim();

                var credCache = new CredentialCache();
                var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
                httpClient.Timeout = TimeSpan.FromSeconds(30); // Timeout'ni oshirdik (15 -> 30)

                // Avval user'ni topishga harakat qilamiz
                String searchApi = "/ISAPI/AccessControl/UserInfo/Search?format=json";
                String searchUrl = "http://" + deviceIp + searchApi;
                credCache.Add(new Uri(searchUrl), "Digest", new NetworkCredential(user, password));

                var searchBody = new JObject();
                var searchInfo = new JObject();
                searchInfo["employeeNo"] = employeeNoFormatted;
                searchBody["UserInfoSearchCond"] = searchInfo;

                var searchContent = new StringContent(searchBody.ToString(Newtonsoft.Json.Formatting.None), System.Text.Encoding.UTF8, "application/json");
                var searchResponse = await httpClient.PostAsync(new Uri(searchUrl), searchContent);
                var searchResponseString = await searchResponse.Content.ReadAsStringAsync();

                bool userExists = false;
                if (searchResponse.IsSuccessStatusCode)
                {
                    try
                    {
                        var searchJson = JObject.Parse(searchResponseString);
                        var userInfoSearch = searchJson["UserInfoSearch"] as JObject;
                        if (userInfoSearch != null && userInfoSearch["numOfMatches"] != null)
                        {
                            int numOfMatches = userInfoSearch["numOfMatches"].Value<int>();
                            userExists = numOfMatches > 0;
                        }
                    }
                    catch { }
                }

                // Agar user mavjud bo'lmasa, avval yaratamiz
                if (!userExists)
                {
                    String recordApi = "/ISAPI/AccessControl/UserInfo/Record?format=json";
                    String recordUrl = "http://" + deviceIp + recordApi;
                    credCache.Add(new Uri(recordUrl), "Digest", new NetworkCredential(user, password));

                    var recordUserInfo = new JObject();
                    recordUserInfo["employeeNo"] = employeeNoFormatted;
                    recordUserInfo["name"] = empName;
                    recordUserInfo["userType"] = "normal"; // Avval normal user sifatida yaratamiz

                    var recordValid = new JObject();
                    recordValid["enable"] = true;
                    recordValid["beginTime"] = beginDateStr;
                    recordValid["endTime"] = endDateStr;
                    recordValid["timeType"] = "local";
                    recordUserInfo["Valid"] = recordValid;

                    var recordRoot = new JObject();
                    recordRoot["UserInfo"] = recordUserInfo;

                    var recordContent = new StringContent(recordRoot.ToString(Newtonsoft.Json.Formatting.None), System.Text.Encoding.UTF8, "application/json");
                    var recordResponse = await httpClient.PostAsync(new Uri(recordUrl), recordContent);
                    var recordResponseString = await recordResponse.Content.ReadAsStringAsync();

                    if (recordResponse.IsSuccessStatusCode)
                    {
                        try
                        {
                            var recordJson = JObject.Parse(recordResponseString);
                            int? recordStatusCode = recordJson["statusCode"]?.Value<int>();
                            if (recordStatusCode == 1)
                            {
                                // 1 soniya kutamiz
                                await Task.Delay(1000);
                            }
                        }
                        catch { }
                    }
                }

                // Endi blocklist'ga qo'shamiz
                String modifyApi = "/ISAPI/AccessControl/UserInfo/Modify?format=json";
                String modifyUrl = "http://" + deviceIp + modifyApi;
                credCache.Add(new Uri(modifyUrl), "Digest", new NetworkCredential(user, password));

                var modifyUserInfo = new JObject();
                modifyUserInfo["employeeNo"] = employeeNoFormatted;
                modifyUserInfo["deleteUser"] = false;
                modifyUserInfo["name"] = empName;
                modifyUserInfo["userType"] = "blackList"; // Blocklist'ga qo'shish
                
                var modifyValid = new JObject();
                modifyValid["enable"] = true;
                modifyValid["beginTime"] = beginDateStr;
                modifyValid["endTime"] = endDateStr;
                modifyValid["timeType"] = "local";
                modifyUserInfo["Valid"] = modifyValid;

                var modifyRoot = new JObject();
                modifyRoot["UserInfo"] = modifyUserInfo;

                String modifyJsonBody = modifyRoot.ToString(Newtonsoft.Json.Formatting.None);

                var modifyContent = new StringContent(modifyJsonBody, System.Text.Encoding.UTF8, "application/json");
                var modifyResponse = await httpClient.PutAsync(new Uri(modifyUrl), modifyContent);
                var modifyResponseString = await modifyResponse.Content.ReadAsStringAsync();

                if (modifyResponse.IsSuccessStatusCode)
                {
                    try
                    {
                        var modifyJson = JObject.Parse(modifyResponseString);
                        int? statusCode = modifyJson["statusCode"]?.Value<int>();
                        String statusString = modifyJson["statusString"]?.ToString();

                        if (statusCode == 1 || statusString == "OK")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception parseEx)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ BlockUserOnDeviceAsync xatolik (EmployeeNo={employeeNo}): {ex.Message}");
                return false;
            }
        }

        private async Task<bool> UnblockUserOnDeviceAsync(string deviceIp, string user, string password, long employeeNo, string ism, string familiya)
        {
            try
            {
                // EmployeeNo ni 8 xonali formatga o'tkazish (oldiga 0 qo'shib)
                String employeeNoFormatted = employeeNo.ToString("D8");

                String beginDateStr = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                String endDateStr = DateTime.Now.AddYears(2).ToString("yyyy-MM-ddTHH:mm:ss");
                String empName = !string.IsNullOrEmpty(ism) ? ism : "";
                if (!string.IsNullOrEmpty(familiya))
                {
                    empName += " " + familiya;
                }
                empName = empName.Trim();

                var credCache = new CredentialCache();
                var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                // Avval user'ni topishga harakat qilamiz
                String searchApi = "/ISAPI/AccessControl/UserInfo/Search?format=json";
                String searchUrl = "http://" + deviceIp + searchApi;
                credCache.Add(new Uri(searchUrl), "Digest", new NetworkCredential(user, password));

                var searchBody = new JObject();
                var searchInfo = new JObject();
                searchInfo["employeeNo"] = employeeNoFormatted;
                searchBody["UserInfoSearchCond"] = searchInfo;

                var searchContent = new StringContent(searchBody.ToString(Newtonsoft.Json.Formatting.None), System.Text.Encoding.UTF8, "application/json");
                var searchResponse = await httpClient.PostAsync(new Uri(searchUrl), searchContent);
                var searchResponseString = await searchResponse.Content.ReadAsStringAsync();

                bool userExists = false;
                if (searchResponse.IsSuccessStatusCode)
                {
                    try
                    {
                        var searchJson = JObject.Parse(searchResponseString);
                        var userInfoSearch = searchJson["UserInfoSearch"] as JObject;
                        if (userInfoSearch != null && userInfoSearch["numOfMatches"] != null)
                        {
                            int numOfMatches = userInfoSearch["numOfMatches"].Value<int>();
                            userExists = numOfMatches > 0;
                        }
                    }
                    catch { }
                }

                // Agar user mavjud bo'lmasa, avval yaratamiz
                if (!userExists)
                {
                    String recordApi = "/ISAPI/AccessControl/UserInfo/Record?format=json";
                    String recordUrl = "http://" + deviceIp + recordApi;
                    credCache.Add(new Uri(recordUrl), "Digest", new NetworkCredential(user, password));

                    var recordUserInfo = new JObject();
                    recordUserInfo["employeeNo"] = employeeNoFormatted;
                    recordUserInfo["name"] = empName;
                    recordUserInfo["userType"] = "normal"; // Normal user sifatida yaratamiz

                    var recordValid = new JObject();
                    recordValid["enable"] = true;
                    recordValid["beginTime"] = beginDateStr;
                    recordValid["endTime"] = endDateStr;
                    recordValid["timeType"] = "local";
                    recordUserInfo["Valid"] = recordValid;

                    var recordRoot = new JObject();
                    recordRoot["UserInfo"] = recordUserInfo;

                    var recordContent = new StringContent(recordRoot.ToString(Newtonsoft.Json.Formatting.None), System.Text.Encoding.UTF8, "application/json");
                    var recordResponse = await httpClient.PostAsync(new Uri(recordUrl), recordContent);
                    var recordResponseString = await recordResponse.Content.ReadAsStringAsync();

                    if (recordResponse.IsSuccessStatusCode)
                    {
                        try
                        {
                            var recordJson = JObject.Parse(recordResponseString);
                            int? recordStatusCode = recordJson["statusCode"]?.Value<int>();
                            if (recordStatusCode == 1)
                            {
                                await Task.Delay(1000);
                            }
                        }
                        catch { }
                    }
                }

                // Endi user'ni normal qilib o'zgartirish (blocklist'dan chiqarish)
                String modifyApi = "/ISAPI/AccessControl/UserInfo/Modify?format=json";
                String modifyUrl = "http://" + deviceIp + modifyApi;
                credCache.Add(new Uri(modifyUrl), "Digest", new NetworkCredential(user, password));

                var modifyUserInfo = new JObject();
                modifyUserInfo["employeeNo"] = employeeNoFormatted;
                modifyUserInfo["deleteUser"] = false;
                modifyUserInfo["name"] = empName;
                modifyUserInfo["userType"] = "normal"; // Blocklist'dan chiqarish - normal qilib qo'yamiz
                
                var modifyValid = new JObject();
                modifyValid["enable"] = true;
                modifyValid["beginTime"] = beginDateStr;
                modifyValid["endTime"] = endDateStr;
                modifyValid["timeType"] = "local";
                modifyUserInfo["Valid"] = modifyValid;

                var modifyRoot = new JObject();
                modifyRoot["UserInfo"] = modifyUserInfo;

                String modifyJsonBody = modifyRoot.ToString(Newtonsoft.Json.Formatting.None);

                var modifyContent = new StringContent(modifyJsonBody, System.Text.Encoding.UTF8, "application/json");
                var modifyResponse = await httpClient.PutAsync(new Uri(modifyUrl), modifyContent);
                var modifyResponseString = await modifyResponse.Content.ReadAsStringAsync();

                if (modifyResponse.IsSuccessStatusCode)
                {
                    try
                    {
                        var modifyJson = JObject.Parse(modifyResponseString);
                        int? statusCode = modifyJson["statusCode"]?.Value<int>();
                        String statusString = modifyJson["statusString"]?.ToString();

                        if (statusCode == 1 || statusString == "OK")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception parseEx)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
