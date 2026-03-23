using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAll.Contex;
using ApiAll.Model.tegirmon;

namespace ApiAll.Controllers.tegirmon
{
    [ApiExplorerSettings(GroupName = "v7")]
    [Route("api/[controller]")]
    [ApiController]
    public class TegirmonHikvisionCameraController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TegirmonHikvisionCameraController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TegirmonHikvisionCamera
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TegirmonHikvisionCamera>>> GetTegirmonHikvisionCamera()
        {
            return await _context.TegirmonHikvisionCamera.ToListAsync();
        }

        // GET: api/TegirmonHikvisionCamera/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TegirmonHikvisionCamera>> GetTegirmonHikvisionCamera(long id)
        {
            var hikvisionCamera = await _context.TegirmonHikvisionCamera.FindAsync(id);

            if (hikvisionCamera == null)
            {
                return NotFound();
            }

            return hikvisionCamera;
        }

        // PUT: api/TegirmonHikvisionCamera/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTegirmonHikvisionCamera(long id, TegirmonHikvisionCamera hikvisionCamera)
        {
            if (id != hikvisionCamera.id)
            {
                return BadRequest();
            }

            _context.Entry(hikvisionCamera).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TegirmonHikvisionCameraExists(id))
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

        // POST: api/TegirmonHikvisionCamera
        [HttpPost]
        public async Task<ActionResult<TegirmonHikvisionCamera>> PostTegirmonHikvisionCamera(TegirmonHikvisionCamera hikvisionCamera)
        {
            _context.TegirmonHikvisionCamera.Update(hikvisionCamera);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTegirmonHikvisionCamera", new { id = hikvisionCamera.id }, hikvisionCamera);
        }

        // DELETE: api/TegirmonHikvisionCamera/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TegirmonHikvisionCamera>> DeleteTegirmonHikvisionCamera(long id)
        {
            var hikvisionCamera = await _context.TegirmonHikvisionCamera.FindAsync(id);
            if (hikvisionCamera == null)
            {
                return NotFound();
            }

            _context.TegirmonHikvisionCamera.Remove(hikvisionCamera);
            await _context.SaveChangesAsync();

            return hikvisionCamera;
        }

        [HttpPost("snapshot/{id}")]
        public async Task<ActionResult> TakeSnapshot(long id)
        {
            var camera = await _context.TegirmonHikvisionCamera.FindAsync(id);
            if (camera == null)
            {
                return NotFound();
            }

            var imageBytes = await FetchSnapshotBytes(camera);
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return StatusCode(502, "Camera snapshot failed");
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "camera");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = "hikvision_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".jpg";
            var filePath = Path.Combine(folderPath, fileName);
            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            return Ok(new { image_url = "camera/" + fileName });
        }

        [HttpPost("snapshot_all")]
        public async Task<ActionResult> TakeSnapshotAll()
        {
            var cameras = await _context.TegirmonHikvisionCamera.ToListAsync();
            if (cameras == null || cameras.Count == 0)
            {
                return NotFound();
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "camera");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var results = new List<object>();
            foreach (var camera in cameras)
            {
                var imageBytes = await FetchSnapshotBytes(camera);
                if (imageBytes == null || imageBytes.Length == 0)
                {
                    results.Add(new { camera_id = camera.id, success = false, image_url = "" });
                    continue;
                }

                var fileName = "hikvision_" + camera.id + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".jpg";
                var filePath = Path.Combine(folderPath, fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                results.Add(new { camera_id = camera.id, success = true, image_url = "camera/" + fileName });
            }

            return Ok(results);
        }

        [HttpGet("live/{id}")]
        public async Task<ActionResult> LiveStream(long id)
        {
            var camera = await _context.TegirmonHikvisionCamera.FindAsync(id);
            if (camera == null)
            {
                return NotFound();
            }

            var baseUrl = camera.ip?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return BadRequest("Camera ip is empty");
            }
            if (!baseUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                !baseUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                baseUrl = "http://" + baseUrl;
            }

            var handler = new HttpClientHandler
            {
                PreAuthenticate = true,
                Credentials = new NetworkCredential(camera.username, camera.password),
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            using (var client = new HttpClient(handler))
            {
                client.Timeout = TimeSpan.FromSeconds(10);

                var primaryUrl = $"{baseUrl}/ISAPI/Streaming/channels/101/httpPreview";
                var fallbackUrl = $"{baseUrl}/ISAPI/Streaming/channels/1/httpPreview";

                var response = await client.GetAsync(primaryUrl, HttpCompletionOption.ResponseHeadersRead);
                if (!response.IsSuccessStatusCode)
                {
                    response.Dispose();
                    response = await client.GetAsync(fallbackUrl, HttpCompletionOption.ResponseHeadersRead);
                }

                if (!response.IsSuccessStatusCode)
                {
                    var status = (int)response.StatusCode;
                    response.Dispose();
                    return StatusCode(status, "Camera live stream failed");
                }

                Response.Headers["Cache-Control"] = "no-cache";
                Response.ContentType = response.Content.Headers.ContentType?.ToString() ?? "multipart/x-mixed-replace";

                await response.Content.CopyToAsync(Response.Body);
                response.Dispose();
                return new EmptyResult();
            }
        }

        private bool TegirmonHikvisionCameraExists(long id)
        {
            return _context.TegirmonHikvisionCamera.Any(e => e.id == id);
        }

        private async Task<byte[]> FetchSnapshotBytes(TegirmonHikvisionCamera camera)
        {
            var baseUrl = camera.ip?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return null;
            }
            if (!baseUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                !baseUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                baseUrl = "http://" + baseUrl;
            }

            var handler = new HttpClientHandler
            {
                PreAuthenticate = true,
                Credentials = new NetworkCredential(camera.username, camera.password)
            };

            using (var client = new HttpClient(handler))
            {
                client.Timeout = TimeSpan.FromSeconds(10);

                var primaryUrl = $"{baseUrl}/ISAPI/Streaming/channels/101/picture";
                var fallbackUrl = $"{baseUrl}/ISAPI/Streaming/channels/1/picture";

                var bytes = await TryDownloadSnapshot(client, primaryUrl);
                if (bytes != null && bytes.Length > 0)
                {
                    return bytes;
                }

                return await TryDownloadSnapshot(client, fallbackUrl);
            }
        }

        private static async Task<byte[]> TryDownloadSnapshot(HttpClient client, string url)
        {
            try
            {
                using (var response = await client.GetAsync(url))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }

                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
