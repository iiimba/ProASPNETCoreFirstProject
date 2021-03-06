using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Platform.Services
{
    public class GuidService : IResponseFormatter
    {
        private Guid guid = Guid.NewGuid();

        public Guid MyGuid => guid;

        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"Guid: {guid}\n{content}");
        }
    }
}
