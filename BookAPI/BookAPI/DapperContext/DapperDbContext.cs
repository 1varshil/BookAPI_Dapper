using Microsoft.Data.SqlClient;
using System.Data;

namespace BookAPI.DapperContext
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("Database");
        }

        public IDbConnection CreateConnection() => new SqlConnection(connectionString);
    }
}
