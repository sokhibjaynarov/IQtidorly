using Microsoft.EntityFrameworkCore;
using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IQtidorly.Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDbContext DbContext { get; set; }
        public DbSet<User> Entities { get => DbContext.Set<User>(); }

        public UserRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<User> AddAsync(User entity)
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

        public virtual async Task AddRangeAsync(IEnumerable<User> entities)
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

        public virtual IQueryable<User> GetAll(bool includeRemovedEntities = false)
        {
            if (typeof(User).GetInterfaces().Contains(typeof(IEntityPersistent)) && includeRemovedEntities == false)
            {
                return Entities.Where(l => (l as IEntityPersistent).EntityState == Enums.EntityState.Active);
            }

            return Entities;
        }

        public async Task<User> GetAsync(Guid id, bool includeRemovedEntities = false)
        {
            var entity = await Entities.FindAsync(id);

            if (entity != null && entity is IEntityPersistent && (entity as IEntityPersistent).EntityState == Enums.EntityState.Inactive && includeRemovedEntities == false)
            {
                return null;
            }

            return entity;
        }

        public virtual void Remove(User entity)
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

        public virtual void RemoveRange(IEnumerable<User> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new Exception("Null entities can not be removed");
            }

            foreach (User entity in entities)
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

        public virtual User Update(User entity)
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

        public virtual void UpdateRange(IEnumerable<User> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new Exception("Null entities can not be updated");
            }

            foreach (User entity in entities)
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
