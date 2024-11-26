using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public List<ChatUser> ChatUsers { get; set; }

        public string FirstName {  get; set; }

        public string LastName { get; set; }
    }
}