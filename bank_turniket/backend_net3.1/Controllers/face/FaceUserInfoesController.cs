using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.skud;
using System.Net.Http;
using System.Net;
using System.Text;

namespace ApiAll.Controllers.face
{
    [ApiExplorerSettings(GroupName = "v12")]
    [Route("api/[controller]")]
    [ApiController]
    public class FaceUserInfoesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public FaceUserInfoesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/FaceUserInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FaceUserInfo>>> GetFaceUserInfo()
        {
            return await _context.FaceUserInfo.ToListAsync();
        }

        // GET: api/FaceUserInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FaceUserInfo>> GetFaceUserInfo(long id)
        {
            var faceUserInfo = await _context.FaceUserInfo.FindAsync(id);

            if (faceUserInfo == null)
            {
                return NotFound();
            }

            return faceUserInfo;
        }

        // PUT: api/FaceUserInfoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaceUserInfo(long id, FaceUserInfo faceUserInfo)
        {
            if (id != faceUserInfo.id)
            {
                return BadRequest();
            }

            _context.Entry(faceUserInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaceUserInfoExists(id))
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

        // POST: api/FaceUserInfoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FaceUserInfo>> PostFaceUserInfo(FaceUserInfo faceUserInfo)
        {
            _context.FaceUserInfo.Update(faceUserInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFaceUserInfo", new { id = faceUserInfo.id }, faceUserInfo);
        }

        [HttpPost("addStudentPost")]
        public async Task<ActionResult<FaceUserInfo>> addStudentPost(FaceUserInfo faceUserInfo)
        {

            // 0 CHECK TO USER EXITS OR NOT
            // TEMPDAN TEKSHIRILDI
            List<FaceUserInfo> faceUserList = await _context.FaceUserInfo.Where(p=>p.student_id == faceUserInfo.student_id).ToListAsync();
            if (faceUserList != null && faceUserList.Count() > 0) {
                return StatusCode(409);
            }
            //ASOSIYDAN TEKSHIRLDI
            List<SkudMyUserinfo> skudMyUserinfos = await _context.SkudMyUserinfo.Where(p => p.student_id == faceUserInfo.student_id).ToListAsync();
            if (skudMyUserinfos != null && skudMyUserinfos.Count() > 0)
            {
                return StatusCode(409);
            }

            //1 DEVICEGA QOSHISH
            //EMP QOSHISH DEVICGA

            /*ODAM QOSHISH DEVICEGA*/


            List<SkudDoors> skudDoorsList = await _context.SkudDoors.ToListAsync();
            if (skudDoorsList != null && skudDoorsList.Count > 0)
            {

                foreach (SkudDoors skudDoors in skudDoorsList)
                {

                    String api = "/ISAPI/AccessControl/UserInfo/SetUp?format=json";
                    String user = "";
                    String password = "";
                    String host_addr = "";
                    long emp_nomer = (long)faceUserInfo.student_id;
                    String emp_name = faceUserInfo.last_name;

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
                        


                    }
                    catch (HttpRequestException e)
                    {
                        return StatusCode(404);
                    }


                }


            }


            //FACE UCHUN BIBLATEKALAR YARATILDI
            if (skudDoorsList != null && skudDoorsList.Count > 0)
            {

                foreach (SkudDoors skudDoors in skudDoorsList)
                {

                    String api = "/ISAPI/Intelligent/FDLib?format=json";
                    String user = "";
                    String password = "";
                    String host_addr = "";
                    long emp_nomer = (long)faceUserInfo.student_id;
                    String emp_name = faceUserInfo.last_name;

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
                        var answer = await httpClient.GetAsync(full_url);
                        var responseString = await answer.Content.ReadAsStringAsync();



                    }
                    catch (HttpRequestException eror)
                    {
                        Console.WriteLine(eror);
                        return StatusCode(404);
                    }


                }


            }

            //EMP YUZI  QOSHILDI
            if (skudDoorsList != null && skudDoorsList.Count > 0)
            {

                foreach (SkudDoors skudDoors in skudDoorsList)
                {

                    String api = "/ISAPI/Intelligent/FDLib/FDSetUp?format=json";
                    String user = "";
                    String password = "";
                    String host_addr = "";
                    long emp_nomer = (long)faceUserInfo.student_id;
                    String emp_name = faceUserInfo.last_name;

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
                        var stringContent = new StringContent("{\r\n  \"faceURL\": " +
                            "\""+faceUserInfo.image+"\"," +
                            "\r\n  \"faceLibType\": \"blackFD\", " +
                            "\r\n  \"FDID\": \"1\"," +
                            "\r\n  \"FPID\": \""+faceUserInfo.student_id+"\"," +
                            "\r\n  \"name\": \""+faceUserInfo.last_name+"\"," +
                            "\r\n  \"bornTime\": \"1999-05-24\"" +
                            "\r\n}", UnicodeEncoding.UTF8, "application/json");
                        var answer = await httpClient.PutAsync(new Uri(full_url), stringContent);

                        var responseString = await answer.Content.ReadAsStringAsync();



                    }
                    catch (HttpRequestException e)
                    {
                        return StatusCode(404);
                    }


                }


            }




            //2 TEPGA QOSHISH
            _context.FaceUserInfo.Update(faceUserInfo);
            await _context.SaveChangesAsync();

            //3 ASOSIYGA QOSHISH
            SkudMyUserinfo myUserinfo = new SkudMyUserinfo();
            myUserinfo.ism = faceUserInfo.first_name;
            myUserinfo.familiya = faceUserInfo.last_name;
            myUserinfo.middle_name = faceUserInfo.middle_name;
            myUserinfo.student_id= faceUserInfo.student_id;
            myUserinfo.organization_id= faceUserInfo.organization_id;
            myUserinfo.group_id= faceUserInfo.group_id;
            myUserinfo.group_name= faceUserInfo.group_name;
            myUserinfo.image_url = faceUserInfo.image;
            myUserinfo.lesson_end= faceUserInfo.lesson_end;
            myUserinfo.lesson_start = faceUserInfo.lesson_start;

            _context.SkudMyUserinfo.Update(myUserinfo);
            await _context.SaveChangesAsync();



            return StatusCode(201);
        }

        // DELETE: api/FaceUserInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FaceUserInfo>> DeleteFaceUserInfo(long id)
        {
            var faceUserInfo = await _context.FaceUserInfo.FindAsync(id);
            if (faceUserInfo == null)
            {
                return NotFound();
            }

            _context.FaceUserInfo.Remove(faceUserInfo);
            await _context.SaveChangesAsync();

            return faceUserInfo;
        }

        private bool FaceUserInfoExists(long id)
        {
            return _context.FaceUserInfo.Any(e => e.id == id);
        }
    }
}
