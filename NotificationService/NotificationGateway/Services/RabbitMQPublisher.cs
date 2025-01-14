using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using NotificationGateway.Models;

namespace NotificationGateway.Services
{
    public class RabbitMQPublisher
    {
        private readonly string _hostName = "localhost";
        private readonly string _queueName;
        private readonly EmailSender _emailSender;

        public RabbitMQPublisher(string queueName)
        {
            _queueName = queueName;
            _emailSender = new EmailSender(); // إنشاء كائن EmailSender
        }

        public void Publish(NotificationRequest request)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var message = JsonSerializer.Serialize(request);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);

            // إرسال الإيميل إذا كان نوع الإشعار "email"
            if (_queueName == "email_queue")
            {
                _emailSender.SendEmail(request.Recipient, "Notification", request.Message);
            }
        }
    }
}
