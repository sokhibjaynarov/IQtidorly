using IQtidorly.Api.Helpers;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace IQtidorly.Api.Middlewares
{
    public class SetLanguageMiddleware
    {
        private readonly RequestDelegate _next;

        public SetLanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRequestLanguageHelper languageHelper)
        {
            var language = context.Request.Headers["Accept-Language"].FirstOrDefault() ?? "uz_UZ";
            languageHelper.PreferredLanguage = language;

            await _next(context);
        }
    }

}
