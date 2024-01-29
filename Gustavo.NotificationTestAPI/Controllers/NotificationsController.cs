using Gustavo.NotificationTestAPI.Model;
using Gustavo.NotificationTestAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

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
        public async Task<ActionResult> GetFromId(int id)
        {
            var notification = await _notificationRepo.GetAsync(id);

            if (notification == null)
            {
                return BadRequest(new { success = false, error_code = "INVALID_NOTIFICATION_ID" });
            }
            return Ok(new { success = true, data = notification });
        }

        // POST api/<NotificationsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NotificationInputModel model)
        {
            try
            {
                var title = model.Title;
                var description = model.Description;
                var interactionUrl = model.InteractionUrl ?? null;
                var imageUrl = model.ImageUrl ?? null;
                var type = model.Type ?? "DEFAULT";
                var displayType = model.DisplayType ?? "ALL_USERS";

                if (title == null || description == null)
                {
                    return BadRequest(new { success = false, error_code = "MISSING_FIELDS" });
                }

                Notification newNotification = new Notification()
                {
                    Title = title,
                    Description = description,
                    InteractionURL = interactionUrl,
                    ImageURL = imageUrl,
                    Type = type,
                    DisplayType = displayType
                };

                var inserted = await _notificationRepo.SaveAsync(newNotification);
                if (inserted)
                {
                    return Ok(new { success = true, data = newNotification });

                }
                else
                {
                    return BadRequest(new { success = false, error_code = "UNKNOWN_DATABASE_ERROR" });
                }
            } catch(Exception ex)
            {
                return BadRequest(new { success = false, error_code = "INTERNAL_ERROR" });
            }
            
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

    public class NotificationInputModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? InteractionUrl { get; set; }
        public string? ImageUrl { get; set; }
        public string Type { get; set; } = "DEFAULT";
        public string DisplayType { get; set; } = "ALL_USERS";
    }
}
