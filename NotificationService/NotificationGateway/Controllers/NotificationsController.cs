using Microsoft.AspNetCore.Mvc;
using NotificationGateway.Models;
using NotificationGateway.Services;
using System.Collections.Generic;

namespace NotificationGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly RabbitMQPublisher _emailPublisher;
        private readonly RabbitMQPublisher _smsPublisher;
        private readonly RabbitMQPublisher _pushPublisher;

        // قائمة لتخزين الطلبات في الذاكرة
        private static readonly List<NotificationRequest> _storedRequests = new List<NotificationRequest>();

        public NotificationsController()
        {
            _emailPublisher = new RabbitMQPublisher("email_queue");
            _smsPublisher = new RabbitMQPublisher("sms_queue");
            _pushPublisher = new RabbitMQPublisher("push_queue");
        }

        [HttpPost]
        public IActionResult SendNotification([FromBody] NotificationRequest request)
        {
            // حفظ الطلب الوارد في القائمة
            _storedRequests.Add(request);

            if (request.Type == "email")
            {
                _emailPublisher.Publish(request);
            }
            else if (request.Type == "sms")
            {
                _smsPublisher.Publish(request);
            }
            else if (request.Type == "push")
            {
                _pushPublisher.Publish(request);
            }
            else
            {
                return BadRequest("Invalid notification type.");
            }

            return Ok("Notification sent and stored successfully!");
        }

        // Endpoint جديد لعرض الطلبات المخزنة
        [HttpGet("stored-requests")]
        public IActionResult GetStoredRequests()
        {
            return Ok(_storedRequests);
        }
    }
}
