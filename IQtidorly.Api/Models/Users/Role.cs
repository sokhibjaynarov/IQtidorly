using Microsoft.AspNetCore.Identity;
using System;

namespace IQtidorly.Api.Models.Users
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
            Id = Guid.NewGuid();
        }

        public Role(string roleName) : this()
        {
            Name = roleName;
        }

        public Role(Guid roleId, string roleName)
        {
            Id = roleId;
            Name = roleName;
        }
    }
}
