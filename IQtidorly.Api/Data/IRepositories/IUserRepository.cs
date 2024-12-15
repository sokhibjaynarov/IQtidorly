using Microsoft.EntityFrameworkCore;
using IQtidorly.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IUserRepository
    {
        ApplicationDbContext DbContext { get; set; }
        DbSet<User> Entities { get; }

        IQueryable<User> GetAll(bool includeRemovedEntities = false);
        Task<User> GetAsync(Guid id, bool includeRemovedEntities = false);

        Task AddRangeAsync(IEnumerable<User> entities);
        Task<User> AddAsync(User entity);

        void Remove(User entity);
        void RemoveRange(IEnumerable<User> entities);

        User Update(User entity);
        void UpdateRange(IEnumerable<User> entities);

        Task<int> SaveChangesAsync();
    }
}
