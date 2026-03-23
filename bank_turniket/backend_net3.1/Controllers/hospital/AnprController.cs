using System.Text;
using System;
using System.IO;
using System.Threading.Tasks; // 👈 BU MUHIM!
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Linq;

// using System.Xml.Linq;

namespace ApiAll.Controllers
{
    [ApiController]
    [Route("anpr")]
    public class AnprController : ControllerBase
    {

        [HttpPost("upload")]
        public async Task<IActionResult> UploadRaw()
        {
            string body;
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                body = await reader.ReadToEndAsync();
            }

            var contentType = Request.ContentType;

            Console.WriteLine($"Content-Type: {contentType}");
            Console.WriteLine($"Body:\n{body}");

            // try
            // {
            //     // XML ni o'qish
            //     XDocument doc = XDocument.Parse(body);

            //     // Namespace aniqlash
            //     XNamespace ns = "http://www.isapi.org/ver20/XMLSchema";

            //     // licensePlate elementini olish
            //     var licensePlate = doc.Root
            //         .Element(ns + "ANPR")
            //         .Element(ns + "licensePlate")
            //         .Value;

            //     Console.WriteLine("License Plate: " + licensePlate);
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine("XML parsing yoki element topishda xato: " + ex.Message);
            // }

            return Ok("Qabul qilindi");
        }
        // [HttpPost("upload")]
        // public async Task<IActionResult> Upload()
        // {
        //     using var reader = new StreamReader(Request.Body, Encoding.UTF8);
        //     var body = await reader.ReadToEndAsync();

        //     try
        //     {
        //         var xml = XDocument.Parse(body);
        //         var plate = xml.Descendants("licensePlate").FirstOrDefault()?.Value ?? "Topilmadi";

        //         Console.WriteLine($"[KAMERA] Raqam: {plate}");

        //         return Ok("Ma'lumot qabul qilindi");
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"[XATO] XML o‘qishda xatolik: {ex.Message}");
        //         return BadRequest("XML format xato");
        //     }
        // }
    }
}








// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Http;
// using System.IO;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Xml.Linq;

// namespace ApiAll.Controllers
// {
//     [ApiController]
//     [Route("anpr")]
//     public class AnprController : ControllerBase
//     {
//         private readonly ApplicationContext _context;

//         public AnprController(ApplicationContext context)
//         {
//             _context = context;
//         }

//         [HttpPost("upload")]
//         [DisableRequestSizeLimit] // 🛡 Katta fayllar uchun cheklovni olib tashlash
//         public async Task<IActionResult> UploadForm()
//         {
//             var form = await Request.ReadFormAsync();

//             var xmlFile = form.Files.FirstOrDefault(f => f.FileName.EndsWith(".xml"));
//             if (xmlFile == null)
//             {
//                 return BadRequest("XML fayl topilmadi");
//             }

//             string xmlContent;
//             using (var reader = new StreamReader(xmlFile.OpenReadStream()))
//             {
//                 xmlContent = await reader.ReadToEndAsync();
//             }

//             try
//             {
//                 var doc = XDocument.Parse(xmlContent);
//                 XNamespace ns = "http://www.isapi.org/ver20/XMLSchema";

//                 var plate = doc.Descendants(ns + "licensePlate").FirstOrDefault()?.Value;

//                 if (!string.IsNullOrWhiteSpace(plate))
//                 {
//                     System.Console.WriteLine($"🚗 Mashina raqami: {plate}");

//                     // Rasmlarni saqlash joyi
//                         string imageFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "images");
//                         Directory.CreateDirectory(imageFolder);

//                         string fileName = $"car_{DateTime.UtcNow.Ticks}.jpg";
//                         string fullPath = Path.Combine(imageFolder, fileName);

//                         using (var stream = new FileStream(fullPath, FileMode.Create))
//                         {
//                             await imageFile.CopyToAsync(stream);
//                         }

//                     // Bazaga yozish
//                         var newCar = new ParkingCarList
//                         {
//                             type = "I",
//                             car_plate = plate,
//                             image_name = fileName,
//                             created_date_time = DateTime.Now
//                         };

//                         _context.ParkingCarLists.Add(newCar);
//                         await _context.SaveChangesAsync();
                    
//                     return Ok(new { message = "Qabul qilindi", plate });
//                 }




    

//                 return BadRequest("Mashina raqami topilmadi");
//             }
//             catch (System.Exception ex)
//             {
//                 System.Console.WriteLine("❌ XML parsing xatolik: " + ex.Message);
//                 return BadRequest("Xato XML format");
//             }
//         }
//     }
// }