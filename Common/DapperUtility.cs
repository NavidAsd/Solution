using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Common
{
    public class DapperUtility
    {
        private readonly IConfiguration Configuration;
        public DapperUtility(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(Configuration.GetConnectionString("LocalConnection"));
        }
    }
}
