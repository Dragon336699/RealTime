using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace RealTimeChat.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient client = new SmtpClient()
            {
                Port = 587,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Credentials = new NetworkCredential("anhruoia1a1@gmail.com", "zobokdpklisuiiqq"),
            };

            return client.SendMailAsync("anhruoia1a1@gmail.com", email, subject, htmlMessage);
        }
    }
}
