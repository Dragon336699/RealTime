using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ChatUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public Guid ChatId { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string NickName { get; set; }
        public string? Role { get; set; }
        public DateTime? JoinedAt { get; set; } = DateTime.UtcNow;

        public Chat Chat { get; set; }
        public User User { get; set; }


    }
}
