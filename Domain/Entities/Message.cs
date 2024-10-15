using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Message
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string content { get; set; }

        [Required]
        public DateTime send_date { get; set; }
        [Required]
        public int message_statusid { get; set; }
        public Message_status MessageStatus { get; set; }

        [Required]
        public int userid { get; set; }
        public User User { get; set; }
        [Required]
        public int chatid { get; set; }
        public Chat Chat { get; set; }

        public List<Attachment> Attachments { get; set; }

    }
}
