using System;
using System.Collections.Generic;

namespace IQtidorly.Api.ViewModels.Users
{
    public class UserInfoViewModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid FileId { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
