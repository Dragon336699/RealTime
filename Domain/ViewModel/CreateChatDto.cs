using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class CreateChatDto
    {
        public List<Guid> usersId { get; set; }
        public List<string> roles { get; set; }
        public Boolean isGroup { get; set; }
    }
}
