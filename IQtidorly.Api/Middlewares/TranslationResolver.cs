using AutoMapper;
using IQtidorly.Api.Models.Base;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace IQtidorly.Api.Middlewares
{
    public class TranslationResolver<TSource> : IValueResolver<TSource, object, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _propertyName;

        public TranslationResolver(IHttpContextAccessor httpContextAccessor, string propertyName)
        {
            _httpContextAccessor = httpContextAccessor;
            _propertyName = propertyName;
        }

        public string Resolve(TSource source, object destination, string destMember, ResolutionContext context)
        {
            if (source == null) return null;

            // Get the requested language from the HTTP request
            string language = _httpContextAccessor.HttpContext?.Request.Headers["Accept-Language"].FirstOrDefault() ?? "uz_UZ";

            // Find the Translations property dynamically
            var translatableProperty = typeof(TSource).GetProperty("Translations");
            if (translatableProperty != null)
            {
                var translations = translatableProperty.GetValue(source);
                if (translations != null)
                {
                    // Get the specific translation model property (e.g., "Title" or "Description")
                    var translationProperty = translations.GetType().GetProperty(_propertyName);

                    if (translationProperty != null)
                    {
                        var translationModel = translationProperty.GetValue(translations) as TranslationModel;
                        if (translationModel != null)
                        {
                            return translationModel.GetTranslation(language);
                        }
                    }
                }
            }

            // Fallback to original value if no translation is found
            return typeof(TSource).GetProperty(_propertyName)?.GetValue(source) as string;
        }
    }
}
