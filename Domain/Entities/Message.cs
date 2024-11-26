using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime SendDate { get; set; }
        [Required]
        public Guid MessageStatusId { get; set; }
        public MessageStatus MessageStatus { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [Required]
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }

        public List<Attachment> Attachments { get; set; }

    }
}
