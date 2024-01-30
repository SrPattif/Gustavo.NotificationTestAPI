using Dapper;
using Gustavo.NotificationTestAPI.Model;

namespace Gustavo.NotificationTestAPI.Repositories
{
    public class NotificationRepository : INotificationRepository
    {

        public readonly DbSession _dbSession;
        public NotificationRepository(DbSession _dbSession)
        {
            this._dbSession = _dbSession;
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            using (var conn = _dbSession.connection)
            {
                var query = @"SELECT * FROM Notifications;";
                List<Notification> notifications = (await conn.QueryAsync<Notification>(query)).ToList();

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

        public async Task<Notification> GetAsync(int id)
        {
            using (var conn = _dbSession.connection)
            {
                var query = "SELECT * FROM Notifications WHERE id=@Id;";
                var notification = await conn.QueryFirstOrDefaultAsync<Notification>(query, new { Id = id });

                return notification;
            }
        }


        public async Task<bool> SaveAsync(Notification notification)
        {
            using (var conn = _dbSession.connection)
            {
                var parmts = new
                {
                    Title = notification.Title,
                    Description = notification.Description,
                    InteractionUrl = notification.InteractionURL,
                    ImageUrl = notification.ImageURL,
                    Type = notification.Type,
                    DisplayType = notification.DisplayType
                };


                var query = "INSERT INTO Notifications (Title, Description, InteractionURL, ImageURL, Type, DisplayType) VALUES (@Title, @Description, @InteractionUrl, @ImageUrl, @Type, @DisplayType);";
                bool inserted = (await conn.ExecuteAsync(query, parmts)) == 1;
            
                return inserted;
            }
        }

        Task<int?> INotificationRepository.UpdateAsync(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
