using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using QuizApp.Models;

namespace QuizApp.Repositories
{
    public class UserRepository
    {
        public UserRepository(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            WebHostEnvironment = webHostEnvironment;
            _configuration = config;
            //DBConnectionString = _configuration.GetConnectionString("Local_SQL_CONNECTIONSTRING")!;
            DBConnectionString = _configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        private readonly IConfiguration _configuration;

        private string DBConnectionString;

        public User GetUser(int userID)
        {
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    var command = new SqlCommand("Select * FROM [User] WHERE UserID = @UserID", conn);
                    command.Parameters.AddWithValue("@UserID", userID);
                    using SqlDataReader reader = command.ExecuteReader();
                    {
                        User user = new User();
                        while (reader.Read())
                        {
                            user.UserID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("UserID")));
                            user.UserName = reader.GetValue(reader.GetOrdinal("Username")).ToString();
                        }
                        conn.Close();
                        return user;
                    }

                }

            }
            catch (Exception ex)
            {
                return new User();
            }
        }

        public User CreateUser(string username)
        {
            int currentMaxUserID = GetMaxUserID();
            int newUserID = currentMaxUserID + 1;
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    var command = new SqlCommand("INSERT INTO [User] (Username) VALUES(@Username)", conn);
                    command.Parameters.AddWithValue("@Username", username + newUserID.ToString());
                    command.ExecuteNonQuery();
                    return new User { UserID = newUserID, UserName = username + newUserID.ToString()};

                }

            }
            catch (Exception ex)
            {
                return new User();
            }
        }

        public void UpdateUserName(int userID, string username)
        {
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    var command = new SqlCommand("UPDATE [User] SET Username = @Username WHERE UserID = @UserID", conn);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                
            }
        }

        public int GetMaxUserID()
        {
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    var command = new SqlCommand("SELECT COALESCE(MAX(UserID), 0) AS UserID FROM [User]", conn);
                    using SqlDataReader reader = command.ExecuteReader();
                    {
                        User user = new User();
                        while (reader.Read())
                        {
                            user.UserID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("UserID")));
                        }
                        conn.Close();
                        return user.UserID;
                    }

                }

            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
