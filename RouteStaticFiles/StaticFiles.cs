using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RouteStaticFiles
{
    public class StaticFiles
    {
        internal static async Task ReturnStaticFile(HttpContext context, PathString filePath)
        {
            FileInfo file;

            switch (filePath)
            {
                case "/":
                    file = new FileInfo($"wwwroot/index.html");
                    break;

                case "/index":
                    file = new FileInfo($"wwwroot/index.html");
                    break;

                default:
                    file = new FileInfo($"wwwroot{filePath}");
                    break;
            }

            byte[] buffer;
            if (file.Exists)
            {
                buffer = File.ReadAllBytes(file.FullName);

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "text/html";
                context.Response.ContentLength = buffer.Length;
                context.Response.Headers.Add("FileName", file.Name);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "text/plain";

                buffer = Encoding.UTF8.GetBytes("Not Found, 404");
            }

            using (var stream = context.Response.Body)
            {
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.FlushAsync();
            }
        }
    }
}
