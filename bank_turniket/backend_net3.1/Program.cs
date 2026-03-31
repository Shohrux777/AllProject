using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace ApiAll
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //NewtonsoftJson
            var PathWithFolderName = Path.Combine(Directory.GetCurrentDirectory(), "images");
            

            try
            {
                if (!Directory.Exists(PathWithFolderName))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(PathWithFolderName);

                }
            }
            catch (Exception) { 
            
            }



            


           // var firstDayOfMonth = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, 1);
           // var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            /*Console.WriteLine(firstDayOfMonth.ToString());
            Console.WriteLine(lastDayOfMonth.AddHours(23.9999).ToString());
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
            Console.WriteLine(DateTime.Now.AddYears(2).ToString("yyyy-MM-ddTHH:mm:ss"));
            Console.WriteLine(DateTime.Now.Date.AddHours(4) +"  ERTALABGI TORT");
            Console.WriteLine(DateTime.Now.Date.AddHours(4).AddDays(1) +"  ERTALABGI TORT ERTANSIKUN");
            */
            // string page_result = LoadHttpPageWithDigestAuthentication("http://192.168.1.102/ISAPI/AccessControl/AcsEvent/capabilities?format=json", "admin","asd12345");
            //Console.WriteLine(page_result);


            // await getMethodDgsAsync();

            //await postMethodDgsAsync();
           // await postMethodDgsAsync();

            CreateHostBuilder(args).Build().Run();
        }

        public static async Task getMethodDgsAsync() {
            var domain = "http://192.168.1.102/ISAPI/AccessControl/AcsEvent/capabilities?format=json";
            var credCache = new CredentialCache();
            credCache.Add(new Uri(domain), "Digest", new NetworkCredential("admin", "asd12345"));
            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
            var answer = await httpClient.GetAsync(new Uri(domain));
            var responseString = await answer.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);

        }

        public static async Task putMethodDgsAsync()
        {

            try
            {
                var domain = "http://192.168.1.102/ISAPI/AccessControl/UserInfo/Search?format=json";
                var credCache = new CredentialCache();
                credCache.Add(new Uri(domain), "Digest", new NetworkCredential("admin", "asd12345"));
                var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });

                var stringContent = new StringContent("{\r\n  \"UserInfoSearchCond\":{\r\n    \"searchID\":\"1\",\r\n    \"searchResultPosition\": 0,\r\n    \"maxResults\":1500\r\n  }\r\n}", UnicodeEncoding.UTF8, "application/json");

                var answer = await httpClient.PostAsync(new Uri(domain), stringContent);
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
                            Console.WriteLine(employeeNo + " " + name);
                        }

                    }
                    catch (JsonReaderException e)
                    {
                        Console.WriteLine("JSON ERROR: " + e.Message);
                    }

                    // Console.WriteLine(responseString);


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
        }

        public static async Task postMethodDgsAsync() {

            try {
                String begin_date_str = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                String end_date_str =  DateTime.Now.AddYears(2).ToString("yyyy-MM-ddTHH:mm:ss");
                var domain = "http://192.168.1.102/ISAPI/AccessControl/UserInfo/Modify?format=json";
                var credCache = new CredentialCache();
                credCache.Add(new Uri(domain), "Digest", new NetworkCredential("admin", "asd12345"));
                var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
                var stringContent = new StringContent("{ \r\n  \"UserInfo\":{ \r\n    \"employeeNo\":\"00000001\", \r\n    \"deleteUser\":false , \r\n    \"name\":\"BEGALI\", \r\n    \"userType\":\"normal\", \r\n    \"Valid\":{ \r\n       \"enable\":true, \r\n       \"beginTime\":\"" + begin_date_str+"\", \r\n       \"endTime\":\""+end_date_str+"\", \r\n       \"timeType\":\"local\" \r\n    }, \r\n    \"checkUser\":true , \r\n    \"addUser\": false \r\n    } \r\n   \r\n}", UnicodeEncoding.UTF8, "application/json"); 
                var answer = await httpClient.PutAsync(new Uri(domain), stringContent);
                if (answer.IsSuccessStatusCode)
                {
                    var responseString = await answer.Content.ReadAsStringAsync();
                    Console.WriteLine(responseString);
                }
                else {
                    Console.WriteLine("ERROR");
                    var responseString = await answer.Content.ReadAsStringAsync();
                    Console.WriteLine(responseString);
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public static string LoadHttpPageWithDigestAuthentication(string url, string username, string password)
        {
            Uri myUri = new Uri(url);
            WebRequest myWebRequest = HttpWebRequest.Create(myUri);

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)myWebRequest;

            NetworkCredential myNetworkCredential = new NetworkCredential(username, password);

            CredentialCache myCredentialCache = new CredentialCache();
            myCredentialCache.Add(myUri, "Digest", myNetworkCredential);

            myHttpWebRequest.PreAuthenticate = true;
            myHttpWebRequest.Credentials = myCredentialCache;

            WebResponse myWebResponse = myWebRequest.GetResponse();

            Stream responseStream = myWebResponse.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

            string pageContent = myStreamReader.ReadToEnd();

            responseStream.Close();

            myWebResponse.Close();

            return pageContent;
        }


        // public static IHostBuilder CreateHostBuilder(string[] args) =>
        //     Host.CreateDefaultBuilder(args)
        //         .ConfigureWebHostDefaults(webBuilder =>
        //         {
        //             webBuilder.UseStartup<Startup>();
        //             webBuilder.UseKestrel(opts =>
        //             {
        //                 opts.Listen(IPAddress.Loopback, port: 5002);
        //                 opts.ListenAnyIP(5003);
        //                 opts.ListenLocalhost(5004, opts => opts.UseHttps());
        //                 opts.ListenLocalhost(5005, opts => opts.UseHttps());
        //                 opts.ListenAnyIP(5006, opts => opts.UseHttps());
        //             });
        //         });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseKestrel(opts =>
                {
                    // Oddiy HTTP portlar
                    opts.Listen(IPAddress.Loopback, port: 5010);
                    opts.ListenAnyIP(5011);

                    // HTTPS - localhost
                    opts.ListenAnyIP(5012, listenOptions =>
                    {
                        listenOptions.UseHttps("tegirmon.pfx", "1234");
                    });

                    // HTTPS - boshqa IP (local tarmoqdan kiradiganlar uchun)
                    opts.ListenAnyIP(5013, listenOptions =>
                    {
                        listenOptions.UseHttps("tegirmon.pfx", "1234");
                    });
                });
            });

    }
}
