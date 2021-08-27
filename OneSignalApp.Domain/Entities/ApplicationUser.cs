using Microsoft.AspNetCore.Identity;
using System;

namespace OneSignalApp.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTime.UtcNow;
        }
    }
}
