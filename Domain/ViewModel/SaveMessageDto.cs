using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class SaveMessageDto
    {
        public Guid userId { get; set; }
        public string content { get; set; }
        public DateTime sendDate { get; set; }
        public string messageStatusId {  get; set; }
        public string chatId { get; set; }

    }
}
