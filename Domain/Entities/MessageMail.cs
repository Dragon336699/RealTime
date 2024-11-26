using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MessageMail
    {
        public string To { get; }
        public string Subject { get; }
        public string Body { get; }
        public string? AttachmentPath { get; }

        public MessageMail(string to, string subject, string body, string? attachmentPath = null)
        {
            To = to;
            Subject = subject;
            Body = body;
            AttachmentPath = attachmentPath;
        }
    }
}
