using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Attachment
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string file_url { get; set; }
        [Required]
        public int file_size { get; set; }
        [Required]
        public int messageid { get; set; }
        public Message Message { get; set; }
    }
}
