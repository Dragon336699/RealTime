using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Chat
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string chat_name { get; set; }
        [Required]
        public Boolean is_group { get; set; }
        [Required]
        public DateTime created_at { get; set; }
        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }
    }
}
