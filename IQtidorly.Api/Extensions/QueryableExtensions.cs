using IQtidorly.Api.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace IQtidorly.Api.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> ApplyTranslations<TEntity>(
        this IQueryable<TEntity> query,
        string preferredLanguage)
        where TEntity : class
        {
            try
            {
                var entityType = typeof(TEntity);

                // 🔹 Find the property that holds translations (JSONB field)
                var translationProperty = entityType.GetProperties()
                    .FirstOrDefault(p => Attribute.IsDefined(p, typeof(TranslationPropertyAttribute)));

                if (translationProperty == null)
                {
                    throw new InvalidOperationException($"No property with [TranslationProperty] attribute found in {entityType.Name}");
                }

                // 🔹 Find all properties marked with [Translatable] that should be translated
                var translatableProperties = entityType.GetProperties()
                    .Where(p => Attribute.IsDefined(p, typeof(TranslatableAttribute)))
                    .ToList();

                return query.Select(entity =>
                {
                    // Create a new object with translated fields
                    var projectedEntity = (TEntity)Activator.CreateInstance(typeof(TEntity));

                    foreach (var prop in entityType.GetProperties())
                    {
                        // Copy original values
                        prop.SetValue(projectedEntity, EF.Property<object>(entity, prop.Name));
                    }

                    // 🔹 Apply translation to each translatable field
                    foreach (var prop in translatableProperties)
                    {
                        var translationField = $"{translationProperty.Name}->{preferredLanguage}->{prop.Name}"; // JSON path

                        var translatedValue = EF.Functions.JsonExtractPathText(
                            EF.Property<string>(entity, translationProperty.Name),
                            preferredLanguage, prop.Name
                        );

                        prop.SetValue(projectedEntity, translatedValue);
                    }

                    return projectedEntity;
                });
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error applying translations", ex);
            }
        }
    }
}
