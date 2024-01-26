using Gustavo.NotificationTestAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gustavo.NotificationTestAPI.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {

        private readonly INotificationRepository _notificationRepo;
        public NotificationsController(INotificationRepository _notificationRepo)
        {
            this._notificationRepo = _notificationRepo;
        }

        // GET: api/<NotificationsController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var notificationsList = await _notificationRepo.GetAllAsync();
            return Ok(new { success = true, data = notificationsList });
        }

        // GET: api/<NotificationsController>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult> GetUserNotifications(int userId)
        {
            var userNotifications = await _notificationRepo.GetUserAllNotificationsAsync(userId);
            return Ok(new { success = true, data = userNotifications });
        }

        // GET api/<NotificationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NotificationsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NotificationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NotificationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
