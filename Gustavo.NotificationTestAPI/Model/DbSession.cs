using Microsoft.Data.SqlClient;
using System.Data;

namespace Gustavo.NotificationTestAPI.Model
{
    public class DbSession : IDisposable
    {

        public IDbConnection connection { get; }

        public DbSession(IConfiguration configuration)
        {
            try
            {
                var connectionString = configuration.GetConnectionString("Sandbox");
                connection = new SqlConnection(connectionString);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public void Dispose() => connection?.Dispose();
    }
}
