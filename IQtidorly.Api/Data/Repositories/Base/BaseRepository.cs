﻿using Microsoft.EntityFrameworkCore;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.Base;
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
    }
}
