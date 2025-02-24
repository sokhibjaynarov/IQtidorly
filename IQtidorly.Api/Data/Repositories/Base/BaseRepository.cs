using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IQtidorly.Api.Data.Repositories.Base
{
    public class BaseRepository<TEntity, TDbContext> : IBaseRepository<TEntity, TDbContext> where TEntity : BaseModel where TDbContext : DbContext
    {

        public TDbContext DbContext { get; set; }
        public DbSet<TEntity> Entities { get => DbContext.Set<TEntity>(); }

        public BaseRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("Null value can not be added");
            }

            if (entity is IEntityAuthorship && (entity as IEntityAuthorship).CreatedById == Guid.Empty)
            {
                throw new Exception("Entity implemented from IEntityAuthorship should have valid CreatedById");
            }

            var entry = await DbContext.AddAsync(entity);

            return entry.Entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new Exception("Null value can not be added");
            }

            foreach (var entity in entities)
            {
                if (entity is IEntityAuthorship && (entity as IEntityAuthorship).CreatedById == Guid.Empty)
                {
                    throw new Exception("Entity implemented from IEntityAuthorship should have valid CreatedById");
                }
            }

            await DbContext.AddRangeAsync(entities);
        }

        public virtual IQueryable<TEntity> GetAll(bool includeRemovedEntities = false)
        {

            if (typeof(TEntity).GetInterfaces().Contains(typeof(IEntityPersistent)) && includeRemovedEntities == false)
            {
                return Entities.Where(l => (l as IEntityPersistent).EntityState == Enums.EntityState.Active);
            }

            return Entities;
        }

        public async Task<TEntity> GetAsync(Guid id, bool includeRemovedEntities = false)
        {
            var entity = await Entities.FindAsync(id);

            if (entity != null && entity is IEntityPersistent && (entity as IEntityPersistent).EntityState == Enums.EntityState.Inactive && includeRemovedEntities == false)
            {
                return null;
            }

            return entity;
        }

        public virtual void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("Null entity can not be removed");
            }

            if (entity is IEntityPersistent)
            {
                if (entity is IEntityAuthorship && (entity as IEntityAuthorship).InactivatedById == Guid.Empty)
                {
                    throw new Exception("Entity implemented from IEntityAuthorship should have valid InactivatedById");
                }

                (entity as IEntityPersistent).EntityState = Enums.EntityState.Inactive;
                (entity as IEntityPersistent).InactivatedDate = DateTime.UtcNow;
                DbContext.Update(entity);
            }
            else
            {
                DbContext.Remove(entity);
            }
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new Exception("Null entities can not be removed");
            }

            foreach (TEntity entity in entities)
            {
                if (entity is IEntityPersistent)
                {
                    if (entity is IEntityAuthorship && (entity as IEntityAuthorship).InactivatedById == Guid.Empty)
                    {
                        throw new Exception("Entity implemented from IEntityAuthorship should have valid InactivatedById");
                    }

                    (entity as IEntityPersistent).EntityState = Enums.EntityState.Inactive;
                    (entity as IEntityPersistent).InactivatedDate = DateTime.UtcNow;

                    DbContext.Update(entity);
                }
                else
                {
                    DbContext.Remove(entity);
                }
            }
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("Null entity can not be updated");
            }

            if (entity is IEntityPersistent)
            {
                if ((entity as IEntityPersistent).EntityState == Enums.EntityState.Inactive)
                {
                    if (entity is IEntityAuthorship && (entity as IEntityAuthorship).InactivatedById == Guid.Empty)
                    {
                        throw new Exception("Entity implemented from IEntityAuthorship should have valid InactivatedById");
                    }

                    (entity as IEntityPersistent).InactivatedDate = DateTime.UtcNow;
                }
            }

            entity.LastModifiedDate = DateTime.UtcNow;

            var entry = DbContext.Update(entity);

            return entry.Entity;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new Exception("Null entities can not be updated");
            }

            foreach (TEntity entity in entities)
            {
                if (entity is IEntityPersistent)
                {
                    if ((entity as IEntityPersistent).EntityState == Enums.EntityState.Inactive)
                    {
                        if (entity is IEntityAuthorship && (entity as IEntityAuthorship).InactivatedById == Guid.Empty)
                        {
                            throw new Exception("Entity implemented from IEntityAuthorship should have valid InactivatedById");
                        }

                        (entity as IEntityPersistent).InactivatedDate = DateTime.UtcNow;
                    }
                }

                entity.LastModifiedDate = DateTime.UtcNow;
            }

            DbContext.UpdateRange(entities);
        }

        //private async Task<List<TEntity>> ApplyTranslationsAsync(IQueryable<TEntity> query, string preferredLanguage)
        //{
        //    // First, execute the query and load the entities into memory.
        //    var entities = await query.ToListAsync();

        //    // Apply translations to each entity after it has been retrieved.
        //    foreach (var entity in entities)
        //    {
        //        ApplyTranslationsToEntity(entity, preferredLanguage);
        //    }

        //    return entities;
        //}

        //private void ApplyTranslationsToEntity(TEntity entity, string preferredLanguage)
        //{
        //    if (entity == null) return;

        //    // Find the translation property on the entity
        //    var translationProperty = entity.GetType().GetProperties()
        //        .FirstOrDefault(p => p.Name.EndsWith("Translation")); // Convention: <Model>Translation

        //    if (translationProperty != null)
        //    {
        //        // Get the translation model from the property
        //        var translationModel = translationProperty.GetValue(entity) as dynamic; // We use dynamic to access properties like 'Name' in the translation model

        //        if (translationModel != null)
        //        {
        //            // Get all properties that have the Translatable attribute
        //            var translatableProperties = entity.GetType().GetProperties()
        //                .Where(p => Attribute.IsDefined(p, typeof(TranslatableAttribute)))
        //                .ToList();

        //            foreach (var property in translatableProperties)
        //            {
        //                // For each translatable property, find its corresponding translation field in the translation model
        //                var translationPropertyName = $"{property.Name}"; // The name of the translatable field, e.g., 'Name'

        //                // Get the value of the translation field for the preferred language
        //                var translatedValue = GetTranslationValue(translationModel, translationPropertyName, preferredLanguage);

        //                if (translatedValue != null)
        //                {
        //                    // Set the translated value to the actual property
        //                    property.SetValue(entity, translatedValue);
        //                }
        //            }
        //        }
        //    }
        //}

        //private string GetTranslationValue(dynamic translationModel, string propertyName, string language)
        //{
        //    // Get the specific property from the translation model (e.g., "Name")
        //    var property = translationModel.GetType().GetProperty(propertyName);

        //    if (property != null)
        //    {
        //        // Access the nested translation object (e.g., TranslationModel for "Name")
        //        var nestedTranslation = property.GetValue(translationModel);

        //        if (nestedTranslation != null)
        //        {
        //            // Get the language-specific value from the nested translation object (e.g., "en_US")
        //            var languageProperty = nestedTranslation.GetType().GetProperty(language);
        //            if (languageProperty != null)
        //            {
        //                return (string)languageProperty.GetValue(nestedTranslation);
        //            }
        //        }
        //    }

        //    // If translation is missing, return null or default behavior
        //    return null;
        //}
    }
}
