using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;

namespace QuizApp.Repositories
{
    public class MainRepository
    {
        public MainRepository(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            WebHostEnvironment = webHostEnvironment;
            _configuration = config;
            DBConnectionString = _configuration.GetConnectionString("SQL_CONNECTIONSTRING")!;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        private readonly IConfiguration _configuration;
        private string DBConnectionString;

        public bool DBIsConnected()
        {
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    conn.Close();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
