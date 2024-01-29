using System.ComponentModel.DataAnnotations.Schema;

namespace Gustavo.NotificationTestAPI.Model
{
    public class Notification
    {

        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public string? InteractionURL { get; set; }
        public string? ImageURL { get; set; }
        public string? Type { get; set; }
        public string? DisplayType { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPublic { get; set; }
    }
}
