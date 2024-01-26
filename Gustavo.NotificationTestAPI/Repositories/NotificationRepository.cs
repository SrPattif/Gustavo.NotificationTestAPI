using Dapper;
using Gustavo.NotificationTestAPI.Model;

namespace Gustavo.NotificationTestAPI.Repositories
{
    public class NotificationRepository : INotificationRepository
    {

        public readonly DbSession _dbSession;
        public NotificationRepository(DbSession _dbSession) {
            this._dbSession = _dbSession;
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            using(var conn = _dbSession.connection)
            {
                var query = @"SELECT * FROM Notifications;";
                List<Notification> notifications = (await conn.QueryAsync<Notification>(query)).ToList();

                foreach(var notification in notifications)
                {
                    if (notification.DisplayType.Equals("ALL_USERS"))
                    {
                        notification.IsPublic = true;
                        notification.UserId = null;
                    }
                    if(notification.DisplayType.Equals("USER")) notification.IsPublic = false;
                }
                return notifications;
            }
        }

        public async Task<List<Notification>> GetUserAllNotificationsAsync(int userId)
        {
            using (var conn = _dbSession.connection)
            {
                var query = @"SELECT * FROM Notifications WHERE DisplayType='ALL_USERS' OR DisplayType='USER' AND UserId=@UserId ORDER BY CreatedAt DESC;";
                List<Notification> notifications = (await conn.QueryAsync<Notification>(query, new { UserId = userId })).ToList();

                foreach (var notification in notifications)
                {
                    if (notification.DisplayType.Equals("ALL_USERS"))
                    {
                        notification.IsPublic = true;
                        notification.UserId = null;
                    }
                    if (notification.DisplayType.Equals("USER")) notification.IsPublic = false;
                }
                return notifications;
            }
        }

        Task<int> INotificationRepository.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<Notification>> INotificationRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Notification> INotificationRepository.GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        
        Task<int> INotificationRepository.SaveAsync(Notification notification)
        {
            throw new NotImplementedException();
        }

        Task<int?> INotificationRepository.UpdateAsync(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
