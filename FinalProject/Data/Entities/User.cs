using Microsoft.AspNetCore.Identity;
using System;

namespace FinalProject.Data.Entities
{
    public class User : IdentityUser
    {

        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
