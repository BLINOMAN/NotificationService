using System.Net;
using System.Net.Mail;

namespace NotificationGateway.Services
{
    public class EmailSender
    {
        private readonly string _smtpServer = "bassq3bvq21t.mailtrap.ru";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUsername = "bassq3bvq21t";
        private readonly string _smtpPassword = "bajygvzmwsid";

        public void SendEmail(string recipient, string subject, string body)
        {
            var smtpClient = new SmtpClient(_smtpServer)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                EnableSsl = false,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("example@example.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(recipient);

            smtpClient.Send(mailMessage);
        }
    }
}
