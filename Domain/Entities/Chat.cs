using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Chat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? ChatName { get; set; }
        [Required]
        public Boolean IsGroup { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public List<ChatUser> ChatUsers { get; set; }
        public List<Message> Messages { get; set; }
    }
}
