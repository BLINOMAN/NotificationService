using System.Net;
using System.Net.Mail;

namespace NotificationGateway.Services
{
    public class EmailSender
    {
        private readonly string _smtpServer = "smtp.example.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUsername = "your-email@example.com";
        private readonly string _smtpPassword = "your-email-password";

        public void SendEmail(string recipient, string subject, string body)
        {
            var smtpClient = new SmtpClient(_smtpServer)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUsername),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(recipient);

            smtpClient.Send(mailMessage);
        }
    }
}
