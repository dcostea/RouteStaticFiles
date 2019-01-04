using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RouteStaticFiles
{
    public class StaticFilesMiddleware
    {
        private readonly RequestDelegate _next;

        public StaticFilesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.HasValue)
            {
                await StaticFiles.ReturnStaticFile(context, context.Request.Path.Value);
                return;
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
