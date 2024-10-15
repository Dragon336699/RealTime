using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Domain.Entities
{
    public class User : IdentityUser<int>
    {
         public List<Chat> Chats {  get; set; }
    }
}