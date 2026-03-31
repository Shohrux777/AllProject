using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.skud;
using ApiAll.Model.tegirmon;
using ApiAll.Model.tekistil;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Collections;
using Npgsql;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ApiAll.Controllers.turniket
{
    [ApiExplorerSettings(GroupName = "v11")]
    [Route("api/[controller]")]
    [ApiController]
    public class SkudMyUserinfoesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SkudMyUserinfoesController(ApplicationContext context)
        {
            _context = context;
        }

        public class ReplaceIoRangeRequest
        {
            public long user_id { get; set; }
            public DateTime first_in_datetime { get; set; }
            public DateTime last_out_datetime { get; set; }
            public string door_name { get; set; }
        }

        public class ReplaceIoRangeResponse
        {
            public bool success { get; set; }
            public int deleted_count { get; set; }
            public long user_id { get; set; }
            public DateTime first_in_datetime { get; set; }
            public DateTime last_out_datetime { get; set; }
        }

        // GET: api/SkudMyUserinfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> GetSkudMyUserinfo()
        {
            return await _context.SkudMyUserinfo.Include(p => p.skudOylik).ToListAsync();
        }

        [HttpGet("deleteAllUsers")]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> deleteAllUsers()
        {
            List<SkudMyUserinfo> usersList = await _context.SkudMyUserinfo.ToListAsync();
            if (usersList != null && usersList.Count > 0 ) {
                _context.SkudMyUserinfo.RemoveRange(usersList);
                await _context.SaveChangesAsync();
            }

            return usersList;
        }

        [HttpGet("getUserFromDevice")]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> getUserFromDevice([FromQuery] long doors_id)
        {
            String api = "/ISAPI/AccessControl/UserInfo/Search?format=json";
            String user = "";
            String password = "";
            String host_addr = "";
            List<SkudMyUserinfo> skudMyUserinfos = new List<SkudMyUserinfo>();
            SkudDoors skudDoors = await _context.SkudDoors.FindAsync(doors_id);
            if (skudDoors != null) {
                user = skudDoors.login;
                password = skudDoors.password;
                host_addr = skudDoors.device;
            }

            String full_url = "http://" + host_addr + api;
            try
            {

                var credCache = new CredentialCache();
                credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });

                var stringContent = new StringContent("{\r\n  \"UserInfoSearchCond\":{\r\n    \"searchID\":\"1\",\r\n    \"searchResultPosition\": 0,\r\n    \"maxResults\":6000\r\n  }\r\n}", UnicodeEncoding.UTF8, "application/json");

                var answer = await httpClient.PostAsync(new Uri(full_url), stringContent);
                if (answer.IsSuccessStatusCode)
                {
                    var responseString = await answer.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(responseString);
                    try
                    {
                        JObject users = JObject.Parse(responseString);
                        JObject UserInfoSearch = (JObject)users["UserInfoSearch"];
                        int totalMatches = (int)UserInfoSearch["totalMatches"];
                        int numOfMatches = (int)UserInfoSearch["numOfMatches"];
                        String responseStatusStrg = (String)UserInfoSearch["responseStatusStrg"];
                        JArray UserInfo = (JArray)UserInfoSearch["UserInfo"];
                        foreach (JObject it in UserInfo)
                        {
                            int employeeNo = (int)it["employeeNo"];
                            String name = (String)it["name"];
                            SkudMyUserinfo usr = new SkudMyUserinfo();
                            usr.userid = employeeNo;
                            usr.ism = name;
                            skudMyUserinfos.Add(usr);
                        }

                    }
                    catch (JsonReaderException e)
                    {
                        Console.WriteLine("JSON ERROR: " + e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }


            return skudMyUserinfos;
        }


        [HttpGet("getUserFromDeviceAllUserWithPasswordAndIpAddress")]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> getUserFromDeviceAllUser([FromQuery] String device_password, [FromQuery] String device_login, [FromQuery] String device_ip)
        {
            String api = "/ISAPI/AccessControl/UserInfo/Search?format=json";
            String user = device_login;
            String password = device_password;
            String host_addr = device_ip;
            List<SkudMyUserinfo> skudMyUserinfos = new List<SkudMyUserinfo>();
     

            String full_url = "http://" + host_addr + api;

            bool not_end_status = false;
            int searchPostion = 0;
            while (not_end_status != true)
            {


                try
                {

                    var credCache = new CredentialCache();
                    credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                    var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });

                    var stringContent = new StringContent("{" +
                        "\r\n  \"UserInfoSearchCond\":{" +
                        "\r\n    \"searchID\":\"1\"," +
                        "\r\n    \"searchResultPosition\": " + searchPostion + "," +
                        "\r\n    \"maxResults\":6000\r\n  }" +
                        "\r\n}", UnicodeEncoding.UTF8, "application/json");

                    var answer = await httpClient.PostAsync(new Uri(full_url), stringContent);
                    if (answer.IsSuccessStatusCode)
                    {
                        var responseString = await answer.Content.ReadAsStringAsync();
                        JObject json = JObject.Parse(responseString);
                        try
                        {
                            JObject users = JObject.Parse(responseString);
                            JObject UserInfoSearch = (JObject)users["UserInfoSearch"];
                            int totalMatches = (int)UserInfoSearch["totalMatches"];
                            int numOfMatches = (int)UserInfoSearch["numOfMatches"];
                            String responseStatusStrg = (String)UserInfoSearch["responseStatusStrg"];
                            JArray UserInfo = (JArray)UserInfoSearch["UserInfo"];
                            foreach (JObject it in UserInfo)
                            {
                                int employeeNo = (int)it["employeeNo"];
                                String name = (String)it["name"];
                                SkudMyUserinfo usr = new SkudMyUserinfo();
                                usr.userid = employeeNo;
                                usr.ism = name;
                                skudMyUserinfos.Add(usr);
                            }

                            searchPostion = searchPostion + numOfMatches;

                            //mos oda topilmadi berkitish kerak
                            if (totalMatches == 0)
                            {
                                not_end_status = true;
                            }

                            //OK digan qiymat kesa xam tugatish kerak
                            String resp = responseStatusStrg.ToLower();
                            if (String.Equals(resp, "ok"))
                            {
                                not_end_status = true;
                            }


                        }
                        catch (JsonReaderException e)
                        {
                            Console.WriteLine("JSON ERROR: " + e.Message);
                            not_end_status = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR");
                        not_end_status = true;
                    }

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e.Message);
                    not_end_status = true;
                }
            }

            return skudMyUserinfos;
        }


        [HttpGet("getUserFromDeviceAllUser")]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> getUserFromDeviceAllUser([FromQuery] long doors_id)
        {
            String api = "/ISAPI/AccessControl/UserInfo/Search?format=json";
            String user = "";
            String password = "";
            String host_addr = "";
            List<SkudMyUserinfo> skudMyUserinfos = new List<SkudMyUserinfo>();
            SkudDoors skudDoors = await _context.SkudDoors.FindAsync(doors_id);
            if (skudDoors != null)
            {
                user = skudDoors.login;
                password = skudDoors.password;
                host_addr = skudDoors.device;
            }

            String full_url = "http://" + host_addr + api;

            bool not_end_status = false;
            int searchPostion = 0;
            while (not_end_status != true) { 
            

            try
            {

                var credCache = new CredentialCache();
                credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });

                var stringContent = new StringContent("{" +
                    "\r\n  \"UserInfoSearchCond\":{" +
                    "\r\n    \"searchID\":\"1\"," +
                    "\r\n    \"searchResultPosition\": "+ searchPostion + "," +
                    "\r\n    \"maxResults\":6000\r\n  }" +
                    "\r\n}", UnicodeEncoding.UTF8, "application/json");

                var answer = await httpClient.PostAsync(new Uri(full_url), stringContent);
                if (answer.IsSuccessStatusCode)
                {
                    var responseString = await answer.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(responseString);
                    try
                    {
                        JObject users = JObject.Parse(responseString);
                        JObject UserInfoSearch = (JObject)users["UserInfoSearch"];
                        int totalMatches = (int)UserInfoSearch["totalMatches"];
                        int numOfMatches = (int)UserInfoSearch["numOfMatches"];
                        String responseStatusStrg = (String)UserInfoSearch["responseStatusStrg"];
                        JArray UserInfo = (JArray)UserInfoSearch["UserInfo"];
                        foreach (JObject it in UserInfo)
                        {
                            int employeeNo = (int)it["employeeNo"];
                            String name = (String)it["name"];
                            SkudMyUserinfo usr = new SkudMyUserinfo();
                            usr.userid = employeeNo;
                            usr.ism = name;
                            skudMyUserinfos.Add(usr);
                        }

                            searchPostion = searchPostion + numOfMatches;

                        //mos oda topilmadi berkitish kerak
                        if (totalMatches == 0) { 
                          not_end_status= true;
                        }

                        //OK digan qiymat kesa xam tugatish kerak
                        String resp = responseStatusStrg.ToLower();
                        if (String.Equals(resp,"ok")) {
                            not_end_status = true;
                        }


                    }
                    catch (JsonReaderException e)
                    {
                        Console.WriteLine("JSON ERROR: " + e.Message);
                        not_end_status = true;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    not_end_status = true;
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                not_end_status=true;
            }
            }

            return skudMyUserinfos;
        }


        [HttpGet("getUserFromDeviceWithPaginationNumber")]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> getUserFromDeviceWithPaginationNumber([FromQuery] long doors_id, [FromQuery] String page_number, [FromQuery] int max_result)
        {
            String api = "/ISAPI/AccessControl/UserInfo/Search?format=json";
            String user = "";
            String password = "";
            String host_addr = "";
            List<SkudMyUserinfo> skudMyUserinfos = new List<SkudMyUserinfo>();
            SkudDoors skudDoors = await _context.SkudDoors.FindAsync(doors_id);
            if (skudDoors != null)
            {
                user = skudDoors.login;
                password = skudDoors.password;
                host_addr = skudDoors.device;
            }

            String full_url = "http://" + host_addr + api;
            try
            {

                var credCache = new CredentialCache();
                credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });

                var stringContent = new StringContent("{\r\n  \"UserInfoSearchCond\":{\r\n    \"searchID\":\""+ page_number + "\",\r\n    \"searchResultPosition\": 0,\r\n    \"maxResults\":"+ max_result + "\r\n  }\r\n}", UnicodeEncoding.UTF8, "application/json");

                var answer = await httpClient.PostAsync(new Uri(full_url), stringContent);
                if (answer.IsSuccessStatusCode)
                {
                    var responseString = await answer.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(responseString);
                    try
                    {
                        JObject users = JObject.Parse(responseString);
                        JObject UserInfoSearch = (JObject)users["UserInfoSearch"];
                        int totalMatches = (int)UserInfoSearch["totalMatches"];
                        JArray UserInfo = (JArray)UserInfoSearch["UserInfo"];
                        foreach (JObject it in UserInfo)
                        {
                            int employeeNo = (int)it["employeeNo"];
                            String name = (String)it["name"];
                            SkudMyUserinfo usr = new SkudMyUserinfo();
                            usr.userid = employeeNo;
                            usr.ism = name;
                            skudMyUserinfos.Add(usr);
                        }

                    }
                    catch (JsonReaderException e)
                    {
                        Console.WriteLine("JSON ERROR: " + e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }


            return skudMyUserinfos;
        }


        [HttpGet("getPagination")]
        public async Task<ActionResult<TexPaginationModel>> getPagination([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationSearchByIsmOrPassportSerailNumberOrMobilePhoneNumberFaceUser")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationSearchByIsmOrPassportSerailNumberOrMobilePhoneNumberFaceUser([FromQuery] int page, [FromQuery] int size,[FromQuery] String fio_or_serial_number)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> categoryList = new List<SkudMyUserinfo>();
            
            // fio_or_serial_number null yoki bo'sh bo'lishini tekshirish
            String searchTerm = fio_or_serial_number?.Trim() ?? "";
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                String searchTermLower = searchTerm.ToLower();
                categoryList = await _context.SkudMyUserinfo
                // .Include(p => p.group)
                .Where(p => (p.ism != null && p.ism.ToLower().Contains(searchTermLower))
                || (p.passport != null && p.passport.ToLower().Contains(searchTermLower))
                 || (p.phone_number != null && p.phone_number.ToLower().Contains(searchTermLower))
                 || (p.born_date != null && p.born_date.ToLower().Contains(searchTermLower)))
                .Skip(page * size).Take(size).OrderByDescending(p => p.userid).ToListAsync();
            }
            else {
                categoryList = await _context.SkudMyUserinfo
                .Skip(page * size).Take(size).OrderByDescending(p => p.userid).ToListAsync();
            }


            if (categoryList == null)
            {
                categoryList = new List<SkudMyUserinfo>();
            }
            paginationModel.items_list = JArray.FromObject(categoryList);
            paginationModel.items_count = categoryList.Count();
            paginationModel.current_item_count = categoryList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationIshlaganVohtiYangisi")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationIshlaganVohtiYangisi([FromQuery] int page,
            [FromQuery] int size, [FromQuery] DateTime need_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }

            foreach (SkudMyUserinfo it in itemList) {
                if (it != null) {
                    //osha kundagi kirdi chiqdilardi olndi
                List<SkudMyCheckinout> checkinouts  = await _context.SkudMyCheckinout
                        .Where(p => p.sana.Date == need_date.Date && p.userid == it.userid && (p.checktype != "K" && p.checktype != "C"))
                .OrderBy(p => p.checktime)
                .Skip(size * page).Take(size)
                .ToListAsync();
                    String ishlagan_vaqti = "0";
                    String ishlagan_vaqti_formatted = "0";
                    //agar natija bolsa chiqaradi bolmasa 0 yozib qoyamiz
                    if (checkinouts != null && checkinouts.Count()> 0) {
                        int counter = 0;
                        double secunds = 0;
                        bool _last_out = false;
                        bool _last_in = false;
                        TimeSpan _last_in_tm = TimeSpan.FromSeconds(0);
                        TimeSpan _last_out_tm = TimeSpan.FromSeconds(0);
                        foreach (SkudMyCheckinout itm_1 in checkinouts) {
                            //KECHASI ISHLADI BUNDA
                            if (counter == 0 && itm_1.checktype == "O")
                            {
                                secunds = secunds + itm_1.checktime.TotalSeconds;
                                _last_out = true;
                                counter++;
                                continue;
                            }
                            //KIRISH
                            if (itm_1.checktype == "I" ) {
                                
                                    //bu eshigdi tagida turibdi qayta qayta olyapti shunchun torilab qoydik
                                    _last_in_tm = itm_1.checktime;
                                    _last_in = true;
                               
                            }
                            //CHIQISH
                            if (itm_1.checktype == "O" ) {
                                if (_last_in)
                                {
                                    secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds);
                                    _last_in = false;
                                }
                                else {
                                    counter++;
                                    continue;
                                }
                            
                            }
                            


                            counter++;
                            }

                        if (_last_in) {
                            secunds = secunds + (86400 - _last_in_tm.TotalSeconds);
                        }

                        ishlagan_vaqti = secunds.ToString();
                        ishlagan_vaqti_formatted = TimeSpan.FromSeconds(secunds).ToString("hh\\:mm\\:ss");

                    }
                    it.ishlagan_vaqti_yangi = ishlagan_vaqti;
                    it.ishlagan_vaqti_yangi_time_format = ishlagan_vaqti_formatted;




                }
            
            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationVaqtBoyichaVaSanaBoyichaSartirofka")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationVaqtBoyichaVaSanaBoyichaSartirofka([FromQuery] int page,
            [FromQuery] int size, [FromQuery] String cur_date_str, [FromQuery] TimeSpan taqqoslash_vaqti)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }
            foreach (SkudMyUserinfo usr in itemList) {
                List<SkudKechQolganTemp> skud_kech_qoganlar = await _context.
                    SkudKechQolganTemp.FromSqlRaw("SELECT " +
                    "\r\nmc.checktime as emp_checktime" +
                    "\r\nFROM my_checkinout mc " +
                    "\r\nLEFT JOIN my_userinfo mu ON mu.userid = mc.userid" +
                    "\r\nWHERE mc.sana = '"+ cur_date_str + "' " +
                    "\r\nAND mc.checktype ='I' " +
                    "\r\nAND mc.userid="+usr.userid+"" +
                    "\r\nORDER BY emp_checktime ASC LIMIT 1 " +
                    "\r\n").ToListAsync();
                if (skud_kech_qoganlar != null && skud_kech_qoganlar.Count() > 0) {
                    usr.emp_checktime = skud_kech_qoganlar.First().emp_checktime;
                    if (usr.emp_checktime != null) {
                        int retVal = 0;
                        retVal = TimeSpan.Compare((TimeSpan)usr.emp_checktime, taqqoslash_vaqti);
                        usr.taqqoshlash_status = retVal;

                    }
                }

            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }



        [HttpGet("getPaginationGrEqualZero")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationGrEqualZero([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .Where(p =>p.gr == 0)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.Where(p => p.gr == 0).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationGrEqualOne")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationGrEqualOne([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .Where(p => p.gr == 1)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.Where(p => p.gr == 1).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationGrEqualTwo")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationGrEqualTwo([FromQuery] int page, [FromQuery] int size)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .Where(p => p.gr == 2)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.Where(p => p.gr == 2).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getPaginationByDepartmentId")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationByDepartmentId([FromQuery] int page,
            [FromQuery] int size, [FromQuery] long dep_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .Where(p => p.departid == dep_id)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.Where(p => p.departid == dep_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getReportWithoutSmenaPaginationByDepartmentId")]
        public async Task<ActionResult<TexPaginationModel>> getReportWithoutSmenaPaginationByDepartmentId([FromQuery] int page,
    [FromQuery] int size, [FromQuery] long dep_id, [FromQuery] DateTime need_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .Where(p => p.departid == dep_id)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }

            foreach (SkudMyUserinfo it in itemList) {
                String date_only = need_date.Date.ToString("yyyy-MM-dd");
                it.kun = date_only;
                List<SkudVaqtTemp> result = await _context.SkudVaqtTemp.FromSqlRaw("select TO_CHAR(working_time_without_gr,'HH24:MI:SS') as vaqt from working_time_without_gr("+it.userid+",'"+date_only+"')").ToListAsync();
                if (result != null && result.Count > 0) {
                    it.vaqt = result[0].vaqt;
                }

            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.Where(p => p.departid == dep_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getReportKemaganlarAllUsers")]
        public async Task<ActionResult<TexPaginationModel>> getReportKemaganlarAllUsers([FromQuery] int page,
            [FromQuery] int size, [FromQuery] DateTime need_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            String date_only = need_date.Date.ToString("yyyy-MM-dd");
            List<SkudKemaganUserTemp> itemList = await _context.SkudKemaganUserTemp
                .FromSqlRaw("  SELECT  mu.userid as userid,mu.ism as ism ,mu.familiya as dept\r\n " +
                " FROM my_userinfo mu\r\n  WHERE mu.userid NOT IN" +
                " (SELECT userid FROM my_checkinout WHERE sana = '"+ date_only + "')\r\n " +
                " GROUP BY mu.userid,mu.ism,mu.familiya\r\n  ORDER BY mu.ism")
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudKemaganUserTemp>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = itemList.Count();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getReportKelganlarAllUsers")]
        public async Task<ActionResult<TexPaginationModel>> getReportKelganlarAllUsers([FromQuery] int page,
     [FromQuery] int size, [FromQuery] DateTime need_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            String date_only = need_date.Date.ToString("yyyy-MM-dd");
            List<SkudKemaganUserTemp> itemList = await _context.SkudKemaganUserTemp
                .FromSqlRaw("  SELECT  mu.userid as userid,mu.ism as ism ,mu.familiya as dept\r\n " +
                " FROM my_userinfo mu\r\n  WHERE mu.userid  IN" +
                " (SELECT userid FROM my_checkinout WHERE sana = '" + date_only + "')\r\n " +
                " GROUP BY mu.userid,mu.ism,mu.familiya\r\n  ORDER BY mu.ism")
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudKemaganUserTemp>();
            }

            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = itemList.Count();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getReportWithoutSmenaPaginationOneUser")]
        public async Task<ActionResult<TexPaginationModel>> getReportWithoutSmenaPaginationOneUser([FromQuery] int page,
[FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long user_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = new List<SkudMyUserinfo>();
            SkudMyUserinfo myUserinfo = await _context.SkudMyUserinfo.FindAsync(user_id);

            if (myUserinfo == null)
            {
               return NotFound();
            }




            for (var dt = begin_date; dt <= end_date; dt = dt.AddDays(1))
            {
                SkudMyUserinfo it = new SkudMyUserinfo();
                it.familiya = myUserinfo.familiya;
                it.userid = user_id;
                it.ism = myUserinfo.ism;
                String date_only = dt.Date.ToString("yyyy-MM-dd");
                it.kun = date_only;

                if (it != null)
                {
                    //osha kundagi kirdi chiqdilardi olndi
                    List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                            .Where(p => p.sana.Date == dt.Date && p.userid == it.userid && (p.checktype != "K" && p.checktype != "C"))
                    .OrderBy(p => p.checktime)
                    .Skip(size * page).Take(size)
                    .ToListAsync();
                    String ishlagan_vaqti = "0";
                    String ishlagan_vaqti_formatted = "0";
                    //agar natija bolsa chiqaradi bolmasa 0 yozib qoyamiz
                    if (checkinouts != null && checkinouts.Count() > 0)
                    {
                        int counter = 0;
                        double secunds = 0;
                        bool _last_out = false;
                        bool _last_in = false;
                        TimeSpan _last_in_tm = TimeSpan.FromSeconds(0);
                        TimeSpan _last_out_tm = TimeSpan.FromSeconds(0);
                        foreach (SkudMyCheckinout itm_1 in checkinouts)
                        {
                            //KECHASI ISHLADI BUNDA
                            if (counter == 0 && itm_1.checktype == "O")
                            {
                                secunds = secunds + itm_1.checktime.TotalSeconds;
                                _last_out = true;
                                counter++;
                                continue;
                            }
                            //KIRISH
                            if (itm_1.checktype == "I")
                            {

                                //bu eshigdi tagida turibdi qayta qayta olyapti shunchun torilab qoydik
                                _last_in_tm = itm_1.checktime;
                                _last_in = true;

                            }
                            //CHIQISH
                            if (itm_1.checktype == "O")
                            {
                                if (_last_in)
                                {
                                    secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds);
                                    _last_in = false;
                                }
                                else
                                {
                                    counter++;
                                    continue;
                                }

                            }



                            counter++;
                        }

                        if (_last_in)
                        {
                            secunds = secunds + (86400 - _last_in_tm.TotalSeconds);
                        }

                        ishlagan_vaqti = secunds.ToString();
                        ishlagan_vaqti_formatted = TimeSpan.FromSeconds(secunds).ToString("hh\\:mm\\:ss");

                    }
                    it.ishlagan_vaqti_yangi = ishlagan_vaqti;
                    it.ishlagan_vaqti_yangi_time_format = ishlagan_vaqti_formatted;
                    it.vaqt = ishlagan_vaqti_formatted;




                }

                /*
                List<SkudVaqtTemp> result = await _context.SkudVaqtTemp.FromSqlRaw("select TO_CHAR(working_time_without_gr,'HH24:MI:SS') as vaqt from working_time_without_gr(" + it.userid + ",'" + date_only + "')").ToListAsync();
                if (result != null && result.Count > 0)
                {
                    it.vaqt = result[0].vaqt;
                }

                */


                itemList.Add(it);

            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }



        [HttpGet("getReportWithoutSmenaPaginationAllUsers")]
        public async Task<ActionResult<TexPaginationModel>> getReportWithoutSmenaPaginationAllUsers([FromQuery] int page,
[FromQuery] int size, [FromQuery] DateTime need_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }

            foreach (SkudMyUserinfo it in itemList)
            {
                String date_only = need_date.Date.ToString("yyyy-MM-dd");
                it.kun = date_only;
                List<SkudVaqtTemp> result = await _context.SkudVaqtTemp.FromSqlRaw("select TO_CHAR(working_time_without_gr,'HH24:MI:SS') as vaqt from working_time_without_gr(" + it.userid + ",'" + date_only + "')").ToListAsync();
                if (result != null && result.Count > 0)
                {
                    it.vaqt = result[0].vaqt;
                }

            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // GET: api/SkudMyUserinfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkudMyUserinfo>> GetSkudMyUserinfo(long id)
        {
            var skudMyUserinfo = await _context.SkudMyUserinfo.FindAsync(id);

            if (skudMyUserinfo == null)
            {
                return NotFound();
            }

            return skudMyUserinfo;
        }


        [HttpGet("deleteOneUserFromOneDevice")]
        public async Task<ActionResult<SkudDoors>> deleteOneUserFromOneDevice([FromQuery] long user_id,[FromQuery] long door_id)
        {

            SkudDoors skudDoors = await _context.SkudDoors.FindAsync(door_id);
            if (skudDoors != null)
            {
                String api = "/ISAPI/AccessControl/UserInfoDetail/Delete?format=json";
                String user = "";
                String password = "";
                String host_addr = "";

                if (skudDoors != null)
                {
                    user = skudDoors.login;
                    password = skudDoors.password;
                    host_addr = skudDoors.device;
                }

                String full_url = "http://" + host_addr + api;
                try
                {

                    var credCache = new CredentialCache();
                    credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                    var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
                    var stringContent = new StringContent("{\r\n  \"UserInfoDetail\":{\r\n    \"mode\":\"byEmployeeNo\",\r\n    \"EmployeeNoList\":[{\r\n      \"employeeNo\":\"" + user_id+"\"\r\n    }]\r\n  }\r\n}", UnicodeEncoding.UTF8, "application/json");
                    var answer = await httpClient.PutAsync(new Uri(full_url), stringContent);

                    var responseString = await answer.Content.ReadAsStringAsync();
                    Console.WriteLine(responseString);


                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            return skudDoors;
        }


        [HttpGet("deleteAllUsersFromOneDevice")]
        public async Task<ActionResult<SkudDoors>> deleteUsersFromOneDevice([FromQuery] long  door_id)
        {
           
            SkudDoors skudDoors = await _context.SkudDoors.FindAsync(door_id);
            if (skudDoors != null )
            {
                    String api = "/ISAPI/AccessControl/UserInfoDetail/Delete?format=json";
                    String user = "";
                    String password = "";
                    String host_addr = "";

                    if (skudDoors != null)
                    {
                        user = skudDoors.login;
                        password = skudDoors.password;
                        host_addr = skudDoors.device;
                    }

                    String full_url = "http://" + host_addr + api;
                    try
                    {

                        var credCache = new CredentialCache();
                        credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                        var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });         
                        var stringContent = new StringContent("{\r\n  \"UserInfoDetail\":{\r\n    \"mode\":\"all\",\r\n    \"EmployeeNoList\":[{\r\n      \"employeeNo\":\"0\"\r\n    }]\r\n  }\r\n}", UnicodeEncoding.UTF8, "application/json");
                        var answer = await httpClient.PutAsync(new Uri(full_url), stringContent);

                        var responseString = await answer.Content.ReadAsStringAsync();
                        Console.WriteLine(responseString);


                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }
            }


            return skudDoors;
        }

        [HttpGet("getUnlockUserForAllDoors")]
        public async Task<ActionResult<SkudMyUserinfo>> getUnlockUserForAllDoors([FromQuery] long user_id)
        {
            var skudMyUserinfo = await _context.SkudMyUserinfo.FindAsync(user_id);
            List<SkudDoors> skudDoorsList = await _context.SkudDoors.ToListAsync();


            if (skudMyUserinfo == null)
            {
                return NotFound();
            }

            if (skudDoorsList != null && skudDoorsList.Count > 0)
            {

                foreach (SkudDoors skudDoors in skudDoorsList)
                {

                    String api = "/ISAPI/AccessControl/UserInfo/Modify?format=json";
                    String user = "";
                    String password = "";
                    String host_addr = "";
                    long emp_nomer = (long)skudMyUserinfo.userid;
                    String emp_name = skudMyUserinfo.ism;




                    if (skudDoors != null)
                    {
                        user = skudDoors.login;
                        password = skudDoors.password;
                        host_addr = skudDoors.device;
                    }

                    String full_url = "http://" + host_addr + api;
                    try
                    {

                        var credCache = new CredentialCache();
                        credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                        var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
                        String begin_date_str = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                        String end_date_str = DateTime.Now.AddYears(2).ToString("yyyy-MM-ddTHH:mm:ss");
                        var stringContent = new StringContent("{ \r\n  \"UserInfo\":{ \r\n    \"employeeNo\":\"" + emp_nomer + "\", \r\n    \"deleteUser\":false , \r\n    \"name\":\"" + emp_name + "\", \r\n    \"userType\":\"normal\", \r\n    \"Valid\":{ \r\n       \"enable\":true, \r\n       \"beginTime\":\"" + begin_date_str + "\", \r\n       \"endTime\":\"" + end_date_str + "\", \r\n       \"timeType\":\"local\" \r\n    }, \r\n    \"checkUser\":true , \r\n    \"addUser\": false \r\n    } \r\n   \r\n}", UnicodeEncoding.UTF8, "application/json");
                        var answer = await httpClient.PutAsync(new Uri(full_url), stringContent);

                        var responseString = await answer.Content.ReadAsStringAsync();
                        Console.WriteLine(responseString);


                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }


                }


            }

            skudMyUserinfo.gr = 0;
            _context.SkudMyUserinfo.Update(skudMyUserinfo);
            await _context.SaveChangesAsync();




            return skudMyUserinfo;
        }

        [HttpGet("addUserToOneDoor")]
        public async Task<ActionResult<IEnumerable<SkudDoors>>> addUserToOneDoor([FromQuery] long user_id, [FromQuery] String fio, [FromQuery] long door_id)
        {

            List<SkudDoors> skudDoorsList = await _context.SkudDoors.Where(p => p.id == door_id).ToListAsync();
            if (skudDoorsList != null && skudDoorsList.Count > 0)
            {

                foreach (SkudDoors skudDoors in skudDoorsList)
                {

                    String api = "/ISAPI/AccessControl/UserInfo/SetUp?format=json";
                    String user = "";
                    String password = "";
                    String host_addr = "";
                    long emp_nomer = (long)user_id;
                    String emp_name = fio;




                    if (skudDoors != null)
                    {
                        user = skudDoors.login;
                        password = skudDoors.password;
                        host_addr = skudDoors.device;
                    }

                    String full_url = "http://" + host_addr + api;
                    try
                    {

                        var credCache = new CredentialCache();
                        credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                        var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
                        String begin_date_str = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                        String end_date_str = DateTime.Now.AddYears(10).ToString("yyyy-MM-ddTHH:mm:ss");
                        var stringContent = new StringContent("{" +
                            "\r\n  \"UserInfo\":{" +
                            "\r\n    \"employeeNo\":\"" + emp_nomer + "\"," +
                            "\r\n    \"name\":\"" + emp_name + "\"," +
                            "\r\n    \"userType\":\"normal\"," +
                            "\r\n    \"Valid\":{\r\n      \"enable\":true ," +
                            "\r\n      \"beginTime\":\"" + begin_date_str + "\"," +
                            "\r\n      \"endTime\":\"" + end_date_str + "\"" +
                            "\r\n    }\r\n  }\r\n}", UnicodeEncoding.UTF8, "application/json");
                        var answer = await httpClient.PutAsync(new Uri(full_url), stringContent);

                        var responseString = await answer.Content.ReadAsStringAsync();
                        Console.WriteLine(responseString);


                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }


                }


            }



            return skudDoorsList;
        }

        [HttpGet("addUserToAllDoors")]
        public async Task<ActionResult<IEnumerable<SkudDoors>>> addUserToAllDoors([FromQuery] long user_id, [FromQuery] String fio)
        {
          
            List<SkudDoors> skudDoorsList = await _context.SkudDoors.ToListAsync();
            if (skudDoorsList != null && skudDoorsList.Count > 0)
            {

                foreach (SkudDoors skudDoors in skudDoorsList)
                {

                    String api = "/ISAPI/AccessControl/UserInfo/SetUp?format=json";
                    String user = "";
                    String password = "";
                    String host_addr = "";
                    long emp_nomer = (long)user_id;
                    String emp_name = fio;




                    if (skudDoors != null)
                    {
                        user = skudDoors.login;
                        password = skudDoors.password;
                        host_addr = skudDoors.device;
                    }

                    String full_url = "http://" + host_addr + api;
                    try
                    {

                        var credCache = new CredentialCache();
                        credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                        var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
                        String begin_date_str = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                        String end_date_str = DateTime.Now.AddYears(10).ToString("yyyy-MM-ddTHH:mm:ss");
                        var stringContent = new StringContent("{" +
                            "\r\n  \"UserInfo\":{" +
                            "\r\n    \"employeeNo\":\""+emp_nomer+"\"," +
                            "\r\n    \"name\":\""+emp_name+"\"," +
                            "\r\n    \"userType\":\"normal\"," +
                            "\r\n    \"Valid\":{\r\n      \"enable\":true ," +
                            "\r\n      \"beginTime\":\""+begin_date_str+"\"," +
                            "\r\n      \"endTime\":\""+end_date_str+"\"" +
                            "\r\n    }\r\n  }\r\n}", UnicodeEncoding.UTF8, "application/json");
                        var answer = await httpClient.PutAsync(new Uri(full_url), stringContent);

                        var responseString = await answer.Content.ReadAsStringAsync();
                        Console.WriteLine(responseString);


                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }


                }


            }



            return skudDoorsList;
        }

        [HttpGet("getBlokUserForAllDoors")]
        public async Task<ActionResult<SkudMyUserinfo>> getBlokUserForAllDoors([FromQuery]long user_id)
        {
            var skudMyUserinfo = await _context.SkudMyUserinfo.FindAsync(user_id);
            List<SkudDoors> skudDoorsList = await _context.SkudDoors.ToListAsync();
            

            if (skudMyUserinfo == null)
            {
                return NotFound();
            }
            
            if (skudDoorsList != null && skudDoorsList.Count > 0) {

                foreach (SkudDoors skudDoors in skudDoorsList) {

                    String api = "/ISAPI/AccessControl/UserInfo/Modify?format=json";
                    String user = "";
                    String password = "";
                    String host_addr = "";
                    long emp_nomer = (long)skudMyUserinfo.userid;
                    String emp_name = skudMyUserinfo.ism;
                    



                    if (skudDoors != null)
                    {
                        user = skudDoors.login;
                        password = skudDoors.password;
                        host_addr = skudDoors.device;
                    }

                    String full_url = "http://" + host_addr + api;
                    try
                    {

                        var credCache = new CredentialCache();
                        credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                        var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
                        String begin_date_str = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                        String end_date_str = DateTime.Now.AddYears(2).ToString("yyyy-MM-ddTHH:mm:ss");
                        var stringContent = new StringContent("{ \r\n  \"UserInfo\":{ \r\n    \"employeeNo\":\""+ emp_nomer + "\", \r\n    \"deleteUser\":false , \r\n    \"name\":\""+emp_name+"\", \r\n    \"userType\":\"blackList\", \r\n    \"Valid\":{ \r\n       \"enable\":true, \r\n       \"beginTime\":\"" + begin_date_str + "\", \r\n       \"endTime\":\"" + end_date_str + "\", \r\n       \"timeType\":\"local\" \r\n    }, \r\n    \"checkUser\":true , \r\n    \"addUser\": false \r\n    } \r\n   \r\n}", UnicodeEncoding.UTF8, "application/json");
                        var answer = await httpClient.PutAsync(new Uri(full_url), stringContent);
                 
                        var responseString = await answer.Content.ReadAsStringAsync();
                        Console.WriteLine(responseString);
                    

                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }


                }

            
            }



            return skudMyUserinfo;
        }


        [HttpGet("getUnBlokUsersListForAllDoorsWithUpdateGr")]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> getUnBlokUsersListForAllDoorsWithUpdateGr([FromQuery] List<long> user_id_list)
        {

            List<SkudMyUserinfo> skudMyUserinfos = new List<SkudMyUserinfo>();
            foreach (long user_id in user_id_list)
            {


                var skudMyUserinfo = await _context.SkudMyUserinfo.FindAsync(user_id);
                List<SkudDoors> skudDoorsList = await _context.SkudDoors.ToListAsync();


                if (skudMyUserinfo == null)
                {
                    return NotFound();
                }

                if (skudMyUserinfo.gr == 1)
                {
                    continue;
                }

                skudMyUserinfos.Add(skudMyUserinfo);


                if (skudDoorsList != null && skudDoorsList.Count > 0)
                {

                    foreach (SkudDoors skudDoors in skudDoorsList)
                    {

                        String api = "/ISAPI/AccessControl/UserInfo/Modify?format=json";
                        String user = "";
                        String password = "";
                        String host_addr = "";
                        long emp_nomer = (long)skudMyUserinfo.userid;
                        String emp_name = skudMyUserinfo.ism;




                        if (skudDoors != null)
                        {
                            user = skudDoors.login;
                            password = skudDoors.password;
                            host_addr = skudDoors.device;
                        }

                        String full_url = "http://" + host_addr + api;
                        try
                        {

                            var credCache = new CredentialCache();
                            credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
                            String begin_date_str = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                            String end_date_str = DateTime.Now.AddYears(2).ToString("yyyy-MM-ddTHH:mm:ss");
                            var stringContent = new StringContent("{ \r\n  \"UserInfo\":{ \r\n    \"employeeNo\":\"" + emp_nomer + "\", \r\n    \"deleteUser\":false , \r\n    \"name\":\"" + emp_name + "\", \r\n    \"userType\":\"normal\", \r\n    \"Valid\":{ \r\n       \"enable\":true, \r\n       \"beginTime\":\"" + begin_date_str + "\", \r\n       \"endTime\":\"" + end_date_str + "\", \r\n       \"timeType\":\"local\" \r\n    }, \r\n    \"checkUser\":true , \r\n    \"addUser\": false \r\n    } \r\n   \r\n}", UnicodeEncoding.UTF8, "application/json");
                            var answer = await httpClient.PutAsync(new Uri(full_url), stringContent);

                            var responseString = await answer.Content.ReadAsStringAsync();
                            Console.WriteLine(responseString);


                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine(e.Message);
                        }


                    }

                    //gr ni 0 qilib qoyish kerak
                    skudMyUserinfo.gr = 0;
                    _context.SkudMyUserinfo.Update(skudMyUserinfo);
                    await _context.SaveChangesAsync();

                }
            }


            return skudMyUserinfos;
        }


        [HttpGet("getBlokUsersListForAllDoorsWithUpdateGrForCallAnyServerForCurrentDay")]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> getBlokUsersListForAllDoorsWithUpdateGrForCallAnyServerForCurrentDay()
        {
            String date_only = DateTime.Now.Date.ToString("yyyy-MM-dd");

            List<SkudKemaganUserTemp> itemList = await _context.SkudKemaganUserTemp
               .FromSqlRaw("  SELECT  mu.userid as userid,mu.ism as ism ,mu.familiya as dept\r\n " +
               " FROM my_userinfo mu\r\n  WHERE mu.userid NOT IN" +
               " (SELECT userid FROM my_checkinout WHERE sana = '" + date_only + "')\r\n " +
               " GROUP BY mu.userid,mu.ism,mu.familiya\r\n  ORDER BY mu.ism")
               .ToListAsync();

            List<SkudMyUserinfo> skudMyUserinfos = new List<SkudMyUserinfo>();
            if (itemList == null || itemList.Count == 0) {
                return skudMyUserinfos;
            }

            if ((int)DateTime.Now.DayOfWeek == 0)
            {
                return skudMyUserinfos;

            }

            foreach (SkudKemaganUserTemp itm in itemList)
            {


                var skudMyUserinfo = await _context.SkudMyUserinfo.FindAsync(itm.userid);
                List<SkudDoors> skudDoorsList = await _context.SkudDoors.ToListAsync();


                if (skudMyUserinfo == null)
                {
                    return NotFound();
                }

                if (skudMyUserinfo.gr == 1)
                {
                    continue;
                }

                skudMyUserinfos.Add(skudMyUserinfo);


                if (skudDoorsList != null && skudDoorsList.Count > 0)
                {

                    foreach (SkudDoors skudDoors in skudDoorsList)
                    {

                        String api = "/ISAPI/AccessControl/UserInfo/Modify?format=json";
                        String user = "";
                        String password = "";
                        String host_addr = "";
                        long emp_nomer = (long)skudMyUserinfo.userid;
                        String emp_name = skudMyUserinfo.ism;




                        if (skudDoors != null)
                        {
                            user = skudDoors.login;
                            password = skudDoors.password;
                            host_addr = skudDoors.device;
                        }

                        String full_url = "http://" + host_addr + api;
                        try
                        {

                            var credCache = new CredentialCache();
                            credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
                            String begin_date_str = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                            String end_date_str = DateTime.Now.AddYears(2).ToString("yyyy-MM-ddTHH:mm:ss");
                            var stringContent = new StringContent("{ \r\n  \"UserInfo\":{ \r\n    \"employeeNo\":\"" + emp_nomer + "\", \r\n    \"deleteUser\":false , \r\n    \"name\":\"" + emp_name + "\", \r\n    \"userType\":\"blackList\", \r\n    \"Valid\":{ \r\n       \"enable\":true, \r\n       \"beginTime\":\"" + begin_date_str + "\", \r\n       \"endTime\":\"" + end_date_str + "\", \r\n       \"timeType\":\"local\" \r\n    }, \r\n    \"checkUser\":true , \r\n    \"addUser\": false \r\n    } \r\n   \r\n}", UnicodeEncoding.UTF8, "application/json");
                            var answer = await httpClient.PutAsync(new Uri(full_url), stringContent);

                            var responseString = await answer.Content.ReadAsStringAsync();
                            Console.WriteLine(responseString);


                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine(e.Message);
                        }


                    }

                    //gr ni 2 qilib qoyish kerak
                    skudMyUserinfo.gr = 2;
                    _context.SkudMyUserinfo.Update(skudMyUserinfo);
                    await _context.SaveChangesAsync();

                }
            }


            return skudMyUserinfos;
        }

        [HttpGet("getBlokUsersListForAllDoorsWithUpdateGr")]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> getBlokUsersListForAllDoorsWithUpdateGr([FromQuery] List<long> user_id_list)
        {

            List<SkudMyUserinfo> skudMyUserinfos = new List<SkudMyUserinfo>();
            foreach (long user_id in user_id_list)
            {


                var skudMyUserinfo = await _context.SkudMyUserinfo.FindAsync(user_id);
                List<SkudDoors> skudDoorsList = await _context.SkudDoors.ToListAsync();


                if (skudMyUserinfo == null)
                {
                    return NotFound();
                }

                if (skudMyUserinfo.gr == 1)
                {
                    continue;
                }

                skudMyUserinfos.Add(skudMyUserinfo);

                
                if (skudDoorsList != null && skudDoorsList.Count > 0)
                {

                    foreach (SkudDoors skudDoors in skudDoorsList)
                    {

                        String api = "/ISAPI/AccessControl/UserInfo/Modify?format=json";
                        String user = "";
                        String password = "";
                        String host_addr = "";
                        long emp_nomer = (long)skudMyUserinfo.userid;
                        String emp_name = skudMyUserinfo.ism;




                        if (skudDoors != null)
                        {
                            user = skudDoors.login;
                            password = skudDoors.password;
                            host_addr = skudDoors.device;
                        }

                        String full_url = "http://" + host_addr + api;
                        try
                        {

                            var credCache = new CredentialCache();
                            credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
                            String begin_date_str = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                            String end_date_str = DateTime.Now.AddYears(2).ToString("yyyy-MM-ddTHH:mm:ss");
                            var stringContent = new StringContent("{ \r\n  \"UserInfo\":{ \r\n    \"employeeNo\":\"" + emp_nomer + "\", \r\n    \"deleteUser\":false , \r\n    \"name\":\"" + emp_name + "\", \r\n    \"userType\":\"blackList\", \r\n    \"Valid\":{ \r\n       \"enable\":true, \r\n       \"beginTime\":\"" + begin_date_str + "\", \r\n       \"endTime\":\"" + end_date_str + "\", \r\n       \"timeType\":\"local\" \r\n    }, \r\n    \"checkUser\":true , \r\n    \"addUser\": false \r\n    } \r\n   \r\n}", UnicodeEncoding.UTF8, "application/json");
                            var answer = await httpClient.PutAsync(new Uri(full_url), stringContent);

                            var responseString = await answer.Content.ReadAsStringAsync();
                            Console.WriteLine(responseString);


                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine(e.Message);
                        }


                    }

                    //gr ni 2 qilib qoyish kerak
                    skudMyUserinfo.gr = 2;
                    _context.SkudMyUserinfo.Update(skudMyUserinfo);
                    await _context.SaveChangesAsync();

                }
            }


            return skudMyUserinfos;
        }


        [HttpGet("getBlokUsersListForAllDoors")]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> getBlokUsersListForAllDoors([FromQuery] List<long> user_id_list)
        {

            List<SkudMyUserinfo> skudMyUserinfos = new List<SkudMyUserinfo>();
            foreach (long user_id in user_id_list) { 
            
           
            var skudMyUserinfo = await _context.SkudMyUserinfo.FindAsync(user_id);
            List<SkudDoors> skudDoorsList = await _context.SkudDoors.ToListAsync();


            if (skudMyUserinfo == null)
            {
                return NotFound();
            }

                if (skudMyUserinfo.gr == 1) {
                    continue;
                }

                skudMyUserinfos.Add(skudMyUserinfo);

            if (skudDoorsList != null && skudDoorsList.Count > 0)
            {

                foreach (SkudDoors skudDoors in skudDoorsList)
                {

                    String api = "/ISAPI/AccessControl/UserInfo/Modify?format=json";
                    String user = "";
                    String password = "";
                    String host_addr = "";
                    long emp_nomer = (long)skudMyUserinfo.userid;
                    String emp_name = skudMyUserinfo.ism;




                    if (skudDoors != null)
                    {
                        user = skudDoors.login;
                        password = skudDoors.password;
                        host_addr = skudDoors.device;
                    }

                    String full_url = "http://" + host_addr + api;
                    try
                    {

                        var credCache = new CredentialCache();
                        credCache.Add(new Uri(full_url), "Digest", new NetworkCredential(user, password));
                        var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
                        String begin_date_str = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                        String end_date_str = DateTime.Now.AddYears(2).ToString("yyyy-MM-ddTHH:mm:ss");
                        var stringContent = new StringContent("{ \r\n  \"UserInfo\":{ \r\n    \"employeeNo\":\"" + emp_nomer + "\", \r\n    \"deleteUser\":false , \r\n    \"name\":\"" + emp_name + "\", \r\n    \"userType\":\"blackList\", \r\n    \"Valid\":{ \r\n       \"enable\":true, \r\n       \"beginTime\":\"" + begin_date_str + "\", \r\n       \"endTime\":\"" + end_date_str + "\", \r\n       \"timeType\":\"local\" \r\n    }, \r\n    \"checkUser\":true , \r\n    \"addUser\": false \r\n    } \r\n   \r\n}", UnicodeEncoding.UTF8, "application/json");
                        var answer = await httpClient.PutAsync(new Uri(full_url), stringContent);

                        var responseString = await answer.Content.ReadAsStringAsync();
                        Console.WriteLine(responseString);


                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }


                }


            }
            }


            return skudMyUserinfos;
        }

        [HttpGet("getReportWithoutSmenaPaginationByAllUserAndOylik")]
        public async Task<ActionResult<TexPaginationModel>> getReportWithoutSmenaPaginationByAllUserAndOylik([FromQuery] int page,
[FromQuery] int size,  [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            Console.WriteLine("1 __________________________");
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();

            Console.WriteLine("2 ________________________");
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }

            foreach (SkudMyUserinfo it in itemList)
            {
                if (it == null) {
                    continue;
                }

                double worked_secunds_sm = 0.0;
                for (var dt = begin_date; dt <= end_date; dt = dt.AddDays(1))
                {

                    String date_only = dt.Date.ToString("yyyy-MM-dd");
                    Console.WriteLine(date_only);
                    it.kun = date_only;

                    if (it != null)
                    {
                        if (it.ishlagan_vaqtlar_ls == null)
                        {
                            it.ishlagan_vaqtlar_ls = new List<string>();
                        }
                        Console.WriteLine("KIRDI");
                        //osha kundagi kirdi chiqdilardi olndi
                        List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .Where(p => p.sana.Date == dt.Date && p.userid == it.userid && (p.checktype != "K" && p.checktype != "C"))
                        .OrderBy(p => p.checktime)
                        .Skip(size * page).Take(size)
                        .ToListAsync();
                        String ishlagan_vaqti = "0";
                        String ishlagan_vaqti_formatted = "0";
                        Console.WriteLine("KIRDI 11111");
                        //agar natija bolsa chiqaradi bolmasa 0 yozib qoyamiz
                        if (checkinouts != null && checkinouts.Count() > 0)
                        {
                            int counter = 0;
                            double secunds = 0;
                            bool _last_out = false;
                            bool _last_in = false;
                            TimeSpan _last_in_tm = TimeSpan.FromSeconds(0);
                            TimeSpan _last_out_tm = TimeSpan.FromSeconds(0);
                            foreach (SkudMyCheckinout itm_1 in checkinouts)
                            {

                                Console.WriteLine("KIRDI 22222");
                                //KECHASI ISHLADI BUNDA
                                if (counter == 0 && itm_1.checktype == "O")
                                {
                                    secunds = secunds + itm_1.checktime.TotalSeconds;
                                   
                                    _last_out = true;
                                    counter++;
                                    continue;
                                }
                                //KIRISH
                                if (itm_1.checktype == "I")
                                {

                                    //bu eshigdi tagida turibdi qayta qayta olyapti shunchun torilab qoydik
                                    _last_in_tm = itm_1.checktime;
                                    _last_in = true;

                                }
                                //CHIQISH
                                if (itm_1.checktype == "O")
                                {
                                    if (_last_in)
                                    {
                                        secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds);
                                        
                                        _last_in = false;
                                    }
                                    else
                                    {
                                        counter++;
                                        continue;
                                    }

                                }



                                counter++;
                            }
                            Console.WriteLine("KIRDI 3333333");
                            if (_last_in)
                            {
                                secunds = secunds + (86400 - _last_in_tm.TotalSeconds);
                               
                            }
                            Console.WriteLine("KIRDI 4444");
                            worked_secunds_sm = worked_secunds_sm + secunds;
                            ishlagan_vaqti = secunds.ToString();
                            
                            ishlagan_vaqti_formatted = TimeSpan.FromSeconds(secunds).ToString("hh\\:mm\\:ss");


                            it.ishlagan_vaqtlar_ls.Add(ishlagan_vaqti_formatted);
                            Console.WriteLine("KIRDI 555");
                        }
                        it.ishlagan_vaqti_yangi = ishlagan_vaqti;
                        it.ishlagan_vaqti_yangi_time_format = ishlagan_vaqti_formatted;
                        Console.WriteLine("KIRDI 666");



                    }


                    /* List<SkudVaqtTemp> result = await _context.SkudVaqtTemp.FromSqlRaw("select TO_CHAR(working_time_without_gr,'HH24:MI:SS') as vaqt from working_time_without_gr(" + it.userid + ",'" + date_only + "')").ToListAsync();
                     if (result != null && result.Count > 0)
                     {
                         String ishlagan_wq = result[0].vaqt;

                         if (it.ishlagan_vaqtlar_ls == null)
                         {
                             it.ishlagan_vaqtlar_ls = new List<string>();
                         }
                         if (ishlagan_wq == null || ishlagan_wq.Trim().Length == 0)
                         {
                             continue;
                         }
                         it.ishlagan_vaqtlar_ls.Add(ishlagan_wq);

                         if (ishlagan_wq.Trim().Length > 0)
                         {
                             string[] vaqtlar = ishlagan_wq.Trim().Split(":");
                             if (vaqtlar.Length > 0)
                             {
                                 int counter = 0;
                                 foreach (string vaqt in vaqtlar)
                                 {
                                     if (vaqt.Trim().Length > 0)
                                     {


                                         if (counter == 0)
                                         {
                                             //hour

                                             int hour = 0;

                                             if (Int32.TryParse(vaqt, out hour))
                                             {
                                                 worked_secunds_sm = worked_secunds_sm + (hour * 3600);

                                             }

                                         }
                                         else if (counter == 1)
                                         {
                                             //minutes

                                             int minutes = 0;

                                             if (Int32.TryParse(vaqt, out minutes))
                                             {
                                                 worked_secunds_sm = worked_secunds_sm + (minutes * 60);


                                             }

                                         }
                                         else if (counter == 2)
                                         {
                                             //secunds
                                             int secunds = 0;

                                             if (Int32.TryParse(vaqt, out secunds))
                                             {
                                                 worked_secunds_sm = worked_secunds_sm + secunds;


                                             }

                                         }
                                     }


                                     counter++;
                                 }
                             }


                         }

                     }*/

                }

                //hamma vaqtlar boyicha olindi ishlaganlar endi uni secundan soatga otkazish kerak
                it.worked_hours_itm = worked_secunds_sm / 3600;
                if (it.skudOylik != null)
                {
                    it.ishlagan_puli = it.worked_hours_itm * it.skudOylik.value;
                }

                Console.WriteLine("CHIQDI");

            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getReportWithoutSmenaPaginationByAllUserAndOylik0404")]
        public async Task<ActionResult<TexPaginationModel>> getReportWithoutSmenaPaginationByAllUserAndOylik0404([FromQuery] int page,
[FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            Console.WriteLine("04 1 __________________________");
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();

            Console.WriteLine("04 2 ________________________");
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }

            foreach (SkudMyUserinfo it in itemList)
            {
                if (it == null)
                {
                    continue;
                }

                double worked_secunds_sm = 0.0;
                for (var dt = begin_date; dt <= end_date; dt = dt.AddDays(1))
                {

                    String date_only = dt.Date.ToString("yyyy-MM-dd");
                    Console.WriteLine(date_only);
                    it.kun = date_only;

                    if (it != null)
                    {
                        if (it.ishlagan_vaqtlar_ls == null)
                        {
                            it.ishlagan_vaqtlar_ls = new List<string>();
                        }
                        //Console.WriteLine("KIRDI");
                        //osha kundagi kirdi chiqdilardi olndi
                        List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .Where(p => p.userid == it.userid && (p.checktype != "K" && p.checktype != "C") && ((p.sana + p.checktime) > dt.Date.AddHours(4) && (p.sana + p.checktime) < dt.Date.AddHours(4).AddDays(1)))
                        .OrderBy(p => p.sana)
                        .OrderBy(p => p.checktime)
                        .Skip(size * page).Take(size)
                        .ToListAsync();
                        // Console.WriteLine("TIME: " + dt.Date.AddHours(4) + " VA " + dt.Date.AddHours(4).AddDays(1));
                        String ishlagan_vaqti = "0";
                        String ishlagan_vaqti_formatted = "0";
                        // Console.WriteLine("KIRDI 11111");
                        //agar natija bolsa chiqaradi bolmasa 0 yozib qoyamiz
                        if (checkinouts != null && checkinouts.Count() > 0)
                        {
                            int counter = 0;
                            double secunds = 0;
                            bool _last_out = false;
                            bool _last_in = false;
                            TimeSpan _last_in_tm = TimeSpan.FromSeconds(0);
                            TimeSpan _last_out_tm = TimeSpan.FromSeconds(0);
                            foreach (SkudMyCheckinout itm_1 in checkinouts)
                            {

                                // Console.WriteLine("KIRDI 22222");
                                //KECHASI ISHLADI BUNDA
                                if (counter == 0 && itm_1.checktype == "O")
                                {
                                    Console.WriteLine("Birinchi chiqish boldi qoshmadik sekund");
                                    //secunds = secunds + itm_1.checktime.TotalSeconds;


                                    _last_out = true;
                                    counter++;
                                    continue;
                                }
                                //KIRISH
                                if (itm_1.checktype == "I")
                                {

                                    //bu eshigdi tagida turibdi qayta qayta olyapti shunchun torilab qoydik
                                    _last_in_tm = itm_1.checktime;
                                    _last_in = true;

                                }
                                //CHIQISH
                                if (itm_1.checktype == "O")
                                {
                                    if (_last_in)
                                    {
                                        secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds);

                                        _last_in = false;
                                    }
                                    else
                                    {
                                        counter++;
                                        continue;
                                    }

                                }



                                counter++;
                            }

                            if (_last_in)
                            {
                                Console.WriteLine("CHIQSHI YOQ FAQAT KIRISH");
                                // secunds = secunds + (86400 - _last_in_tm.TotalSeconds);

                            }

                            worked_secunds_sm = worked_secunds_sm + secunds;
                            ishlagan_vaqti = secunds.ToString();

                            ishlagan_vaqti_formatted = TimeSpan.FromSeconds(secunds).ToString("hh\\:mm\\:ss");


                            it.ishlagan_vaqtlar_ls.Add(ishlagan_vaqti_formatted);

                        }
                        it.ishlagan_vaqti_yangi = ishlagan_vaqti;
                        it.ishlagan_vaqti_yangi_time_format = ishlagan_vaqti_formatted;




                    }

                }

                //hamma vaqtlar boyicha olindi ishlaganlar endi uni secundan soatga otkazish kerak
                it.worked_hours_itm = worked_secunds_sm / 3600;
                if (it.skudOylik != null)
                {
                    it.ishlagan_puli = it.worked_hours_itm * it.skudOylik.value;
                }

                Console.WriteLine("FINISH");

            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        [HttpGet("getReportWithoutSmenaPaginationByAllUserAndOylik0404New")]
        public async Task<ActionResult<TexPaginationModel>> getReportWithogetReportWithoutSmenaPaginationByAllUserAndOylik0404NewutSmenaPaginationByAllUserAndOylik0404WithUserId([FromQuery] int page,
[FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {

            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();


            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }

            foreach (SkudMyUserinfo it in itemList)
            {
                if (it == null)
                {
                    continue;
                }

                double worked_secunds_sm = 0.0;
                for (var dt = begin_date; dt <= end_date; dt = dt.AddDays(1))
                {

                    String date_only = dt.Date.ToString("yyyy-MM-dd");
                    Console.WriteLine(date_only);
                    it.kun = date_only;

                    if (it != null)
                    {
                        if (it.ishlagan_vaqtlar_ls == null)
                        {
                            it.ishlagan_vaqtlar_ls = new List<string>();
                        }
                        //Console.WriteLine("KIRDI");
                        //osha kundagi kirdi chiqdilardi olndi
                        List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .FromSqlRaw("SELECT code, userid, sana, checktime, checktype, door_name, status,''::text as begona" +
                                "\r\nFROM public.my_checkinout" +
                                "\r\nWHERE (sana+checktime)   >= '" + dt.Date.AddHours(4).ToString("yyyy-MM-ddTHH:mm:ss")
                                + "' and (sana+checktime) <= '" + dt.Date.AddHours(4).AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss") + "'" +
                                " \r\nand checktype != 'K' and checktype != 'C'" +
                                "\r\nAND userid = " + it.userid + "" +
                                "\r\nORDER BY sana , checktime")
                        .ToListAsync();

                        /*List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .Where(p => p.userid == it.userid && (p.checktype != "K" && p.checktype != "C") && ((p.sana + p.checktime) >= dt.Date.AddHours(4) && (p.sana + p.checktime) <= dt.Date.AddHours(4).AddDays(1)))
                        .OrderBy(p => p.sana)
                        .OrderBy(p => p.checktime)
                        .Skip(size * page).Take(size)
                        .ToListAsync();*/
                       // Console.WriteLine("TIME: " + dt.Date.AddHours(4) + " VA " + dt.Date.AddHours(4).AddDays(1));
                        String ishlagan_vaqti = "0";
                        String ishlagan_vaqti_formatted = "0";
                        // Console.WriteLine("KIRDI 11111");
                        //agar natija bolsa chiqaradi bolmasa 0 yozib qoyamiz
                        if (checkinouts != null && checkinouts.Count() > 0)
                        {
                           // Console.WriteLine(checkinouts.Count());

                            int counter = 0;
                            double secunds = 0;
                            bool _last_out = false;
                            bool _last_in = false;
                            TimeSpan _last_in_tm = TimeSpan.FromSeconds(0);
                            TimeSpan _last_out_tm = TimeSpan.FromSeconds(0);
                            foreach (SkudMyCheckinout itm_1 in checkinouts)
                            {

                                // Console.WriteLine("KIRDI 22222");
                                //KECHASI ISHLADI BUNDA
                                if (counter == 0 && itm_1.checktype == "O")
                                {
                                   // Console.WriteLine("Birinchi chiqish boldi qoshmadik sekund");
                                    //secunds = secunds + itm_1.checktime.TotalSeconds;


                                    _last_out = true;
                                    counter++;
                                    continue;
                                }
                                //KIRISH
                                if (itm_1.checktype == "I")
                                {

                                    //bu eshigdi tagida turibdi qayta qayta olyapti shunchun torilab qoydik
                                    _last_in_tm = itm_1.checktime;
                                    _last_in = true;

                                }
                                //CHIQISH
                                if (itm_1.checktype == "O")
                                {
                                    if (_last_in)
                                    {
                                        if (_last_in_tm.TotalSeconds < itm_1.checktime.TotalSeconds)
                                        {
                                            secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds);
                                        }
                                        else
                                        {
                                            secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds) + 86400;
                                        }


                                        _last_in = false;

                                    //    Console.WriteLine(itm_1.checktime.ToString() + "  ---------   " + _last_in_tm.ToString());
                                    //    Console.WriteLine(itm_1.checktime.TotalSeconds.ToString() + "  ---------   " + _last_in_tm.TotalSeconds.ToString());

                                    }
                                    else
                                    {
                                        counter++;
                                        continue;
                                    }

                                }



                                counter++;
                            }

                            if (_last_in)
                            {
                               // Console.WriteLine("CHIQSHI YOQ FAQAT KIRISH");
                                // secunds = secunds + (86400 - _last_in_tm.TotalSeconds);

                            }

                            worked_secunds_sm = worked_secunds_sm + secunds;
                            ishlagan_vaqti = secunds.ToString();

                            ishlagan_vaqti_formatted = TimeSpan.FromSeconds(secunds).ToString("hh\\:mm\\:ss");


                            it.ishlagan_vaqtlar_ls.Add(ishlagan_vaqti_formatted);

                        }
                        it.ishlagan_vaqti_yangi = ishlagan_vaqti;
                        it.ishlagan_vaqti_yangi_time_format = ishlagan_vaqti_formatted;




                    }

                }

                //hamma vaqtlar boyicha olindi ishlaganlar endi uni secundan soatga otkazish kerak
                it.worked_hours_itm = worked_secunds_sm / 3600;
                it.worked_hours_itm_str = TimeSpan.FromSeconds(worked_secunds_sm).ToString("hh\\:mm\\:ss");

                if (it.skudOylik != null)
                {
                    it.ishlagan_puli = it.worked_hours_itm * it.skudOylik.value;
                }

               // Console.WriteLine("FINISH");

            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getReportWithoutSmenaPaginationByAllUserAndOylik0404Qorovulxona")]
        public async Task<ActionResult<TexPaginationModel>> getReportWithogetReportWithoutSmenaPaginationByAllUserAndOylik0404Qorovulxona([FromQuery] int page,
        [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {

            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();


            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }

            foreach (SkudMyUserinfo it in itemList)
            {
                if (it == null)
                {
                    continue;
                }

                double worked_secunds_sm = 0.0;
                for (var dt = begin_date; dt <= end_date; dt = dt.AddDays(1))
                {

                    String date_only = dt.Date.ToString("yyyy-MM-dd");
                    Console.WriteLine(date_only);
                    it.kun = date_only;

                    if (it != null)
                    {
                        if (it.ishlagan_vaqtlar_ls == null)
                        {
                            it.ishlagan_vaqtlar_ls = new List<string>();
                        }
                        //Console.WriteLine("KIRDI");
                        //osha kundagi kirdi chiqdilardi olndi
                        List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .FromSqlRaw("SELECT code, userid, sana, checktime, checktype, door_name, status,''::text as begona" +
                                "\r\nFROM public.my_checkinout" +
                                "\r\nWHERE (sana+checktime)   >= '" + dt.Date.AddHours(4).ToString("yyyy-MM-ddTHH:mm:ss")
                                + "' and (sana+checktime) <= '" + dt.Date.AddHours(4).AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss") + "'" +
                                " \r\nand checktype != 'I' and checktype != 'O'" +
                                "\r\nAND userid = " + it.userid + "" +
                                "\r\nORDER BY sana , checktime")
                        .ToListAsync();

                        /*List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .Where(p => p.userid == it.userid && (p.checktype != "K" && p.checktype != "C") && ((p.sana + p.checktime) >= dt.Date.AddHours(4) && (p.sana + p.checktime) <= dt.Date.AddHours(4).AddDays(1)))
                        .OrderBy(p => p.sana)
                        .OrderBy(p => p.checktime)
                        .Skip(size * page).Take(size)
                        .ToListAsync();*/
                        // Console.WriteLine("TIME: " + dt.Date.AddHours(4) + " VA " + dt.Date.AddHours(4).AddDays(1));
                        String ishlagan_vaqti = "0";
                        String ishlagan_vaqti_formatted = "0";
                        // Console.WriteLine("KIRDI 11111");
                        //agar natija bolsa chiqaradi bolmasa 0 yozib qoyamiz
                        if (checkinouts != null && checkinouts.Count() > 0)
                        {
                            // Console.WriteLine(checkinouts.Count());

                            int counter = 0;
                            double secunds = 0;
                            bool _last_out = false;
                            bool _last_in = false;
                            TimeSpan _last_in_tm = TimeSpan.FromSeconds(0);
                            TimeSpan _last_out_tm = TimeSpan.FromSeconds(0);
                            foreach (SkudMyCheckinout itm_1 in checkinouts)
                            {

                                // Console.WriteLine("KIRDI 22222");
                                //KECHASI ISHLADI BUNDA
                                if (counter == 0 && itm_1.checktype == "C")
                                {
                                    // Console.WriteLine("Birinchi chiqish boldi qoshmadik sekund");
                                    //secunds = secunds + itm_1.checktime.TotalSeconds;


                                    _last_out = true;
                                    counter++;
                                    continue;
                                }
                                //KIRISH
                                if (itm_1.checktype == "K")
                                {

                                    //bu eshigdi tagida turibdi qayta qayta olyapti shunchun torilab qoydik
                                    _last_in_tm = itm_1.checktime;
                                    _last_in = true;

                                }
                                //CHIQISH
                                if (itm_1.checktype == "C")
                                {
                                    if (_last_in)
                                    {
                                        if (_last_in_tm.TotalSeconds < itm_1.checktime.TotalSeconds)
                                        {
                                            secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds);
                                        }
                                        else
                                        {
                                            secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds) + 86400;
                                        }


                                        _last_in = false;

                                        //    Console.WriteLine(itm_1.checktime.ToString() + "  ---------   " + _last_in_tm.ToString());
                                        //    Console.WriteLine(itm_1.checktime.TotalSeconds.ToString() + "  ---------   " + _last_in_tm.TotalSeconds.ToString());

                                    }
                                    else
                                    {
                                        counter++;
                                        continue;
                                    }

                                }
                                counter++;
                            }

                            if (_last_in)
                            {
                                // Console.WriteLine("CHIQSHI YOQ FAQAT KIRISH");
                                // secunds = secunds + (86400 - _last_in_tm.TotalSeconds);

                            }

                            worked_secunds_sm = worked_secunds_sm + secunds;
                            ishlagan_vaqti = secunds.ToString();

                            ishlagan_vaqti_formatted = TimeSpan.FromSeconds(secunds).ToString("hh\\:mm\\:ss");


                            it.ishlagan_vaqtlar_ls.Add(ishlagan_vaqti_formatted);

                            if (it.vaqt_flag != 0 && it.vaqt_flag != 1 && it.vaqt_flag != 3) {
                                if (secunds == 0)
                                {
                                    var start = dt.Date.AddHours(4);
                                    var end   = start.AddDays(1);

                                    var hasKirishYokiChiqish = (await _context.SkudMyCheckinout
                                        .Where(x => x.userid == it.userid && 
                                                    (x.checktype == "K" || x.checktype == "C"))
                                        .Select(x => new { 
                                            x.userid, 
                                            x.checktype, 
                                            FullDate = x.sana.Add(x.checktime)   // EF Core buni ko‘tarolmasa, keyin xotirada hisoblaymiz
                                        })
                                        .ToListAsync())
                                        .Any(x => x.FullDate >= start && x.FullDate <= end);

                                    if (hasKirishYokiChiqish)
                                    {
                                        it.vaqt_flag = 3; // Kirish yoki chiqish bor, lekin ishlagan vaqt 0
                                        
                                    }
                                }
                                else if ((secunds / 3600) <= 7 && (secunds / 3600) > 0)
                                {
                                    it.vaqt_flag = 0; // kam ishlagan kun
                                   
                                }
                                else if ((secunds / 3600) > 13)
                                {
                                    it.vaqt_flag = 1; // juda ko‘p ishlagan kun
                                    
                                }
                            }

                        }
                        it.ishlagan_vaqti_yangi = ishlagan_vaqti;
                        it.ishlagan_vaqti_yangi_time_format = ishlagan_vaqti_formatted;
                    }

                }

                //hamma vaqtlar boyicha olindi ishlaganlar endi uni secundan soatga otkazish kerak
                it.worked_hours_itm = worked_secunds_sm / 3600;
                it.worked_hours_itm_str = TimeSpan.FromSeconds(worked_secunds_sm).ToString("hh\\:mm\\:ss");

                if (it.skudOylik != null)
                {
                    it.ishlagan_puli = it.worked_hours_itm * it.skudOylik.value;
                }

                // // Shu yerga qo‘shamiz
                // var daysDiff = (end_date.Date - begin_date.Date).TotalDays;
                // if (daysDiff == 0) // 1 kun tanlanganda
                // {
                //     if (it.worked_hours_itm == 0)
                //     {
                //         // Shu kun uchun kirish yoki chiqish yozuvi bo'lganmi?
                //         bool hasKirishYokiChiqish = await _context.SkudMyCheckinout
                //             .AnyAsync(x => x.userid == it.userid &&
                //                         (x.checktype == "K" || x.checktype == "C") &&
                //                         ((x.sana + x.checktime) >= begin_date.Date.AddHours(4) &&
                //                             (x.sana + x.checktime) <= begin_date.Date.AddHours(4).AddDays(1)));

                //         if (hasKirishYokiChiqish)
                //         {
                //             it.vaqt_flag = 3; // Kirish yoki chiqish bor, lekin ishlagan vaqt 0
                //         }
                //         else
                //         {
                //             it.vaqt_flag = null; // yoki hech narsa bermaslik
                //         }
                //     }
                //     if (it.worked_hours_itm <= 7 && it.worked_hours_itm > 0)
                //     {
                //         it.vaqt_flag = 0;
                //     }
                //     else if (it.worked_hours_itm > 13)
                //     {
                //         it.vaqt_flag = 1;
                //     }
                // }
                // else if (daysDiff > 0) // 1 kundan ko‘p
                // {
                //     it.vaqt_flag = 2;
                // }
                
                
                // Xodim eng birinchi kirgan/chiqqan vaqtini topish
                var firstCheck = await _context.SkudMyCheckinout
                    .Where(x => x.userid == it.userid && (x.checktype == "K" || x.checktype == "C"))
                    .OrderBy(x => x.sana)
                    .ThenBy(x => x.checktime)
                    .FirstOrDefaultAsync();

                if (firstCheck != null)
                {
                    DateTime firstDateTime = firstCheck.sana.Add(firstCheck.checktime);
                    TimeSpan diff = DateTime.Now - firstDateTime;

                    int years = diff.Days / 365;
                    int days = diff.Days % 365;
                    int hours = diff.Hours;

                    it.kelganidan_beri = $"{years} yil {days} kun {hours} soat";
                }
                else
                {
                    it.kelganidan_beri = $"{0} yil {0} kun {0} soat";
                }
               // Console.WriteLine("FINISH");

               // SkudUserNote jadvali mavjudligini tekshiramiz (agar jadval mavjud bo'lmasa, xatolikni boshqaramiz)
               bool hasUserNote = false;
               try
               {
                   hasUserNote = await _context.SkudUserNote
                                .AnyAsync(x => x.userid == it.userid);
               }
               catch (PostgresException ex) when (ex.SqlState == "42P01") // Jadval mavjud emas
               {
                   // Jadval mavjud emas, hasUserNote = false qoldiramiz
                   Console.WriteLine($"⚠️ SkudUserNote jadvali mavjud emas: {ex.Message}");
               }
               catch (Exception ex)
               {
                   // Boshqa xatoliklar
                   Console.WriteLine($"⚠️ SkudUserNote tekshirishda xatolik: {ex.Message}");
               }

                if(hasUserNote){
                    it.user_note = 1;
                }


            }


            paginationModel.items_list = JArray.FromObject(
                itemList
                .OrderBy(it => it.vaqt_flag == 3 ? 0
                   : it.vaqt_flag == 0 ? 1
                   : it.vaqt_flag == 1 ? 2
                   : 3)
                );
            paginationModel.items_count = await _context.SkudMyUserinfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }










        [HttpGet("getReportWithoutSmenaPaginationByAllUserAndOylik0404IchkiQorovulxona")]
        public async Task<ActionResult<TexPaginationModel>> getReportWithogetReportWithoutSmenaPaginationByAllUserAndOylik0404IchkiQorovulxona([FromQuery] int page,
        [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {

            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();


            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }

            foreach (SkudMyUserinfo it in itemList)
            {
                if (it == null)
                {
                    continue;
                }

                double worked_secunds_sm = 0.0;
                for (var dt = begin_date; dt <= end_date; dt = dt.AddDays(1))
                {

                    String date_only = dt.Date.ToString("yyyy-MM-dd");
                    Console.WriteLine(date_only);
                    it.kun = date_only;

                    if (it != null)
                    {
                        if (it.ishlagan_vaqtlar_ls == null)
                        {
                            it.ishlagan_vaqtlar_ls = new List<string>();
                        }
                        //Console.WriteLine("KIRDI");
                        //osha kundagi kirdi chiqdilardi olndi
                        List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .FromSqlRaw("SELECT code, userid, sana, checktime, checktype, door_name, status,''::text as begona" +
                                "\r\nFROM public.my_checkinout" +
                                "\r\nWHERE (sana+checktime)   >= '" + dt.Date.AddHours(4).ToString("yyyy-MM-ddTHH:mm:ss")
                                + "' and (sana+checktime) <= '" + dt.Date.AddHours(4).AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss") + "'" +
                                " \r\nand checktype != 'K' and checktype != 'C'" +
                                "\r\nAND userid = " + it.userid + "" +
                                "\r\nORDER BY sana , checktime")
                        .ToListAsync();

                        /*List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .Where(p => p.userid == it.userid && (p.checktype != "K" && p.checktype != "C") && ((p.sana + p.checktime) >= dt.Date.AddHours(4) && (p.sana + p.checktime) <= dt.Date.AddHours(4).AddDays(1)))
                        .OrderBy(p => p.sana)
                        .OrderBy(p => p.checktime)
                        .Skip(size * page).Take(size)
                        .ToListAsync();*/
                        // Console.WriteLine("TIME: " + dt.Date.AddHours(4) + " VA " + dt.Date.AddHours(4).AddDays(1));
                        String ishlagan_vaqti = "0";
                        String ishlagan_vaqti_formatted = "0";
                        // Console.WriteLine("KIRDI 11111");
                        //agar natija bolsa chiqaradi bolmasa 0 yozib qoyamiz
                        if (checkinouts != null && checkinouts.Count() > 0)
                        {
                            // Console.WriteLine(checkinouts.Count());

                            int counter = 0;
                            double secunds = 0;
                            bool _last_out = false;
                            bool _last_in = false;
                            TimeSpan _last_in_tm = TimeSpan.FromSeconds(0);
                            TimeSpan _last_out_tm = TimeSpan.FromSeconds(0);
                            foreach (SkudMyCheckinout itm_1 in checkinouts)
                            {

                                // Console.WriteLine("KIRDI 22222");
                                //KECHASI ISHLADI BUNDA
                                if (counter == 0 && itm_1.checktype == "O")
                                {
                                    // Console.WriteLine("Birinchi chiqish boldi qoshmadik sekund");
                                    //secunds = secunds + itm_1.checktime.TotalSeconds;


                                    _last_out = true;
                                    counter++;
                                    continue;
                                }
                                //KIRISH
                                if (itm_1.checktype == "I")
                                {

                                    //bu eshigdi tagida turibdi qayta qayta olyapti shunchun torilab qoydik
                                    _last_in_tm = itm_1.checktime;
                                    _last_in = true;

                                }
                                //CHIQISH
                                if (itm_1.checktype == "O")
                                {
                                    if (_last_in)
                                    {
                                        if (_last_in_tm.TotalSeconds < itm_1.checktime.TotalSeconds)
                                        {
                                            secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds);
                                        }
                                        else
                                        {
                                            secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds) + 86400;
                                        }


                                        _last_in = false;

                                        //    Console.WriteLine(itm_1.checktime.ToString() + "  ---------   " + _last_in_tm.ToString());
                                        //    Console.WriteLine(itm_1.checktime.TotalSeconds.ToString() + "  ---------   " + _last_in_tm.TotalSeconds.ToString());

                                    }
                                    else
                                    {
                                        counter++;
                                        continue;
                                    }

                                }
                                counter++;
                            }

                            if (_last_in)
                            {
                                // Console.WriteLine("CHIQSHI YOQ FAQAT KIRISH");
                                // secunds = secunds + (86400 - _last_in_tm.TotalSeconds);

                            }

                            worked_secunds_sm = worked_secunds_sm + secunds;
                            ishlagan_vaqti = secunds.ToString();

                            ishlagan_vaqti_formatted = TimeSpan.FromSeconds(secunds).ToString("hh\\:mm\\:ss");


                            it.ishlagan_vaqtlar_ls.Add(ishlagan_vaqti_formatted);

                            if (it.vaqt_flag != 0 && it.vaqt_flag != 1 && it.vaqt_flag != 3) {
                                if (secunds == 0)
                                {
                                    var start = dt.Date.AddHours(4);
                                    var end   = start.AddDays(1);

                                    var hasKirishYokiChiqish = (await _context.SkudMyCheckinout
                                        .Where(x => x.userid == it.userid && 
                                                    (x.checktype == "I" || x.checktype == "O"))
                                        .Select(x => new { 
                                            x.userid, 
                                            x.checktype, 
                                            FullDate = x.sana.Add(x.checktime)   // EF Core buni ko‘tarolmasa, keyin xotirada hisoblaymiz
                                        })
                                        .ToListAsync())
                                        .Any(x => x.FullDate >= start && x.FullDate <= end);

                                    if (hasKirishYokiChiqish)
                                    {
                                        it.vaqt_flag = 3; // Kirish yoki chiqish bor, lekin ishlagan vaqt 0
                                        
                                    }
                                }
                                else if ((secunds / 3600) <= 7 && (secunds / 3600) > 0)
                                {
                                    it.vaqt_flag = 0; // kam ishlagan kun
                                   
                                }
                                else if ((secunds / 3600) > 13)
                                {
                                    it.vaqt_flag = 1; // juda ko‘p ishlagan kun
                                    
                                }
                            }

                        }
                        it.ishlagan_vaqti_yangi = ishlagan_vaqti;
                        it.ishlagan_vaqti_yangi_time_format = ishlagan_vaqti_formatted;
                    }

                }

                //hamma vaqtlar boyicha olindi ishlaganlar endi uni secundan soatga otkazish kerak
                it.worked_hours_itm = worked_secunds_sm / 3600;
                it.worked_hours_itm_str = TimeSpan.FromSeconds(worked_secunds_sm).ToString("hh\\:mm\\:ss");

                if (it.skudOylik != null)
                {
                    it.ishlagan_puli = it.worked_hours_itm * it.skudOylik.value;
                }

                // // Shu yerga qo‘shamiz
                // var daysDiff = (end_date.Date - begin_date.Date).TotalDays;
                // if (daysDiff == 0) // 1 kun tanlanganda
                // {
                //     if (it.worked_hours_itm == 0)
                //     {
                //         // Shu kun uchun kirish yoki chiqish yozuvi bo'lganmi?
                //         bool hasKirishYokiChiqish = await _context.SkudMyCheckinout
                //             .AnyAsync(x => x.userid == it.userid &&
                //                         (x.checktype == "K" || x.checktype == "C") &&
                //                         ((x.sana + x.checktime) >= begin_date.Date.AddHours(4) &&
                //                             (x.sana + x.checktime) <= begin_date.Date.AddHours(4).AddDays(1)));

                //         if (hasKirishYokiChiqish)
                //         {
                //             it.vaqt_flag = 3; // Kirish yoki chiqish bor, lekin ishlagan vaqt 0
                //         }
                //         else
                //         {
                //             it.vaqt_flag = null; // yoki hech narsa bermaslik
                //         }
                //     }
                //     if (it.worked_hours_itm <= 7 && it.worked_hours_itm > 0)
                //     {
                //         it.vaqt_flag = 0;
                //     }
                //     else if (it.worked_hours_itm > 13)
                //     {
                //         it.vaqt_flag = 1;
                //     }
                // }
                // else if (daysDiff > 0) // 1 kundan ko‘p
                // {
                //     it.vaqt_flag = 2;
                // }
                
                
                // Xodim eng birinchi kirgan/chiqqan vaqtini topish
                var firstCheck = await _context.SkudMyCheckinout
                    .Where(x => x.userid == it.userid && (x.checktype == "I" || x.checktype == "O"))
                    .OrderBy(x => x.sana)
                    .ThenBy(x => x.checktime)
                    .FirstOrDefaultAsync();

                if (firstCheck != null)
                {
                    DateTime firstDateTime = firstCheck.sana.Add(firstCheck.checktime);
                    TimeSpan diff = DateTime.Now - firstDateTime;

                    int years = diff.Days / 365;
                    int days = diff.Days % 365;
                    int hours = diff.Hours;

                    it.kelganidan_beri = $"{years} yil {days} kun {hours} soat";
                }
                else
                {
                    it.kelganidan_beri = $"{0} yil {0} kun {0} soat";
                }
               // Console.WriteLine("FINISH");

               // SkudUserNote jadvali mavjudligini tekshiramiz (agar jadval mavjud bo'lmasa, xatolikni boshqaramiz)
               bool hasUserNote = false;
               try
               {
                   hasUserNote = await _context.SkudUserNote
                                .AnyAsync(x => x.userid == it.userid);
               }
               catch (PostgresException ex) when (ex.SqlState == "42P01") // Jadval mavjud emas
               {
                   // Jadval mavjud emas, hasUserNote = false qoldiramiz
                   Console.WriteLine($"⚠️ SkudUserNote jadvali mavjud emas: {ex.Message}");
               }
               catch (Exception ex)
               {
                   // Boshqa xatoliklar
                   Console.WriteLine($"⚠️ SkudUserNote tekshirishda xatolik: {ex.Message}");
               }

                if(hasUserNote){
                    it.user_note = 1;
                }


            }


            paginationModel.items_list = JArray.FromObject(
                itemList
                .OrderBy(it => it.vaqt_flag == 3 ? 0
                   : it.vaqt_flag == 0 ? 1
                   : it.vaqt_flag == 1 ? 2
                   : 3)
                );
            paginationModel.items_count = await _context.SkudMyUserinfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpPost("replaceMyCheckinoutIoByRange")]
        public async Task<ActionResult<ReplaceIoRangeResponse>> replaceMyCheckinoutIoByRange([FromBody] ReplaceIoRangeRequest req)
        {
            if (req == null)
            {
                return BadRequest("Request body is required.");
            }

            if (req.user_id <= 0)
            {
                return BadRequest("user_id is required.");
            }

            if (req.last_out_datetime <= req.first_in_datetime)
            {
                return BadRequest("last_out_datetime must be greater than first_in_datetime.");
            }

            DateTime rangeStart = req.first_in_datetime;
            DateTime rangeEnd = req.last_out_datetime;

            // We load a slightly wider date window then filter by full datetime in memory.
            DateTime dateFrom = rangeStart.Date.AddDays(-1);
            DateTime dateTo = rangeEnd.Date.AddDays(1);

            using var trx = await _context.Database.BeginTransactionAsync();
            try
            {
                List<SkudMyCheckinout> existing = await _context.SkudMyCheckinout
                    .Where(x => x.userid == req.user_id
                                && (x.checktype == "I" || x.checktype == "O")
                                && x.sana >= dateFrom
                                && x.sana <= dateTo)
                    .ToListAsync();

                List<SkudMyCheckinout> toDelete = existing
                    .Where(x =>
                    {
                        DateTime full = x.sana.Add(x.checktime);
                        return full >= rangeStart && full <= rangeEnd;
                    })
                    .ToList();

                if (toDelete.Count > 0)
                {
                    _context.SkudMyCheckinout.RemoveRange(toDelete);
                }

                string doorName = string.IsNullOrWhiteSpace(req.door_name) ? "Manual edit" : req.door_name;

                SkudMyCheckinout firstIn = new SkudMyCheckinout
                {
                    userid = req.user_id,
                    sana = req.first_in_datetime.Date,
                    checktime = req.first_in_datetime.TimeOfDay,
                    checktype = "I",
                    door_name = doorName,
                    status = false
                };

                SkudMyCheckinout lastOut = new SkudMyCheckinout
                {
                    userid = req.user_id,
                    sana = req.last_out_datetime.Date,
                    checktime = req.last_out_datetime.TimeOfDay,
                    checktype = "O",
                    door_name = doorName,
                    status = false
                };

                _context.SkudMyCheckinout.Add(firstIn);
                _context.SkudMyCheckinout.Add(lastOut);

                await _context.SaveChangesAsync();
                await trx.CommitAsync();

                return Ok(new ReplaceIoRangeResponse
                {
                    success = true,
                    deleted_count = toDelete.Count,
                    user_id = req.user_id,
                    first_in_datetime = req.first_in_datetime,
                    last_out_datetime = req.last_out_datetime
                });
            }
            catch (Exception ex)
            {
                await trx.RollbackAsync();
                return BadRequest("Failed to replace checkinout range: " + ex.Message);
            }
        }





        [HttpPost("replaceMyCheckinoutIoByRangeQorovulxona")]
        public async Task<ActionResult<ReplaceIoRangeResponse>> replaceMyCheckinoutIoByRangeQorovulxona([FromBody] ReplaceIoRangeRequest req)
        {
            if (req == null)
            {
                return BadRequest("Request body is required.");
            }

            if (req.user_id <= 0)
            {
                return BadRequest("user_id is required.");
            }

            if (req.last_out_datetime <= req.first_in_datetime)
            {
                return BadRequest("last_out_datetime must be greater than first_in_datetime.");
            }

            DateTime rangeStart = req.first_in_datetime;
            DateTime rangeEnd = req.last_out_datetime;

            // We load a slightly wider date window then filter by full datetime in memory.
            DateTime dateFrom = rangeStart.Date.AddDays(-1);
            DateTime dateTo = rangeEnd.Date.AddDays(1);

            using var trx = await _context.Database.BeginTransactionAsync();
            try
            {
                List<SkudMyCheckinout> existing = await _context.SkudMyCheckinout
                    .Where(x => x.userid == req.user_id
                                && (x.checktype == "K" || x.checktype == "C")
                                && x.sana >= dateFrom
                                && x.sana <= dateTo)
                    .ToListAsync();

                List<SkudMyCheckinout> toDelete = existing
                    .Where(x =>
                    {
                        DateTime full = x.sana.Add(x.checktime);
                        return full >= rangeStart && full <= rangeEnd;
                    })
                    .ToList();

                if (toDelete.Count > 0)
                {
                    _context.SkudMyCheckinout.RemoveRange(toDelete);
                }

                string doorName = string.IsNullOrWhiteSpace(req.door_name) ? "Manual edit" : req.door_name;

                SkudMyCheckinout firstIn = new SkudMyCheckinout
                {
                    userid = req.user_id,
                    sana = req.first_in_datetime.Date,
                    checktime = req.first_in_datetime.TimeOfDay,
                    checktype = "K",
                    door_name = doorName,
                    status = false
                };

                SkudMyCheckinout lastOut = new SkudMyCheckinout
                {
                    userid = req.user_id,
                    sana = req.last_out_datetime.Date,
                    checktime = req.last_out_datetime.TimeOfDay,
                    checktype = "C",
                    door_name = doorName,
                    status = false
                };

                _context.SkudMyCheckinout.Add(firstIn);
                _context.SkudMyCheckinout.Add(lastOut);

                await _context.SaveChangesAsync();
                await trx.CommitAsync();

                return Ok(new ReplaceIoRangeResponse
                {
                    success = true,
                    deleted_count = toDelete.Count,
                    user_id = req.user_id,
                    first_in_datetime = req.first_in_datetime,
                    last_out_datetime = req.last_out_datetime
                });
            }
            catch (Exception ex)
            {
                await trx.RollbackAsync();
                return BadRequest("Failed to replace checkinout range: " + ex.Message);
            }
        }



        [HttpGet("getReportWithoutSmenaPaginationByAllUserAndOylik0404WithUserId")]
        public async Task<ActionResult<TexPaginationModel>> getReportWithoutSmenaPaginationByAllUserAndOylik0404WithUserId([FromQuery] int page,
[FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long user_id)
        {
           
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .OrderByDescending(p => p.badgenumber)
                .Where(p=>p.userid == user_id)
                .Skip(size * page).Take(size)
                .ToListAsync();

          
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }

            foreach (SkudMyUserinfo it in itemList)
            {
                if (it == null)
                {
                    continue;
                }

                double worked_secunds_sm = 0.0;
                for (var dt = begin_date; dt <= end_date; dt = dt.AddDays(1))
                {

                    String date_only = dt.Date.ToString("yyyy-MM-dd");
                    Console.WriteLine(date_only);
                    it.kun = date_only;

                    if (it != null)
                    {
                        if (it.ishlagan_vaqtlar_ls == null)
                        {
                            it.ishlagan_vaqtlar_ls = new List<string>();
                        }
                        //Console.WriteLine("KIRDI");
                        //osha kundagi kirdi chiqdilardi olndi
                        List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .FromSqlRaw("SELECT code, userid, sana, checktime, checktype, door_name, status,''::text as begona" +
                                "\r\nFROM public.my_checkinout" +
                                "\r\nWHERE (sana+checktime)   >= '"+ dt.Date.AddHours(4).ToString("yyyy-MM-ddTHH:mm:ss") 
                                + "' and (sana+checktime) <= '"+ dt.Date.AddHours(4).AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss") + "'" +
                                " \r\nand checktype != 'K' and checktype != 'C'" +
                                "\r\nAND userid = "+user_id+"" +
                                "\r\nORDER BY sana , checktime")
                        .ToListAsync();

                        /*List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .Where(p => p.userid == it.userid && (p.checktype != "K" && p.checktype != "C") && ((p.sana + p.checktime) >= dt.Date.AddHours(4) && (p.sana + p.checktime) <= dt.Date.AddHours(4).AddDays(1)))
                        .OrderBy(p => p.sana)
                        .OrderBy(p => p.checktime)
                        .Skip(size * page).Take(size)
                        .ToListAsync();*/
                        Console.WriteLine("TIME: "+dt.Date.AddHours(4) + " VA "+ dt.Date.AddHours(4).AddDays(1));
                        String ishlagan_vaqti = "0";
                        String ishlagan_vaqti_formatted = "0";
                       // Console.WriteLine("KIRDI 11111");
                        //agar natija bolsa chiqaradi bolmasa 0 yozib qoyamiz
                        if (checkinouts != null && checkinouts.Count() > 0)
                        {
                            Console.WriteLine(checkinouts.Count());

                            int counter = 0;
                            double secunds = 0;
                            bool _last_out = false;
                            bool _last_in = false;
                            TimeSpan _last_in_tm = TimeSpan.FromSeconds(0);
                            TimeSpan _last_out_tm = TimeSpan.FromSeconds(0);
                            foreach (SkudMyCheckinout itm_1 in checkinouts)
                            {

                               // Console.WriteLine("KIRDI 22222");
                                //KECHASI ISHLADI BUNDA
                                if (counter == 0 && itm_1.checktype == "O")
                                {
                                    Console.WriteLine("Birinchi chiqish boldi qoshmadik sekund");
                                    //secunds = secunds + itm_1.checktime.TotalSeconds;
                                    

                                    _last_out = true;
                                    counter++;
                                    continue;
                                }
                                //KIRISH
                                if (itm_1.checktype == "I")
                                {

                                    //bu eshigdi tagida turibdi qayta qayta olyapti shunchun torilab qoydik
                                    _last_in_tm = itm_1.checktime;
                                    _last_in = true;

                                }
                                //CHIQISH
                                if (itm_1.checktype == "O")
                                {
                                    if (_last_in)
                                    {
                                        if (_last_in_tm.TotalSeconds < itm_1.checktime.TotalSeconds)
                                        {
                                            secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds);
                                        }
                                        else {
                                            secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds) + 86400;
                                        }
                                        

                                        _last_in = false;

                                        Console.WriteLine(itm_1.checktime.ToString() + "  ---------   "+ _last_in_tm.ToString());
                                        Console.WriteLine(itm_1.checktime.TotalSeconds.ToString() + "  ---------   "+ _last_in_tm.TotalSeconds.ToString());

                                    }
                                    else
                                    {
                                        counter++;
                                        continue;
                                    }

                                }



                                counter++;
                            }
                            
                            if (_last_in)
                            {
                                Console.WriteLine("CHIQSHI YOQ FAQAT KIRISH");
                               // secunds = secunds + (86400 - _last_in_tm.TotalSeconds);

                            }
                            
                            worked_secunds_sm = worked_secunds_sm + secunds;
                            ishlagan_vaqti = secunds.ToString();

                            ishlagan_vaqti_formatted = TimeSpan.FromSeconds(secunds).ToString("hh\\:mm\\:ss");


                            it.ishlagan_vaqtlar_ls.Add(ishlagan_vaqti_formatted);
                            
                        }
                        it.ishlagan_vaqti_yangi = ishlagan_vaqti;
                        it.ishlagan_vaqti_yangi_time_format = ishlagan_vaqti_formatted;
                        



                    }

                }

                //hamma vaqtlar boyicha olindi ishlaganlar endi uni secundan soatga otkazish kerak
                it.worked_hours_itm = worked_secunds_sm / 3600;
                it.worked_hours_itm_str = TimeSpan.FromSeconds(worked_secunds_sm).ToString("hh\\:mm\\:ss");
                if (it.skudOylik != null)
                {
                    it.ishlagan_puli = it.worked_hours_itm * it.skudOylik.value;
                }

                Console.WriteLine("FINISH");

            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        [HttpGet("getReportWithoutSmenaPaginationByAllUserAndOylik0404QarovulWithUserId")]
        public async Task<ActionResult<TexPaginationModel>> getReportWithoutSmenaPaginationByAllUserAndOylik0404QarovulWithUserId([FromQuery] int page,
        [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long user_id)
        {
           
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .OrderByDescending(p => p.badgenumber)
                .Where(p=>p.userid == user_id)
                .Skip(size * page).Take(size)
                .ToListAsync();

          
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }

            foreach (SkudMyUserinfo it in itemList)
            {
                if (it == null)
                {
                    continue;
                }

                double worked_secunds_sm = 0.0;
                for (var dt = begin_date; dt <= end_date; dt = dt.AddDays(1))
                {

                    String date_only = dt.Date.ToString("yyyy-MM-dd");
                    Console.WriteLine(date_only);
                    it.kun = date_only;

                    if (it != null)
                    {
                        if (it.ishlagan_vaqtlar_ls == null)
                        {
                            it.ishlagan_vaqtlar_ls = new List<string>();
                        }
                        //Console.WriteLine("KIRDI");
                        //osha kundagi kirdi chiqdilardi olndi
                        List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .FromSqlRaw("SELECT code, userid, sana, checktime, checktype, door_name, status,''::text as begona" +
                                "\r\nFROM public.my_checkinout" +
                                "\r\nWHERE (sana+checktime)   >= '"+ dt.Date.AddHours(4).ToString("yyyy-MM-ddTHH:mm:ss") 
                                + "' and (sana+checktime) <= '"+ dt.Date.AddHours(4).AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss") + "'" +
                                " \r\nand checktype != 'I' and checktype != 'O'" +
                                "\r\nAND userid = "+user_id+"" +
                                "\r\nORDER BY sana , checktime")
                        .ToListAsync();

                        /*List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .Where(p => p.userid == it.userid && (p.checktype != "K" && p.checktype != "C") && ((p.sana + p.checktime) >= dt.Date.AddHours(4) && (p.sana + p.checktime) <= dt.Date.AddHours(4).AddDays(1)))
                        .OrderBy(p => p.sana)
                        .OrderBy(p => p.checktime)
                        .Skip(size * page).Take(size)
                        .ToListAsync();*/
                        Console.WriteLine("TIME: "+dt.Date.AddHours(4) + " VA "+ dt.Date.AddHours(4).AddDays(1));
                        String ishlagan_vaqti = "0";
                        String ishlagan_vaqti_formatted = "0";
                       // Console.WriteLine("KIRDI 11111");
                        //agar natija bolsa chiqaradi bolmasa 0 yozib qoyamiz
                        if (checkinouts != null && checkinouts.Count() > 0)
                        {
                            Console.WriteLine(checkinouts.Count());

                            int counter = 0;
                            double secunds = 0;
                            bool _last_out = false;
                            bool _last_in = false;
                            TimeSpan _last_in_tm = TimeSpan.FromSeconds(0);
                            TimeSpan _last_out_tm = TimeSpan.FromSeconds(0);
                            foreach (SkudMyCheckinout itm_1 in checkinouts)
                            {

                               // Console.WriteLine("KIRDI 22222");
                                //KECHASI ISHLADI BUNDA
                                if (counter == 0 && itm_1.checktype == "C")
                                {
                                    Console.WriteLine("Birinchi chiqish boldi qoshmadik sekund");
                                    //secunds = secunds + itm_1.checktime.TotalSeconds;
                                    

                                    _last_out = true;
                                    counter++;
                                    continue;
                                }
                                //KIRISH
                                if (itm_1.checktype == "K")
                                {

                                    //bu eshigdi tagida turibdi qayta qayta olyapti shunchun torilab qoydik
                                    _last_in_tm = itm_1.checktime;
                                    _last_in = true;

                                }
                                //CHIQISH
                                if (itm_1.checktype == "C")
                                {
                                    if (_last_in)
                                    {
                                        if (_last_in_tm.TotalSeconds < itm_1.checktime.TotalSeconds)
                                        {
                                            secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds);
                                        }
                                        else {
                                            secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds) + 86400;
                                        }
                                        

                                        _last_in = false;

                                        Console.WriteLine(itm_1.checktime.ToString() + "  ---------   "+ _last_in_tm.ToString());
                                        Console.WriteLine(itm_1.checktime.TotalSeconds.ToString() + "  ---------   "+ _last_in_tm.TotalSeconds.ToString());

                                    }
                                    else
                                    {
                                        counter++;
                                        continue;
                                    }

                                }



                                counter++;
                            }
                            
                            if (_last_in)
                            {
                                Console.WriteLine("CHIQSHI YOQ FAQAT KIRISH");
                               // secunds = secunds + (86400 - _last_in_tm.TotalSeconds);

                            }
                            
                            worked_secunds_sm = worked_secunds_sm + secunds;
                            ishlagan_vaqti = secunds.ToString();

                            ishlagan_vaqti_formatted = TimeSpan.FromSeconds(secunds).ToString("hh\\:mm\\:ss");


                            it.ishlagan_vaqtlar_ls.Add(ishlagan_vaqti_formatted);
                            
                        }
                        it.ishlagan_vaqti_yangi = ishlagan_vaqti;
                        it.ishlagan_vaqti_yangi_time_format = ishlagan_vaqti_formatted;
                        



                    }

                }

                //hamma vaqtlar boyicha olindi ishlaganlar endi uni secundan soatga otkazish kerak
                it.worked_hours_itm = worked_secunds_sm / 3600;
                it.worked_hours_itm_str = TimeSpan.FromSeconds(worked_secunds_sm).ToString("hh\\:mm\\:ss");
                if (it.skudOylik != null)
                {
                    it.ishlagan_puli = it.worked_hours_itm * it.skudOylik.value;
                }

                Console.WriteLine("FINISH");

            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }


        [HttpGet("getReportWithoutSmenaPaginationByAllUserAndOylik0404IchkiQarovulWithUserId")]
        public async Task<ActionResult<TexPaginationModel>> getReportWithoutSmenaPaginationByAllUserAndOylik0404IchkiQarovulWithUserId([FromQuery] int page,
        [FromQuery] int size, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date, [FromQuery] long user_id)
        {
           
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .OrderByDescending(p => p.badgenumber)
                .Where(p=>p.userid == user_id)
                .Skip(size * page).Take(size)
                .ToListAsync();

          
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }

            foreach (SkudMyUserinfo it in itemList)
            {
                if (it == null)
                {
                    continue;
                }

                double worked_secunds_sm = 0.0;
                for (var dt = begin_date; dt <= end_date; dt = dt.AddDays(1))
                {

                    String date_only = dt.Date.ToString("yyyy-MM-dd");
                    Console.WriteLine(date_only);
                    it.kun = date_only;

                    if (it != null)
                    {
                        if (it.ishlagan_vaqtlar_ls == null)
                        {
                            it.ishlagan_vaqtlar_ls = new List<string>();
                        }
                        //Console.WriteLine("KIRDI");
                        //osha kundagi kirdi chiqdilardi olndi
                        List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .FromSqlRaw("SELECT code, userid, sana, checktime, checktype, door_name, status,''::text as begona" +
                                "\r\nFROM public.my_checkinout" +
                                "\r\nWHERE (sana+checktime)   >= '"+ dt.Date.AddHours(4).ToString("yyyy-MM-ddTHH:mm:ss") 
                                + "' and (sana+checktime) <= '"+ dt.Date.AddHours(4).AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss") + "'" +
                                " \r\nand checktype != 'K' and checktype != 'C'" +
                                "\r\nAND userid = "+user_id+"" +
                                "\r\nORDER BY sana , checktime")
                        .ToListAsync();

                        /*List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .Where(p => p.userid == it.userid && (p.checktype != "K" && p.checktype != "C") && ((p.sana + p.checktime) >= dt.Date.AddHours(4) && (p.sana + p.checktime) <= dt.Date.AddHours(4).AddDays(1)))
                        .OrderBy(p => p.sana)
                        .OrderBy(p => p.checktime)
                        .Skip(size * page).Take(size)
                        .ToListAsync();*/
                        Console.WriteLine("TIME: "+dt.Date.AddHours(4) + " VA "+ dt.Date.AddHours(4).AddDays(1));
                        String ishlagan_vaqti = "0";
                        String ishlagan_vaqti_formatted = "0";
                       // Console.WriteLine("KIRDI 11111");
                        //agar natija bolsa chiqaradi bolmasa 0 yozib qoyamiz
                        if (checkinouts != null && checkinouts.Count() > 0)
                        {
                            Console.WriteLine(checkinouts.Count());

                            int counter = 0;
                            double secunds = 0;
                            bool _last_out = false;
                            bool _last_in = false;
                            TimeSpan _last_in_tm = TimeSpan.FromSeconds(0);
                            TimeSpan _last_out_tm = TimeSpan.FromSeconds(0);
                            foreach (SkudMyCheckinout itm_1 in checkinouts)
                            {

                               // Console.WriteLine("KIRDI 22222");
                                //KECHASI ISHLADI BUNDA
                                if (counter == 0 && itm_1.checktype == "O")
                                {
                                    Console.WriteLine("Birinchi chiqish boldi qoshmadik sekund");
                                    //secunds = secunds + itm_1.checktime.TotalSeconds;
                                    

                                    _last_out = true;
                                    counter++;
                                    continue;
                                }
                                //KIRISH
                                if (itm_1.checktype == "I")
                                {

                                    //bu eshigdi tagida turibdi qayta qayta olyapti shunchun torilab qoydik
                                    _last_in_tm = itm_1.checktime;
                                    _last_in = true;

                                }
                                //CHIQISH
                                if (itm_1.checktype == "O")
                                {
                                    if (_last_in)
                                    {
                                        if (_last_in_tm.TotalSeconds < itm_1.checktime.TotalSeconds)
                                        {
                                            secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds);
                                        }
                                        else {
                                            secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds) + 86400;
                                        }
                                        

                                        _last_in = false;

                                        Console.WriteLine(itm_1.checktime.ToString() + "  ---------   "+ _last_in_tm.ToString());
                                        Console.WriteLine(itm_1.checktime.TotalSeconds.ToString() + "  ---------   "+ _last_in_tm.TotalSeconds.ToString());

                                    }
                                    else
                                    {
                                        counter++;
                                        continue;
                                    }

                                }



                                counter++;
                            }
                            
                            if (_last_in)
                            {
                                Console.WriteLine("CHIQSHI YOQ FAQAT KIRISH");
                               // secunds = secunds + (86400 - _last_in_tm.TotalSeconds);

                            }
                            
                            worked_secunds_sm = worked_secunds_sm + secunds;
                            ishlagan_vaqti = secunds.ToString();

                            ishlagan_vaqti_formatted = TimeSpan.FromSeconds(secunds).ToString("hh\\:mm\\:ss");


                            it.ishlagan_vaqtlar_ls.Add(ishlagan_vaqti_formatted);
                            
                        }
                        it.ishlagan_vaqti_yangi = ishlagan_vaqti;
                        it.ishlagan_vaqti_yangi_time_format = ishlagan_vaqti_formatted;
                        



                    }

                }

                //hamma vaqtlar boyicha olindi ishlaganlar endi uni secundan soatga otkazish kerak
                it.worked_hours_itm = worked_secunds_sm / 3600;
                it.worked_hours_itm_str = TimeSpan.FromSeconds(worked_secunds_sm).ToString("hh\\:mm\\:ss");
                if (it.skudOylik != null)
                {
                    it.ishlagan_puli = it.worked_hours_itm * it.skudOylik.value;
                }

                Console.WriteLine("FINISH");

            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationBy0404AllUsers")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationBy0404AllUsers([FromQuery] int page,
    [FromQuery] int size, [FromQuery] DateTime date_b)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyCheckinout> itemList = await _context.SkudMyCheckinout
                .FromSqlRaw("SELECT code, userid, sana, checktime, checktype, door_name, status,'' as begona" +
                "\r\nFROM public.my_checkinout" +
                "\r\nWHERE (sana+checktime)>= '"+ date_b.Date.AddHours(4).ToString("yyyy-MM-ddTHH:mm:ss") + "' and (sana+checktime) <= '"+ date_b.Date.AddHours(4).AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss") + "' " +
                "\r\nORDER BY userid,sana , checktime")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyCheckinout>();
            }

            foreach (SkudMyCheckinout itm in itemList)
            {
                itm.userinfo = await _context.SkudMyUserinfo.FindAsync(itm.userid);

            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyCheckinout.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationBy0404AllUsersOnlyOne")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationBy0404AllUsersOnlyOne([FromQuery] int page,
[FromQuery] int size, [FromQuery] DateTime date_b)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyCheckinout> itemList = await _context.SkudMyCheckinout
                .FromSqlRaw("SELECT code, userid, sana, checktime, checktype, door_name, status,'' as begona" +
                "\r\nFROM public.my_checkinout" +
                "\r\nWHERE (sana+checktime)>= '" + date_b.Date.AddHours(4).ToString("yyyy-MM-ddTHH:mm:ss") + "' and (sana+checktime) <= '" + date_b.Date.AddHours(4).AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss") + "' " +
                "\r\nORDER BY userid,sana , checktime")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyCheckinout>();
            }
            List<SkudMyCheckinout> itemNeList = new List<SkudMyCheckinout>();
            long last_ser_id = 0;
            foreach (SkudMyCheckinout itm in itemList) {
                if (itm.userid != last_ser_id) {
                    itemNeList.Add(itm);
                    last_ser_id = itm.userid;
                }
            
            }
            itemList = itemNeList;

            foreach (SkudMyCheckinout itm in itemList)
            {
                itm.userinfo = await _context.SkudMyUserinfo.FindAsync(itm.userid);

            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyCheckinout.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationBy0404")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationBy0404([FromQuery] int page,
            [FromQuery] int size, [FromQuery] DateTime date_b, [FromQuery] long user_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyCheckinout> itemList = await _context.SkudMyCheckinout
                .FromSqlRaw("SELECT code, userid, sana, checktime, checktype, door_name, status,(SELECT  mu.ism\r\nFROM my_userinfo mu" +
                "\r\nWHERE mu.userid = "+ user_id + " ) as begona" +
                "\r\nFROM public.my_checkinout" +
                "\r\nWHERE (sana+checktime)   >= '"+ date_b.Date.AddHours(4).ToString("yyyy-MM-ddTHH:mm:ss") + "' and (sana+checktime) <= '"+ date_b.Date.AddHours(4).AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss") + "' " +
                "\r\nand checktype != 'K' and checktype != 'C'" +
                "\r\nAND userid = "+ user_id + "" +
                "\r\nORDER BY sana , checktime")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyCheckinout>();
            }

            foreach (SkudMyCheckinout itm in itemList)
            {
                itm.userinfo = await _context.SkudMyUserinfo.FindAsync(itm.userid);

            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyCheckinout.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getPaginationBy0404Qarovul")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationBy0404Qarovul([FromQuery] int page,
            [FromQuery] int size, [FromQuery] DateTime date_b, [FromQuery] long user_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyCheckinout> itemList = await _context.SkudMyCheckinout
                .FromSqlRaw("SELECT code, userid, sana, checktime, checktype, door_name, status,(SELECT  mu.ism\r\nFROM my_userinfo mu" +
                "\r\nWHERE mu.userid = "+ user_id + " ) as begona" +
                "\r\nFROM public.my_checkinout" +
                "\r\nWHERE (sana+checktime)   >= '"+ date_b.Date.AddHours(4).ToString("yyyy-MM-ddTHH:mm:ss") + "' and (sana+checktime) <= '"+ date_b.Date.AddHours(4).AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss") + "' " +
                "\r\nand checktype != 'I' and checktype != 'O'" +
                "\r\nAND userid = "+ user_id + "" +
                "\r\nORDER BY sana , checktime")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyCheckinout>();
            }

            foreach (SkudMyCheckinout itm in itemList)
            {
                itm.userinfo = await _context.SkudMyUserinfo.FindAsync(itm.userid);

            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyCheckinout.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }
        [HttpGet("getPaginationBy0404IchkiQarovul")]
        public async Task<ActionResult<TexPaginationModel>> getPaginationBy0404IchkiQarovul([FromQuery] int page,
            [FromQuery] int size, [FromQuery] DateTime date_b, [FromQuery] long user_id)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyCheckinout> itemList = await _context.SkudMyCheckinout
                .FromSqlRaw("SELECT code, userid, sana, checktime, checktype, door_name, status,(SELECT  mu.ism\r\nFROM my_userinfo mu" +
                "\r\nWHERE mu.userid = "+ user_id + " ) as begona" +
                "\r\nFROM public.my_checkinout" +
                "\r\nWHERE (sana+checktime)   >= '"+ date_b.Date.AddHours(4).ToString("yyyy-MM-ddTHH:mm:ss") + "' and (sana+checktime) <= '"+ date_b.Date.AddHours(4).AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss") + "' " +
                "\r\nand checktype != 'K' and checktype != 'C'" +
                "\r\nAND userid = "+ user_id + "" +
                "\r\nORDER BY sana , checktime")
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyCheckinout>();
            }

            foreach (SkudMyCheckinout itm in itemList)
            {
                itm.userinfo = await _context.SkudMyUserinfo.FindAsync(itm.userid);

            }


            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyCheckinout.CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        [HttpGet("getReportWithoutSmenaPaginationByDepartmentIdAndOylik")]
        public async Task<ActionResult<TexPaginationModel>> getReportWithoutSmenaPaginationByDepartmentIdAndOylik([FromQuery] int page,
[FromQuery] int size, [FromQuery] long dep_id, [FromQuery] DateTime begin_date, [FromQuery] DateTime end_date)
        {
            TexPaginationModel paginationModel = new TexPaginationModel();
            List<SkudMyUserinfo> itemList = await _context.SkudMyUserinfo
                .Include(p => p.skudOylik)
                .Where(p => p.departid == dep_id)
                .OrderByDescending(p => p.badgenumber)
                .Skip(size * page).Take(size)
                .ToListAsync();
            if (itemList == null)
            {
                itemList = new List<SkudMyUserinfo>();
            }

            foreach (SkudMyUserinfo it in itemList)
            {
                double worked_secunds_sm = 0.0;
                for (var dt = begin_date; dt <= end_date; dt = dt.AddDays(1))
                {
                    
                    String date_only = dt.Date.ToString("yyyy-MM-dd");
                 
                    it.kun = date_only;

                    if (it != null)
                    {
                        if (it.ishlagan_vaqtlar_ls == null)
                        {
                            it.ishlagan_vaqtlar_ls = new List<string>();
                        }
                        //osha kundagi kirdi chiqdilardi olndi
                        List<SkudMyCheckinout> checkinouts = await _context.SkudMyCheckinout
                                .Where(p => p.sana.Date == dt.Date && p.userid == it.userid && (p.checktype != "K" && p.checktype != "C"))
                        .OrderBy(p => p.checktime)
                        .Skip(size * page).Take(size)
                        .ToListAsync();
                        String ishlagan_vaqti = "0";
                        String ishlagan_vaqti_formatted = "0";
                        //agar natija bolsa chiqaradi bolmasa 0 yozib qoyamiz
                        if (checkinouts != null && checkinouts.Count() > 0)
                        {
                            int counter = 0;
                            double secunds = 0;
                            bool _last_out = false;
                            bool _last_in = false;
                            TimeSpan _last_in_tm = TimeSpan.FromSeconds(0);
                            TimeSpan _last_out_tm = TimeSpan.FromSeconds(0);
                            foreach (SkudMyCheckinout itm_1 in checkinouts)
                            {
                                //KECHASI ISHLADI BUNDA
                                if (counter == 0 && itm_1.checktype == "O")
                                {
                                    secunds = secunds + itm_1.checktime.TotalSeconds;
                                    _last_out = true;
                                    counter++;
                                    continue;
                                }
                                //KIRISH
                                if (itm_1.checktype == "I")
                                {

                                    //bu eshigdi tagida turibdi qayta qayta olyapti shunchun torilab qoydik
                                    _last_in_tm = itm_1.checktime;
                                    _last_in = true;

                                }
                                //CHIQISH
                                if (itm_1.checktype == "O")
                                {
                                    if (_last_in)
                                    {
                                        secunds = secunds + (itm_1.checktime.TotalSeconds - _last_in_tm.TotalSeconds);
                                        _last_in = false;
                                    }
                                    else
                                    {
                                        counter++;
                                        continue;
                                    }

                                }



                                counter++;
                            }

                            if (_last_in)
                            {
                                secunds = secunds + (86400 - _last_in_tm.TotalSeconds);
                            }

                            ishlagan_vaqti = secunds.ToString();
                            worked_secunds_sm = worked_secunds_sm + secunds;
                            ishlagan_vaqti_formatted = TimeSpan.FromSeconds(secunds).ToString("hh\\:mm\\:ss");

                        }
                        it.ishlagan_vaqti_yangi = ishlagan_vaqti;
                        it.ishlagan_vaqti_yangi_time_format = ishlagan_vaqti_formatted;
                        it.ishlagan_vaqtlar_ls.Add(ishlagan_vaqti_formatted);



                    }

                    /*

                    List<SkudVaqtTemp> result = await _context.SkudVaqtTemp.FromSqlRaw("select TO_CHAR(working_time_without_gr,'HH24:MI:SS') as vaqt from working_time_without_gr(" + it.userid + ",'" + date_only + "')").ToListAsync();
                    if (result != null && result.Count > 0)
                    {
                        String ishlagan_wq = result[0].vaqt;
                       
                        if (it.ishlagan_vaqtlar_ls == null) {
                            it.ishlagan_vaqtlar_ls = new List<string>();
                        }
                        if (ishlagan_wq == null || ishlagan_wq.Trim().Length == 0) {
                            continue;
                        }
                        it.ishlagan_vaqtlar_ls.Add(ishlagan_wq);
                    
                         if (ishlagan_wq.Trim().Length > 0) {
                              string[] vaqtlar = ishlagan_wq.Trim().Split(":");
                              if (vaqtlar.Length > 0) {
                                  int counter = 0;
                                  foreach (string vaqt in vaqtlar) {
                                      if (vaqt.Trim().Length > 0) {


                                      if (counter == 0)
                                      {
                                          //hour

                                          int hour = 0;

                                          if (Int32.TryParse(vaqt, out hour))
                                          {
                                              worked_secunds_sm = worked_secunds_sm + (hour * 3600);

                                          }

                                      }
                                      else if (counter == 1)
                                      {
                                          //minutes

                                          int minutes = 0;

                                          if (Int32.TryParse(vaqt, out minutes))
                                          {
                                              worked_secunds_sm = worked_secunds_sm + (minutes * 60);


                                          }

                                      }
                                      else if (counter == 2) {
                                          //secunds
                                          int secunds = 0;

                                          if (Int32.TryParse(vaqt, out secunds))
                                          {
                                              worked_secunds_sm = worked_secunds_sm + secunds;


                                          }

                                      }
                                      }


                                      counter++;
                                  }
                              }


                          }

                    }*/

                }

                //hamma vaqtlar boyicha olindi ishlaganlar endi uni secundan soatga otkazish kerak
                it.worked_hours_itm = worked_secunds_sm / 3600;
                if (it.skudOylik != null) {
                    it.ishlagan_puli = it.worked_hours_itm * it.skudOylik.value;
                }

            }
            paginationModel.items_list = JArray.FromObject(itemList);
            paginationModel.items_count = await _context.SkudMyUserinfo.Where(p => p.departid == dep_id).CountAsync();
            paginationModel.current_item_count = itemList.Count();
            paginationModel.current_page = page;
            return paginationModel;
        }

        // PUT: api/SkudMyUserinfoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkudMyUserinfo(long id, SkudMyUserinfo skudMyUserinfo)
        {
            if (id != skudMyUserinfo.badgenumber)
            {
                return BadRequest();
            }

            _context.Entry(skudMyUserinfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkudMyUserinfoExists(id))
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

        // Rasmni face device talablariga moslab qayta ishlash
        private Task<byte[]> ProcessImageForFaceDeviceAsync(byte[] imageBytes)
        {
            try
            {
                // System.Drawing ishlatamiz (OpenCvSharp4 o'rniga, chunki u murakkab)
                using (var ms = new System.IO.MemoryStream(imageBytes))
                {
                    using (var originalImage = System.Drawing.Image.FromStream(ms))
                    {
                        // DS-K1T341CMF uchun tavsiya etilgan rezolyutsiya: 640x480
                        // Ba'zi firmware'larda 640x480 aniq talab qilinadi
                        int minWidth = 640;
                        int minHeight = 480;
                        int maxWidth = 640; // DS-K1T341CMF uchun 640x480 optimal
                        int maxHeight = 480;
                        
                        int targetWidth = originalImage.Width;
                        int targetHeight = originalImage.Height;
                        
                        // Agar rasm kichik bo'lsa, kattalashtiramiz
                        if (targetWidth < minWidth || targetHeight < minHeight)
                        {
                            double scaleX = (double)minWidth / targetWidth;
                            double scaleY = (double)minHeight / targetHeight;
                            double scale = Math.Max(scaleX, scaleY);
                            
                            targetWidth = (int)(targetWidth * scale);
                            targetHeight = (int)(targetHeight * scale);
                        }
                        
                        // Agar rasm juda katta bo'lsa, kichiklashtiramiz (Base64 uzunligini cheklash uchun)
                        if (targetWidth > maxWidth || targetHeight > maxHeight)
                        {
                            double scaleX = (double)maxWidth / targetWidth;
                            double scaleY = (double)maxHeight / targetHeight;
                            double scale = Math.Min(scaleX, scaleY);
                            
                            targetWidth = (int)(targetWidth * scale);
                            targetHeight = (int)(targetHeight * scale);
                            
                            // Minimal o'lchamni saqlash
                            if (targetWidth < minWidth) targetWidth = minWidth;
                            if (targetHeight < minHeight) targetHeight = minHeight;
                        }
                        
                        // Rasmni qayta o'lchamlash
                        using (var resizedImage = new System.Drawing.Bitmap(targetWidth, targetHeight))
                        {
                            using (var graphics = System.Drawing.Graphics.FromImage(resizedImage))
                            {
                                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                                
                                // Oq fon qo'shish
                                graphics.Clear(System.Drawing.Color.White);
                                
                                // Rasmni markazga joylashtirish
                                graphics.DrawImage(originalImage, 0, 0, targetWidth, targetHeight);
                            }
                            
                            // JPEG sifatida saqlash va siqish (60-75 KB - Base64 uzunligi 100K dan kam bo'lishi uchun)
                            using (var outputMs = new System.IO.MemoryStream())
                            {
                                var jpegCodec = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()
                                    .FirstOrDefault(c => c.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
                                
                                var encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                                
                                // DS-K1T341CMF uchun Base64 uzunligi 50KB dan kam bo'lishi tavsiya etiladi
                                // (Base64 = binary * 1.33, shuning uchun 37 KB * 1.33 = ~50KB)
                                int targetMaxSize = 37 * 1024; // 37 KB (Base64: ~50KB - DS-K1T341CMF tavsiyasi)
                                int minSize = 30 * 1024; // 30 KB (minimal)
                                
                                // Quality'ni boshlang'ich qiymatdan boshlaymiz (70%)
                                long quality = 70;
                                byte[] resultBytes = null;
                                
                                // Rasmni siqish - quality'ni pasaytirib, 75 KB dan kam qilamiz
                                do
                                {
                                    outputMs.SetLength(0);
                                    encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(
                                        System.Drawing.Imaging.Encoder.Quality, quality);
                                    resizedImage.Save(outputMs, jpegCodec, encoderParams);
                                    resultBytes = outputMs.ToArray();
                                    
                                    Console.WriteLine($"   Quality: {quality}%, Hajm: {resultBytes.Length / 1024} KB, Base64: ~{resultBytes.Length * 133 / 1000}K");
                                    
                                    // Agar hajm 37 KB dan katta bo'lsa, quality'ni pasaytiramiz (DS-K1T341CMF uchun 50KB Base64 limit)
                                    if (resultBytes.Length > targetMaxSize && quality > 40)
                                    {
                                        quality -= 5;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                } while (resultBytes.Length > targetMaxSize && quality >= 40);
                                
                                // Agar hali ham katta bo'lsa, o'lchamni kamaytiramiz
                                if (resultBytes.Length > targetMaxSize)
                                {
                                    double resizeFactor = Math.Sqrt((double)targetMaxSize / resultBytes.Length);
                                    int newWidth = (int)(targetWidth * resizeFactor);
                                    int newHeight = (int)(targetHeight * resizeFactor);
                                    
                                    // Minimal o'lchamni saqlash
                                    if (newWidth < minWidth) newWidth = minWidth;
                                    if (newHeight < minHeight) newHeight = minHeight;
                                    
                                    using (var finalImage = new System.Drawing.Bitmap(newWidth, newHeight))
                                    {
                                        using (var graphics = System.Drawing.Graphics.FromImage(finalImage))
                                        {
                                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                                            graphics.Clear(System.Drawing.Color.White);
                                            graphics.DrawImage(resizedImage, 0, 0, newWidth, newHeight);
                                        }
                                        
                                        // Quality'ni 60% ga o'rnatamiz
                                        outputMs.SetLength(0);
                                        encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(
                                            System.Drawing.Imaging.Encoder.Quality, 60L);
                                        finalImage.Save(outputMs, jpegCodec, encoderParams);
                                        resultBytes = outputMs.ToArray();
                                        
                                        // Agar hali ham katta bo'lsa, quality'ni yanada pasaytiramiz
                                        if (resultBytes.Length > targetMaxSize)
                                        {
                                            for (quality = 55; quality >= 40 && resultBytes.Length > targetMaxSize; quality -= 5)
                                            {
                                                outputMs.SetLength(0);
                                                encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(
                                                    System.Drawing.Imaging.Encoder.Quality, quality);
                                                finalImage.Save(outputMs, jpegCodec, encoderParams);
                                                resultBytes = outputMs.ToArray();
                                                Console.WriteLine($"   Qayta o'lchamlandi: {newWidth}x{newHeight}, Quality: {quality}%, Hajm: {resultBytes.Length / 1024} KB");
                                            }
                                        }
                                        
                                        Console.WriteLine($"   Rasm qayta o'lchamlandi: {newWidth}x{newHeight}, hajm: {resultBytes.Length / 1024} KB");
                                    }
                                }
                                
                                // Base64 uzunligini hisoblaymiz
                                int base64Length = (int)(resultBytes.Length * 1.33);
                                Console.WriteLine($"✅ Rasm qayta ishlandi: {targetWidth}x{targetHeight} → {resultBytes.Length / 1024} KB (Base64: ~{base64Length / 1000}K)");
                                
                                return Task.FromResult(resultBytes);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Rasm qayta ishlashda xatolik: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                // Xatolik bo'lsa, original rasmni qaytaramiz
                return Task.FromResult(imageBytes);
            }
        }

        // Face device'larga user va rasm yuborish
        private async Task<List<object>> AddUserToFaceDevicesAsync(long employeeNo, String name, String ism, String familiya, String imageUrl)
        {
            List<object> results = new List<object>();
            List<SkudDoors> skudDoorsList = await _context.SkudDoors.ToListAsync();
            
            if (skudDoorsList == null || skudDoorsList.Count == 0)
            {
                Console.WriteLine("⚠️ Face device'lar topilmadi (SkudDoors bo'sh)");
                return results;
            }
            
            foreach (SkudDoors skudDoor in skudDoorsList)
            {
                String deviceIp = skudDoor.device;
                String login = skudDoor.login;
                String password = skudDoor.password;
                String doorName = skudDoor.acc_name ?? "N/A";
                
                if (string.IsNullOrEmpty(deviceIp))
                {
                    results.Add(new { doorId = skudDoor.id, doorName = doorName, deviceIp = deviceIp ?? "N/A", status = "skipped", message = "IP manzil konfiguratsiya qilinmagan" });
                    continue;
                }
                
                Console.WriteLine($"\n📤 ========== Face device'ga user qo'shish ==========");
                Console.WriteLine($"Device: {deviceIp} (door: {doorName})");
                Console.WriteLine($"Employee No: {employeeNo}");
                Console.WriteLine($"Ism: {ism}");
                Console.WriteLine($"Familiya: {familiya}");
                Console.WriteLine($"To'liq ism: {name}");
                
                bool userCreated = false;
                bool faceUploaded = false;
                String errorMessage = "";
                
                try
                {
                    // ========== User yaratish ==========
                    Console.WriteLine($"\n📋 User yaratish (UserInfo/Record)");
                    
                    String userRecordApi = "/ISAPI/AccessControl/UserInfo/Record?format=json";
                    String userRecordUrl = "http://" + deviceIp + userRecordApi;
                    
                    // User ma'lumotlari
                    String beginTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                    String endTime = DateTime.Now.AddYears(5).ToString("yyyy-MM-ddTHH:mm:ss");
                    
                    var userInfo = new JObject();
                    userInfo["employeeNo"] = employeeNo.ToString();
                    userInfo["name"] = name;
                    userInfo["userType"] = "normal";
                    
                    var validObj = new JObject();
                    validObj["enable"] = true;
                    validObj["beginTime"] = beginTime;
                    validObj["endTime"] = endTime;
                    userInfo["Valid"] = validObj;
                    
                    userInfo["doorRight"] = "1";
                    
                    var userRoot = new JObject();
                    userRoot["UserInfo"] = userInfo;
                    
                    String userJsonBody = userRoot.ToString(Formatting.None);
                    
                    var userCredCache = new CredentialCache();
                    userCredCache.Add(new Uri(userRecordUrl), "Digest", new NetworkCredential(login, password));
                    var userHttpClient = new HttpClient(new HttpClientHandler { Credentials = userCredCache });
                    userHttpClient.Timeout = TimeSpan.FromSeconds(15);
                    
                    Console.WriteLine($"URL: {userRecordUrl}");
                    Console.WriteLine($"Method: POST");
                    Console.WriteLine($"Body: {userJsonBody}");
                    
                    var userContent = new StringContent(userJsonBody, UnicodeEncoding.UTF8, "application/json");
                    var userResponse = await userHttpClient.PostAsync(new Uri(userRecordUrl), userContent);
                    var userResponseString = await userResponse.Content.ReadAsStringAsync();
                    
                    Console.WriteLine($"Response Status: {userResponse.StatusCode}");
                    Console.WriteLine($"Response Body: {userResponseString}");
                    
                    if (userResponse.IsSuccessStatusCode)
                    {
                        try
                        {
                            var responseJson = JObject.Parse(userResponseString);
                            int? statusCode = responseJson["statusCode"]?.Value<int>();
                            String statusString = responseJson["statusString"]?.ToString();
                            
                            if (statusCode == 1 || statusString == "OK")
                            {
                                userCreated = true;
                                Console.WriteLine($"✅ User muvaffaqiyatli yaratildi");
                            }
                            else if (statusCode == 6)
                            {
                                String errorMsg = responseJson["errorMsg"]?.ToString();
                                String subStatusCode = responseJson["subStatusCode"]?.ToString();
                                
                                if (subStatusCode?.Contains("employeeNoAlreadyExist") == true || errorMsg?.Contains("checkUser") == true)
                                {
                                    userCreated = true; // User allaqachon mavjud - bu ham muvaffaqiyatli
                                    Console.WriteLine($"✅ User allaqachon mavjud (yangi yaratish shart emas)");
                                }
                                else
                                {
                                    errorMessage = $"User yaratish xatolik: statusCode={statusCode}, statusString={statusString}, errorMsg={errorMsg}";
                                    Console.WriteLine($"❌ {errorMessage}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Response parse xatolik: {ex.Message}");
                            userCreated = userResponse.IsSuccessStatusCode;
                        }
                    }
                    else
                    {
                        errorMessage = $"HTTP {userResponse.StatusCode}: {userResponseString}";
                        Console.WriteLine($"❌ User yaratish xatolik: {errorMessage}");
                    }
                    
                    // ========== Rasm yuborish (agar user yaratilgan bo'lsa va rasm URL mavjud bo'lsa) ==========
                    if (userCreated && !string.IsNullOrEmpty(imageUrl))
                    {
                        Console.WriteLine($"\n📋 Rasm yuborish (UserInfo/Modify bilan faceInfo)");
                        
                        try
                        {
                            // Rasmni URL'dan yuklab olish
                            byte[] imageBytes = null;
                            using (var imageHttpClient = new HttpClient())
                            {
                                imageHttpClient.Timeout = TimeSpan.FromSeconds(30);
                                imageBytes = await imageHttpClient.GetByteArrayAsync(imageUrl);
                                Console.WriteLine($"✅ Rasm yuklandi, hajmi: {imageBytes.Length} bytes");
                            }
                            
                            if (imageBytes != null && imageBytes.Length > 0)
                            {
                                // Rasmni face device talablariga moslab qayta ishlash
                                byte[] processedImageBytes = await ProcessImageForFaceDeviceAsync(imageBytes);
                                
                                if (processedImageBytes != null && processedImageBytes.Length > 0)
                                {
                                    // Base64 ga o'tkazish
                                    String base64Image = Convert.ToBase64String(processedImageBytes);
                                    
                                    // Base64 stringni tozalash (newline va space'larni olib tashlash)
                                    base64Image = base64Image.Replace("\n", "").Replace("\r", "").Replace(" ", "");
                                    
                                    Console.WriteLine($"✅ Rasm qayta ishlandi, hajmi: {processedImageBytes.Length} bytes, Base64 uzunligi: {base64Image.Length} characters");
                                    
                                    // Base64 uzunligini tekshirish (50KB dan kam bo'lishi kerak - DS-K1T341CMF tavsiyasi)
                                    int maxBase64Length = 50 * 1024; // 50KB (DS-K1T341CMF uchun tavsiya etilgan)
                                    if (base64Image.Length > maxBase64Length)
                                    {
                                        Console.WriteLine($"⚠️ Base64 uzunligi juda katta: {base64Image.Length / 1024}KB (maksimal: {maxBase64Length / 1024}KB)");
                                        Console.WriteLine($"⚠️ DS-K1T341CMF uchun 50KB dan kam bo'lishi tavsiya etiladi");
                                        Console.WriteLine($"⚠️ Rasm yanada siqilishi kerak, lekin hozircha yuborilmoqda...");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"✅ Base64 uzunligi optimal: {base64Image.Length / 1024}KB (maksimal: {maxBase64Length / 1024}KB)");
                                    }
                                    
                                    // DS-K1T341CMF uchun face yozish faqat FaceDataRecord orqali ishlaydi
                                    // UserInfo/Modify face yozish uchun ishlamaydi (faqat metadata uchun)
                                    // MUHIM: DS-K1T341CMF ba'zi firmware'larda name va Valid maydonlarini ham talab qiladi
                                    // faceLibType = "normalFD" bo'lishi kerak (ba'zi firmware'larda "blackFD" ham ishlaydi, lekin "normalFD" eng xavfsiz)
                                    String faceDataRecordApi = "/ISAPI/Intelligent/FDLib/FaceDataRecord?format=json";
                                    String faceDataRecordUrl = "http://" + deviceIp + faceDataRecordApi;
                                    
                                    // DS-K1T341CMF uchun multi-variant yondashuv - turli faceLibType variantlarini sinab ko'ramiz
                                    // name maydoni majburiy (bo'sh bo'lmasligi kerak)
                                    String faceName = string.IsNullOrWhiteSpace(name) ? "User" : name;
                                    
                                    // valid maydoni majburiy (kichik harf bilan - "valid", "Valid" emas!)
                                    String faceBeginTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                                    String faceEndTime = DateTime.Now.AddYears(5).ToString("yyyy-MM-ddTHH:mm:ss");
                                    var faceValidObj = new JObject();
                                    faceValidObj["enable"] = true;
                                    faceValidObj["beginTime"] = faceBeginTime;
                                    faceValidObj["endTime"] = faceEndTime;
                                    
                                    // Variantlar ro'yxati (DS-K1T341CMF uchun turli firmware'lar uchun)
                                    String[] faceLibTypeVariants = { "normalFD", "blackFD", "normal" };
                                    bool faceUploadSuccess = false;
                                    
                                    foreach (String faceLibTypeValue in faceLibTypeVariants)
                                    {
                                        Console.WriteLine($"\n📷 ========== FaceDataRecord API Chaqiruvi (Variant: faceLibType='{faceLibTypeValue}') ==========");
                                        
                                        // FaceDataRecord JSON body yaratish
                                        var faceDataRecord = new JObject();
                                        faceDataRecord["employeeNo"] = employeeNo.ToString();
                                        faceDataRecord["faceLibType"] = faceLibTypeValue;
                                        faceDataRecord["name"] = faceName;
                                        faceDataRecord["faceData"] = base64Image;
                                        faceDataRecord["valid"] = faceValidObj;
                                        
                                        // Root object
                                        var faceDataRecordRoot = new JObject();
                                        faceDataRecordRoot["FaceDataRecord"] = faceDataRecord;
                                        
                                        // JSON serialization
                                        String faceDataRecordJsonBody = faceDataRecordRoot.ToString(Newtonsoft.Json.Formatting.None);
                                        
                                        var faceCredCache = new CredentialCache();
                                        faceCredCache.Add(new Uri(faceDataRecordUrl), "Digest", new NetworkCredential(login, password));
                                        var faceHttpClient = new HttpClient(new HttpClientHandler { Credentials = faceCredCache });
                                        faceHttpClient.Timeout = TimeSpan.FromSeconds(30);
                                        
                                        Console.WriteLine($"📷 URL: {faceDataRecordUrl}");
                                        Console.WriteLine($"📷 Method: POST");
                                        Console.WriteLine($"📷 Employee No: {employeeNo}");
                                        Console.WriteLine($"📷 Name: {faceName}");
                                        Console.WriteLine($"📷 FaceLibType: {faceLibTypeValue}");
                                        Console.WriteLine($"📷 Base64 uzunligi: {base64Image.Length} characters (~{base64Image.Length / 1024}KB)");
                                        Console.WriteLine($"📷 To'liq JSON Body:");
                                        Console.WriteLine(faceDataRecordJsonBody);
                                        
                                        var faceContent = new StringContent(faceDataRecordJsonBody, System.Text.Encoding.UTF8, "application/json");
                                        var faceResponse = await faceHttpClient.PostAsync(new Uri(faceDataRecordUrl), faceContent);
                                        var faceResponseString = await faceResponse.Content.ReadAsStringAsync();
                                        
                                        Console.WriteLine($"📷 Response Status: {faceResponse.StatusCode}");
                                        Console.WriteLine($"📷 Response Body: {faceResponseString}");
                                        
                                        if (faceResponse.IsSuccessStatusCode)
                                        {
                                            try
                                            {
                                                var responseJson = JObject.Parse(faceResponseString);
                                                int? statusCode = responseJson["statusCode"]?.Value<int>();
                                                String statusString = responseJson["statusString"]?.ToString();
                                                
                                                if (statusCode == 1 || statusString == "OK")
                                                {
                                                    faceUploadSuccess = true;
                                                    faceUploaded = true;
                                                    Console.WriteLine($"✅ Rasm muvaffaqiyatli yuklandi (FaceDataRecord) - statusCode={statusCode}, statusString={statusString}");
                                                    Console.WriteLine($"✅ DS-K1T341CMF da FaceDataRecord orqali face device'ning built-in database'iga yoziladi (faceLibType='{faceLibTypeValue}' bilan)");
                                                    break; // Muvaffaqiyatli bo'lsa, boshqa variantlarni sinash shart emas
                                                }
                                                else
                                                {
                                                    String errorMsg = responseJson["errorMsg"]?.ToString();
                                                    String subStatusCode = responseJson["subStatusCode"]?.ToString();
                                                    Console.WriteLine($"⚠️ Variant '{faceLibTypeValue}' ishlamadi: statusCode={statusCode}, statusString={statusString}, errorMsg={errorMsg}, subStatusCode={subStatusCode}");
                                                    
                                                    // Agar faceLibType xatoligi bo'lsa, keyingi variantni sinab ko'ramiz
                                                    if (errorMsg == "faceLibType" || subStatusCode == "MessageParametersLack")
                                                    {
                                                        Console.WriteLine($"⚠️ faceLibType='{faceLibTypeValue}' qabul qilinmadi, keyingi variantni sinab ko'ramiz...");
                                                        continue;
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"⚠️ Response parse xatolik: {ex.Message}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"⚠️ HTTP xatolik: {faceResponse.StatusCode} - {faceResponseString}");
                                            
                                            // Agar faceLibType xatoligi bo'lsa, keyingi variantni sinab ko'ramiz
                                            try
                                            {
                                                var errorJson = JObject.Parse(faceResponseString);
                                                String errorMsg = errorJson["errorMsg"]?.ToString();
                                                String subStatusCode = errorJson["subStatusCode"]?.ToString();
                                                
                                                if (errorMsg == "faceLibType" || subStatusCode == "MessageParametersLack")
                                                {
                                                    Console.WriteLine($"⚠️ faceLibType='{faceLibTypeValue}' qabul qilinmadi, keyingi variantni sinab ko'ramiz...");
                                                    continue;
                                                }
                                            }
                                            catch { }
                                        }
                                        
                                        Console.WriteLine($"📷 ================================================\n");
                                    }
                                    
                                    // Agar barcha variantlar ishlamasa, faceLibType'siz sinab ko'ramiz
                                    if (!faceUploadSuccess)
                                    {
                                        Console.WriteLine($"\n📷 ========== FaceDataRecord API Chaqiruvi (Variant: faceLibType O'CHIRILGAN) ==========");
                                        Console.WriteLine($"⚠️ Barcha faceLibType variantlari ishlamadi, faceLibType'siz sinab ko'ramiz...");
                                        
                                        // FaceDataRecord JSON body yaratish (faceLibType'siz)
                                        var faceDataRecord = new JObject();
                                        faceDataRecord["employeeNo"] = employeeNo.ToString();
                                        // faceLibType olib tashlandi
                                        faceDataRecord["name"] = faceName;
                                        faceDataRecord["faceData"] = base64Image;
                                        faceDataRecord["valid"] = faceValidObj;
                                        
                                        // Root object
                                        var faceDataRecordRoot = new JObject();
                                        faceDataRecordRoot["FaceDataRecord"] = faceDataRecord;
                                        
                                        // JSON serialization
                                        String faceDataRecordJsonBody = faceDataRecordRoot.ToString(Newtonsoft.Json.Formatting.None);
                                        
                                        var faceCredCache = new CredentialCache();
                                        faceCredCache.Add(new Uri(faceDataRecordUrl), "Digest", new NetworkCredential(login, password));
                                        var faceHttpClient = new HttpClient(new HttpClientHandler { Credentials = faceCredCache });
                                        faceHttpClient.Timeout = TimeSpan.FromSeconds(30);
                                        
                                        Console.WriteLine($"📷 URL: {faceDataRecordUrl}");
                                        Console.WriteLine($"📷 Method: POST");
                                        Console.WriteLine($"📷 Employee No: {employeeNo}");
                                        Console.WriteLine($"📷 Name: {faceName}");
                                        Console.WriteLine($"📷 FaceLibType: O'CHIRILGAN");
                                        Console.WriteLine($"📷 To'liq JSON Body:");
                                        Console.WriteLine(faceDataRecordJsonBody);
                                        
                                        var faceContent = new StringContent(faceDataRecordJsonBody, System.Text.Encoding.UTF8, "application/json");
                                        var faceResponse = await faceHttpClient.PostAsync(new Uri(faceDataRecordUrl), faceContent);
                                        var faceResponseString = await faceResponse.Content.ReadAsStringAsync();
                                        
                                        Console.WriteLine($"📷 Response Status: {faceResponse.StatusCode}");
                                        Console.WriteLine($"📷 Response Body: {faceResponseString}");
                                        
                                        if (faceResponse.IsSuccessStatusCode)
                                        {
                                            try
                                            {
                                                var responseJson = JObject.Parse(faceResponseString);
                                                int? statusCode = responseJson["statusCode"]?.Value<int>();
                                                String statusString = responseJson["statusString"]?.ToString();
                                                
                                                if (statusCode == 1 || statusString == "OK")
                                                {
                                                    faceUploaded = true;
                                                    Console.WriteLine($"✅ Rasm muvaffaqiyatli yuklandi (FaceDataRecord, faceLibType'siz) - statusCode={statusCode}, statusString={statusString}");
                                                }
                                                else
                                                {
                                                    String errorMsg = responseJson["errorMsg"]?.ToString();
                                                    Console.WriteLine($"⚠️ Rasm yuklashda xatolik (faceLibType'siz): statusCode={statusCode}, statusString={statusString}, errorMsg={errorMsg}");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"⚠️ Response parse xatolik: {ex.Message}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"⚠️ Rasm yuklash HTTP xatolik (faceLibType'siz): {faceResponse.StatusCode} - {faceResponseString}");
                                        }
                                        
                                        Console.WriteLine($"📷 ================================================\n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"⚠️ Rasm qayta ishlashda xatolik yuz berdi");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"⚠️ Rasm yuborishda xatolik: {ex.Message}");
                            Console.WriteLine($"Stack trace: {ex.StackTrace}");
                        }
                    }
                    else if (!string.IsNullOrEmpty(imageUrl))
                    {
                        Console.WriteLine($"⚠️ User yaratilmadi, rasm yuborish o'tkazib yuborildi");
                    }
                    else
                    {
                        Console.WriteLine($"ℹ️ Rasm URL mavjud emas, rasm yuborish o'tkazib yuborildi");
                    }
                }
                catch (HttpRequestException e)
                {
                    errorMessage = $"Ulanish xatosi: {e.Message}";
                    Console.WriteLine($"Face device HttpRequestException for {deviceIp}: {errorMessage}");
                }
                catch (TaskCanceledException)
                {
                    errorMessage = "Vaqt tugadi (timeout)";
                    Console.WriteLine($"Face device timeout for {deviceIp}");
                }
                catch (Exception e)
                {
                    errorMessage = $"Xatolik: {e.Message}";
                    Console.WriteLine($"Face device Exception for {deviceIp}: {errorMessage}");
                }
                
                // Natijani qo'shish
                // User yaratilgan bo'lishi kerak, rasm esa ixtiyoriy (agar URL mavjud bo'lsa)
                String finalStatus = userCreated ? "success" : "failed";
                String finalMessage = "";
                
                if (userCreated)
                {
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        finalMessage = faceUploaded ? "Muvaffaqiyatli qo'shildi (user + rasm)" : "User qo'shildi, lekin rasm yuborishda xatolik";
                    }
                    else
                    {
                        finalMessage = "Muvaffaqiyatli qo'shildi (user, rasm yo'q)";
                    }
                }
                else
                {
                    finalMessage = errorMessage;
                }
                
                results.Add(new
                {
                    doorId = skudDoor.id,
                    doorName = doorName,
                    deviceIp = deviceIp,
                    status = finalStatus,
                    message = finalMessage,
                    userCreated = userCreated,
                    faceUploaded = faceUploaded
                });
            }
            
            return results;
        }

        // POST: api/SkudMyUserinfoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<object>> PostSkudMyUserinfo([FromBody] JObject jsonData)
        {
            try
            {
                // JSON body'dan ma'lumotlarni olish
                SkudMyUserinfo skudMyUserinfo = jsonData.ToObject<SkudMyUserinfo>();
                
                // Database'ga qo'shish
                _context.SkudMyUserinfo.Add(skudMyUserinfo);
                await _context.SaveChangesAsync();

                // ================== QARZDORLIK DAFTARIGA AVTOMATIK QO'SHISH ==================
                // Yangi xodim qo'shilganda uni avtomatik ravishda TegirmonQarzdorlikUser jadvaliga ham qo'shib qo'yamiz
                try
                {
                    await EnsureQarzdorlikUserForSkudUserAsync(skudMyUserinfo);
                }
                catch (Exception exQarz)
                {
                    // Qarzdorlik qismida xatolik bo'lsa ham, asosiy user saqlanishi kerak
                    Console.WriteLine($"⚠️ Qarzdorlik user yaratishda xatolik (SkudMyUserinfo badgenumber={skudMyUserinfo.badgenumber}): {exQarz.Message}");
                }
                // ========================================================================

                // Face device'larga yuborish natijalari
                List<object> faceResults = new List<object>();
                
                // Employee ID ni olish (JSON body'dan badgenumber yoki userid, agar 0 bo'lsa database'dan olingan badgenumber)
                long employeeId = 0;
                if (jsonData["badgenumber"] != null && jsonData["badgenumber"].Type != JTokenType.Null)
                {
                    long.TryParse(jsonData["badgenumber"].ToString(), out employeeId);
                }
                if (employeeId == 0 && jsonData["userid"] != null && jsonData["userid"].Type != JTokenType.Null)
                {
                    long.TryParse(jsonData["userid"].ToString(), out employeeId);
                }
                // Agar hali ham 0 bo'lsa, database'ga qo'shilgandan keyin olingan badgenumber ni ishlatamiz
                if (employeeId == 0)
                {
                    employeeId = skudMyUserinfo.badgenumber;
                }
                
                // Ism va familiya'ni olish
                String ism = skudMyUserinfo.ism ?? "";
                String familiya = skudMyUserinfo.familiya ?? "";
                String userName = $"{ism} {familiya}".Trim();
                if (string.IsNullOrEmpty(userName))
                {
                    userName = ism ?? familiya ?? "";
                }
                
                Console.WriteLine($"📋 Employee ID: {employeeId}, Ism: {ism}, Familiya: {familiya}, To'liq ism: {userName}");
                
                // Rasm URL'ni olish
                String imageUrl = jsonData["image_url"]?.ToString();
                if (string.IsNullOrEmpty(imageUrl))
                {
                    imageUrl = skudMyUserinfo.image_url;
                }
                
                // Agar nisbiy URL bo'lsa, to'liq URL'ga aylantirish
                if (!string.IsNullOrEmpty(imageUrl) && !Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
                {
                    // Nisbiy URL ni to'liq URL'ga aylantirish
                    var request = HttpContext.Request;
                    String baseUrl = $"{request.Scheme}://{request.Host}";
                    if (request.Host.Port.HasValue)
                    {
                        baseUrl = $"{request.Scheme}://{request.Host.Host}:{request.Host.Port}";
                    }
                    
                    // Agar imageUrl "/" bilan boshlanmasa, "/" qo'shamiz
                    if (!imageUrl.StartsWith("/"))
                    {
                        imageUrl = "/" + imageUrl;
                    }
                    
                    imageUrl = baseUrl + imageUrl;
                    Console.WriteLine($"📷 Nisbiy URL to'liq URL'ga aylantirildi: {imageUrl}");
                }
                
                Console.WriteLine($"📷 Image URL: {imageUrl ?? "Mavjud emas"}");
                
                // ===============================================
                //  DEVICE'LARGA YUBORISH VAQTINCHA O'CHIRILGAN
                //  Quyidagi kodni keyinroq SDK tayyor bo'lganda
                //  qayta yoqish mumkin.
                //
                // // 2 soniya kutamiz (user yaratilishi uchun)
                // await Task.Delay(2000);
                // 
                // // Face device'larga yuborish (user yaratish + rasm yuborish)
                // faceResults = await AddUserToFaceDevicesAsync(
                //     employeeId,
                //     userName,
                //     ism,
                //     familiya,
                //     imageUrl
                // );
                //
                // // Barcha face device'larga muvaffaqiyatli yozilganligini tekshiramiz
                // bool allFaceDevicesSuccess = faceResults.All(r => 
                // {
                //     try
                //     {
                //         var resultObj = JObject.FromObject(r);
                //         String status = resultObj["status"]?.ToString();
                //         return status == "success" || status == "skipped";
                //     }
                //     catch
                //     {
                //         return false;
                //     }
                // });
                //
                // Hozircha device'larga hech narsa yuborilmaydi
                // ===============================================

                faceResults = new List<object>();
                bool allFaceDevicesSuccess = true;

                // Natijani qaytarish (faqat CRM bazasiga saqlash muvaffaqiyati)
                return Ok(new
                {
                    user = skudMyUserinfo,
                    faceResults = faceResults,
                    success = allFaceDevicesSuccess,
                    message = "Muvaffaqiyatli saqlandi (device'larga yuborish vaqtincha o'chirilgan)"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PostSkudMyUserinfo xatolik: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("updateUserInfo")]
        public async Task<ActionResult<SkudMyUserinfo>> updateUserInfo(SkudMyUserinfo skudMyUserinfo)
        {
            _context.SkudMyUserinfo.Update(skudMyUserinfo);
            await _context.SaveChangesAsync();

            // Xodim update qilinganda TegirmonQarzdorlikUser ni ham update qilish
            try
            {
                await UpdateQarzdorlikUserForSkudUserAsync(skudMyUserinfo);
            }
            catch (Exception exQarz)
            {
                // Qarzdorlik qismida xatolik bo'lsa ham, asosiy user update bo'lishi kerak
                Console.WriteLine($"⚠️ Qarzdorlik user update qilishda xatolik (SkudMyUserinfo badgenumber={skudMyUserinfo.badgenumber}): {exQarz.Message}");
            }

            return skudMyUserinfo;
        }

        [HttpPost("updateUserInfoList")]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> updateUserInfoList([FromQuery] List<SkudMyUserinfo> skudMyUserinfoList)
        {
            _context.SkudMyUserinfo.UpdateRange(skudMyUserinfoList);
            await _context.SaveChangesAsync();

            return skudMyUserinfoList;
        }

        [HttpPost("updateUserInfoListOnlyNotBlokedUsers")]
        public async Task<ActionResult<IEnumerable<SkudMyUserinfo>>> updateUserInfoListOnlyNotBlokedUsers( List<SkudMyUserinfo> skudMyUserinfoList)
        {
            foreach (SkudMyUserinfo it in skudMyUserinfoList) {

                if (it.gr == 1)
                {
                    _context.SkudMyUserinfo.Update(it);
                    await _context.SaveChangesAsync();
                }
                else {
                    _context.SkudMyUserinfo.Add(it);
                    await _context.SaveChangesAsync();
                }
         

            }

    

            return skudMyUserinfoList;
        }

        // Face device'lardan user o'chirish funksiyasi
        private async Task<List<object>> DeleteUserFromAllFacesAsync(long user_id, String fio)
        {
            List<object> results = new List<object>();
            List<SkudDoors> skudDoorsList = await _context.SkudDoors.ToListAsync();
            
            if (skudDoorsList == null || skudDoorsList.Count == 0)
            {
                Console.WriteLine("⚠️ Face device'lar topilmadi (SkudDoors bo'sh)");
                return results;
            }
            
            foreach (SkudDoors skudDoors in skudDoorsList)
            {
                String user = "";
                String password = "";
                String host_addr = "";
                long emp_nomer = user_id;

                if (skudDoors != null)
                {
                    user = skudDoors.login;
                    password = skudDoors.password;
                    host_addr = skudDoors.device;
                }

                if (string.IsNullOrEmpty(host_addr))
                {
                    results.Add(new { doorId = skudDoors.id, doorName = skudDoors.acc_name ?? "N/A", deviceIp = host_addr ?? "N/A", status = "skipped", message = "IP manzil konfiguratsiya qilinmagan" });
                    continue;
                }

                bool success = false;
                String errorMessage = "";
                String doorName = skudDoors.acc_name ?? "N/A";
                Console.WriteLine($"\n🗑️ ========== User'ni facedan o'chirish ==========");
                Console.WriteLine($"Device: {host_addr} (door: {doorName})");
                Console.WriteLine($"Employee No: {emp_nomer}");
                Console.WriteLine($"Name: {fio ?? "N/A"}");

                try
                {
                    var credCache = new CredentialCache();
                    var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
                    httpClient.Timeout = TimeSpan.FromSeconds(15);

                    // ========== User o'chirish (DS-K1T341CMF uchun - UserInfo/Modify deleteUser=true yetarli, face avtomatik o'chadi) ==========
                    Console.WriteLine($"\n📋 User o'chirish (UserInfo/Modify deleteUser=true)");
                    Console.WriteLine($"ℹ️ DS-K1T341CMF modelida UserInfo/Modify deleteUser=true qilganda face ham avtomatik o'chadi");
                    
                    String modifyApi = "/ISAPI/AccessControl/UserInfo/Modify?format=json";
                    String modifyUrl = "http://" + host_addr + modifyApi;
                    
                    // deleteUser: true bo'lganda, minimal format ishlatamiz (Valid object'ini yubormaymiz)
                    var modifyUserInfo = new JObject();
                    modifyUserInfo["employeeNo"] = emp_nomer.ToString();
                    modifyUserInfo["deleteUser"] = true;
                    
                    var modifyRoot = new JObject();
                    modifyRoot["UserInfo"] = modifyUserInfo;
                    
                    String modifyJsonBody = modifyRoot.ToString(Formatting.None);
                    
                    credCache.Add(new Uri(modifyUrl), "Digest", new NetworkCredential(user, password));
                    
                    Console.WriteLine($"URL: {modifyUrl}");
                    Console.WriteLine($"Method: PUT");
                    Console.WriteLine($"Body: {modifyJsonBody}");
                    
                    var modifyContent = new StringContent(modifyJsonBody, UnicodeEncoding.UTF8, "application/json");
                    var modifyAnswer = await httpClient.PutAsync(new Uri(modifyUrl), modifyContent);
                    var modifyResponseString = await modifyAnswer.Content.ReadAsStringAsync();
                    
                    Console.WriteLine($"Response Status: {modifyAnswer.StatusCode}");
                    Console.WriteLine($"Response Body: {modifyResponseString}");
                    
                    bool userDeleted = false;
                    if (modifyAnswer.IsSuccessStatusCode)
                    {
                        try
                        {
                            var responseJson = JObject.Parse(modifyResponseString);
                            int? statusCode = responseJson["statusCode"]?.Value<int>();
                            String statusString = responseJson["statusString"]?.ToString();
                            String errorMsg = responseJson["errorMsg"]?.ToString();
                            
                            if (statusCode == 1 || statusString == "OK")
                            {
                                userDeleted = true;
                                Console.WriteLine($"✅ User muvaffaqiyatli o'chirildi (AccessControl)");
                            }
                            else if (statusCode == 6 && (errorMsg?.Contains("checkUser") == true || errorMsg?.Contains("userNotExist") == true))
                            {
                                userDeleted = true; // User topilmadi - bu ham muvaffaqiyatli, chunki maqsad - user'ni yo'q qilish
                                Console.WriteLine($"✅ User topilmadi (allaqachon o'chirilgan) - bu muvaffaqiyatli deb hisoblanadi");
                            }
                            else
                            {
                                errorMessage = $"AccessControl o'chirish xatolik: statusCode={statusCode}, statusString={statusString}, errorMsg={errorMsg}";
                                Console.WriteLine($"❌ {errorMessage}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Response parse xatolik: {ex.Message}");
                            userDeleted = modifyAnswer.IsSuccessStatusCode;
                        }
                    }
                    else
                    {
                        errorMessage = $"HTTP {modifyAnswer.StatusCode}: {modifyResponseString}";
                        Console.WriteLine($"❌ {errorMessage}");
                    }
                    
                    // Muvaffaqiyatli o'chirish - DS-K1T341CMF'da UserInfo/Modify deleteUser=true qilganda face ham avtomatik o'chadi
                    if (userDeleted)
                    {
                        success = true;
                        Console.WriteLine($"✅ User muvaffaqiyatli o'chirildi (face avtomatik o'chdi)");
                        
                        // O'chirilganini tekshirish (debug uchun)
                        try
                        {
                            Console.WriteLine($"\n🔍 O'chirilganini tekshirish (FDSearch)...");
                            await Task.Delay(500); // Device'ga vaqt berish
                            
                            String faceSearchApi = "/ISAPI/Intelligent/FDLib/FDSearch?format=json";
                            String faceSearchUrl = "http://" + host_addr + faceSearchApi;
                            
                            var fdSearch = new JObject();
                            fdSearch["searchID"] = "check_" + emp_nomer;
                            fdSearch["searchResultPosition"] = 0;
                            fdSearch["maxResults"] = 5;
                            fdSearch["employeeNo"] = emp_nomer.ToString();
                            
                            var fdSearchRoot = new JObject();
                            fdSearchRoot["FDSearch"] = fdSearch;
                            
                            String faceSearchJsonBody = fdSearchRoot.ToString(Formatting.None);
                            
                            var faceSearchCredCache = new CredentialCache();
                            faceSearchCredCache.Add(new Uri(faceSearchUrl), "Digest", new NetworkCredential(user, password));
                            var faceSearchHttpClient = new HttpClient(new HttpClientHandler { Credentials = faceSearchCredCache });
                            faceSearchHttpClient.Timeout = TimeSpan.FromSeconds(10);
                            
                            var faceSearchContent = new StringContent(faceSearchJsonBody, UnicodeEncoding.UTF8, "application/json");
                            var faceSearchResponse = await faceSearchHttpClient.PostAsync(new Uri(faceSearchUrl), faceSearchContent);
                            var faceSearchResponseString = await faceSearchResponse.Content.ReadAsStringAsync();
                            
                            if (faceSearchResponse.IsSuccessStatusCode)
                            {
                                try
                                {
                                    var searchResponseJson = JObject.Parse(faceSearchResponseString);
                                    var fdSearchResult = searchResponseJson["FDSearch"] as JObject;
                                    if (fdSearchResult != null)
                                    {
                                        int? numOfMatches = fdSearchResult["numOfMatches"]?.Value<int>();
                                        if (numOfMatches == 0)
                                        {
                                            Console.WriteLine($"✅ Tasdiqlandi: Face to'liq o'chirildi (numOfMatches = 0)");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"⚠️ Ogohlantirish: Face hali ham mavjud (numOfMatches = {numOfMatches})");
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Tekshirish response parse xatolik: {ex.Message}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Tekshirishda xatolik: {ex.Message}");
                        }
                    }
                    else
                    {
                        errorMessage = errorMessage ?? "AccessControl'dan o'chirish muvaffaqiyatsiz";
                        Console.WriteLine($"❌ {errorMessage}");
                    }
                }
                catch (HttpRequestException e)
                {
                    errorMessage = $"Ulanish xatosi: {e.Message}";
                    Console.WriteLine($"Face delete HttpRequestException for {host_addr}: {errorMessage}");
                }
                catch (TaskCanceledException)
                {
                    errorMessage = "Vaqt tugadi (timeout)";
                    Console.WriteLine($"Face delete timeout for {host_addr}");
                }
                catch (Exception e)
                {
                    errorMessage = $"Xatolik: {e.Message}";
                    Console.WriteLine($"Face delete Exception for {host_addr}: {errorMessage}");
                }

                results.Add(new
                {
                    doorId = skudDoors.id,
                    doorName = doorName,
                    deviceIp = host_addr,
                    status = success ? "success" : "failed",
                    message = success ? "Muvaffaqiyatli o'chirildi" : errorMessage
                });
            }
            
            return results;
        }

        /// <summary>
        /// Yangi qo'shilgan SkudMyUserinfo foydalanuvchisini qarzdorlik daftariga (TegirmonQarzdorlikUser) avtomatik qo'shadi,
        /// agar u yerda hali mavjud bo'lmasa.
        /// </summary>
        private async Task EnsureQarzdorlikUserForSkudUserAsync(SkudMyUserinfo skudUser)
        {
            try
            {
                if (skudUser == null)
                    return;

                // user_face_id sifatida userid (agar mavjud bo'lsa) yoki badgenumber dan foydalanamiz
                int userFaceId = 0;
                if (skudUser.userid.HasValue && skudUser.userid.Value > 0)
                {
                    userFaceId = Convert.ToInt32(skudUser.userid.Value);
                }
                else if (skudUser.badgenumber > 0 && skudUser.badgenumber <= int.MaxValue)
                {
                    userFaceId = (int)skudUser.badgenumber;
                }

                if (userFaceId <= 0)
                {
                    Console.WriteLine($"ℹ️ Qarzdorlik user uchun user_face_id aniqlanmadi (SkudUser badgenumber={skudUser.badgenumber}, userid={skudUser.userid})");
                    return;
                }

                // Agar allaqachon qarzdorlik jadvalida bo'lsa, qaytib chiqamiz
                bool existsQarz = await _context.TegirmonQarzdorlikUser.AnyAsync(t => t.user_face_id == userFaceId);
                if (existsQarz)
                {
                    Console.WriteLine($"ℹ️ Qarzdorlik user allaqachon mavjud (user_face_id={userFaceId})");
                    return;
                }

                var qarzUser = new TegirmonQarzdorlikUser
                {
                    fio = skudUser.ism,
                    phone_number = skudUser.phone_number,
                    addiotionala_information = skudUser.born_date,
                    passport_number = skudUser.passport,
                    adddress = skudUser.address,
                    image_url = skudUser.image_url,
                    user_face_id = userFaceId,
                    created_date = DateTime.Now,
                    udate_date = DateTime.Now,
                    sum = 0,
                    dollor = 0
                };

                await _context.TegirmonQarzdorlikUser.AddAsync(qarzUser);
                await _context.SaveChangesAsync();

                Console.WriteLine($"✅ Yangi qarzdorlik foydalanuvchi qo'shildi: user_face_id={userFaceId}, fio={qarzUser.fio}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ EnsureQarzdorlikUserForSkudUserAsync xatolik (badgenumber={skudUser?.badgenumber}, userid={skudUser?.userid}): {ex.Message}");
            }
        }

        /// <summary>
        /// SkudMyUserinfo foydalanuvchisi update qilinganda TegirmonQarzdorlikUser ni ham update qiladi,
        /// agar u yerda mavjud bo'lsa. Agar mavjud bo'lmasa, yangi qo'shadi.
        /// </summary>
        private async Task UpdateQarzdorlikUserForSkudUserAsync(SkudMyUserinfo skudUser)
        {
            try
            {
                if (skudUser == null)
                    return;

                // user_face_id sifatida userid (agar mavjud bo'lsa) yoki badgenumber dan foydalanamiz
                int userFaceId = 0;
                if (skudUser.userid.HasValue && skudUser.userid.Value > 0)
                {
                    userFaceId = Convert.ToInt32(skudUser.userid.Value);
                }
                else if (skudUser.badgenumber > 0 && skudUser.badgenumber <= int.MaxValue)
                {
                    userFaceId = (int)skudUser.badgenumber;
                }

                if (userFaceId <= 0)
                {
                    Console.WriteLine($"ℹ️ Qarzdorlik user update uchun user_face_id aniqlanmadi (SkudUser badgenumber={skudUser.badgenumber}, userid={skudUser.userid})");
                    return;
                }

                // TegirmonQarzdorlikUser ni topish
                var existingQarzUser = await _context.TegirmonQarzdorlikUser
                    .FirstOrDefaultAsync(t => t.user_face_id == userFaceId);

                if (existingQarzUser != null)
                {
                    // Mavjud bo'lsa, update qilish
                    // sum va dollor ni saqlab qolish (qarz ma'lumotlari o'zgarib ketmasligi uchun)
                    double oldSum = existingQarzUser.sum;
                    double oldDollor = existingQarzUser.dollor;
                    
                    existingQarzUser.fio = skudUser.ism;
                    existingQarzUser.phone_number = skudUser.phone_number;
                    existingQarzUser.addiotionala_information = skudUser.born_date;
                    existingQarzUser.passport_number = skudUser.passport;
                    existingQarzUser.adddress = skudUser.address;
                    existingQarzUser.image_url = skudUser.image_url;
                    existingQarzUser.udate_date = DateTime.Now;
                    
                    // sum va dollor ni eski qiymatlariga qaytarish (agar ular o'zgargan bo'lsa)
                    existingQarzUser.sum = oldSum;
                    existingQarzUser.dollor = oldDollor;

                    _context.TegirmonQarzdorlikUser.Update(existingQarzUser);
                    await _context.SaveChangesAsync();

                    Console.WriteLine($"✅ Qarzdorlik foydalanuvchi update qilindi: user_face_id={userFaceId}, fio={existingQarzUser.fio}, sum={oldSum}, dollor={oldDollor}");
                }
                else
                {
                    // Mavjud bo'lmasa, yangi qo'shish (EnsureQarzdorlikUserForSkudUserAsync funksiyasini chaqiramiz)
                    await EnsureQarzdorlikUserForSkudUserAsync(skudUser);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ UpdateQarzdorlikUserForSkudUserAsync xatolik (badgenumber={skudUser?.badgenumber}, userid={skudUser?.userid}): {ex.Message}");
            }
        }

        // DELETE: api/SkudMyUserinfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteSkudMyUserinfo(long id)
        {
            var skudMyUserinfo = await _context.SkudMyUserinfo.FindAsync(id);
            if (skudMyUserinfo == null)
            {
                return NotFound(new { error = "User topilmadi", message = $"ID {id} bo'yicha user topilmadi" });
            }

            // ========== VAQTINCHA O'CHIRILGAN: Device'lardan o'chirish ==========
            // Hozircha device'lardan o'chirilmaydi, faqat bazadan o'chiriladi
            // Kod saqlanib qolgan, faqat comment qilingan
            
            // // User nomini olish
            // String ism = skudMyUserinfo.ism ?? "";
            // String familiya = skudMyUserinfo.familiya ?? "";
            // String userName = $"{ism} {familiya}".Trim();
            // if (string.IsNullOrEmpty(userName))
            // {
            //     userName = ism ?? familiya ?? "";
            // }
            // 
            // // Avval face device'lardan o'chirish
            // List<object> faceDeleteResults = await DeleteUserFromAllFacesAsync(skudMyUserinfo.badgenumber, userName);
            // 
            // // Face device'lardan o'chirish natijalarini tekshiramiz
            // bool allFaceDevicesDeleted = faceDeleteResults.All(r =>
            // {
            //     try
            //     {
            //         var resultObj = JObject.FromObject(r);
            //         String status = resultObj["status"]?.ToString();
            //         return status == "success" || status == "skipped";
            //     }
            //     catch
            //     {
            //         return false;
            //     }
            // });
            // 
            // // Agar barcha face device'lardan o'chirish muvaffaqiyatsiz bo'lsa, programmadan ham o'chirmaymiz
            // if (!allFaceDevicesDeleted)
            // {
            //     // Muvaffaqiyatsiz device'larni topamiz
            //     var failedDevices = faceDeleteResults.Where(r =>
            //     {
            //         try
            //         {
            //             var resultObj = JObject.FromObject(r);
            //             String status = resultObj["status"]?.ToString();
            //             return status == "failed";
            //         }
            //         catch
            //         {
            //             return false;
            //         }
            //     }).Select(r =>
            //     {
            //         try
            //         {
            //             var resultObj = JObject.FromObject(r);
            //             return new
            //             {
            //                 doorName = resultObj["doorName"]?.ToString() ?? "N/A",
            //                 deviceIp = resultObj["deviceIp"]?.ToString() ?? "N/A",
            //                 message = resultObj["message"]?.ToString() ?? "Noma'lum xatolik"
            //             };
            //         }
            //         catch
            //         {
            //             return new { doorName = "N/A", deviceIp = "N/A", message = "Noma'lum xatolik" };
            //         }
            //     }).ToList();
            // 
            //     String errorMessage = "Face device'lardan o'chirish muvaffaqiyatsiz:\n";
            //     foreach (var device in failedDevices)
            //     {
            //         errorMessage += $"- {device.doorName} ({device.deviceIp}): {device.message}\n";
            //     }
            // 
            //     return BadRequest(new
            //     {
            //         error = "Face device'lardan o'chirish muvaffaqiyatsiz",
            //         message = errorMessage.Trim(),
            //         faceResults = faceDeleteResults,
            //         failedDevices = failedDevices
            //     });
            // }

            // Vaqtincha faqat bazadan o'chiramiz (device'lardan o'chirilmaydi)
            _context.SkudMyUserinfo.Remove(skudMyUserinfo);
            await _context.SaveChangesAsync();

            Console.WriteLine($"ℹ️ User bazadan o'chirildi (device'lardan o'chirish vaqtincha o'chirilgan)");

            return Ok(new
            {
                user = skudMyUserinfo,
                faceResults = new List<object>(), // Bo'sh natija
                message = "Muvaffaqiyatli o'chirildi (device'lardan o'chirish vaqtincha o'chirilgan)"
            });
            // ========== VAQTINCHA O'CHIRILGAN: Device'lardan o'chirish ==========
        }

        private bool SkudMyUserinfoExists(long id)
        {
            return _context.SkudMyUserinfo.Any(e => e.badgenumber == id);
        }
    }
}
