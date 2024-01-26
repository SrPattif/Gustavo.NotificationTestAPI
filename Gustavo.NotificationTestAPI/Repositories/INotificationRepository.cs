using Gustavo.NotificationTestAPI.Model;

namespace Gustavo.NotificationTestAPI.Repositories
{
    public interface INotificationRepository
    {

        Task<List<Notification>> GetAllAsync();
        Task<Notification> GetAsync(int id);
        Task<List<Notification>> GetUserAllNotificationsAsync(int userId);
        Task<int> SaveAsync(Notification notification);
        Task<int?> UpdateAsync(Notification notification);
        Task<int> DeleteAsync(int id);
    }
}
