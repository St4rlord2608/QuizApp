using Microsoft.Data.SqlClient;
using QuizApp.Models;

namespace QuizApp.Repositories
{
    public class BuzzerRepository
    {
        public BuzzerRepository(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            WebHostEnvironment = webHostEnvironment;
            _configuration = config;
            DBConnectionString = _configuration.GetConnectionString("SQL_CONNECTIONSTRING")!;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        private readonly IConfiguration _configuration;

        private string DBConnectionString;

        public LobbyBuzzData GetLobbyBuzzData(string lobbyCode)
        {
            LobbyBuzzData lobbyBuzzData = new LobbyBuzzData();
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    var command = new SqlCommand("Select * FROM LobbyBuzzData WHERE LobbyCode = @LobbyCode", conn);
                    command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                    using SqlDataReader reader = command.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            lobbyBuzzData.LobbyCode = reader.GetValue(reader.GetOrdinal("LobbyCode")).ToString();
                            lobbyBuzzData.IsBuzzed = (bool)reader.GetValue(reader.GetOrdinal("IsBuzzed"));
                            lobbyBuzzData.BuzzedUserID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("BuzzedUserID")));
                        }
                        conn.Close();
                    }

                }

            }
            catch (Exception ex)
            {
            }
            return lobbyBuzzData;
        }

        public void DeleteBuzzerLobby(string lobbyCode, int userID)
        {
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    var command = new SqlCommand("DELETE FROM BuzzerLobby WHERE LobbyCode = @LobbyCode AND UserID = @UserID", conn);
                    command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.ExecuteNonQuery();
                    conn.Close();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public IEnumerable<BuzzerLobby> GetAllBuzzerLobbiesForLobby(string lobbyCode)
        {
            List<BuzzerLobby> buzzerLobbies = new List<BuzzerLobby>();
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    var command = new SqlCommand("Select * FROM BuzzerLobby WHERE LobbyCode = @LobbyCode", conn);
                    command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                    using SqlDataReader reader = command.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            BuzzerLobby buzzerLobby = new BuzzerLobby();
                            buzzerLobby.LobbyCode = reader.GetValue(reader.GetOrdinal("LobbyCode")).ToString();
                            buzzerLobby.UserID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("UserID")));
                            buzzerLobby.Points = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Points")));
                            buzzerLobby.TextLocked = (bool)(reader.GetValue(reader.GetOrdinal("TextLocked")));
                            buzzerLobbies.Add(buzzerLobby);

                        }
                        conn.Close();
                        return buzzerLobbies;
                    }

                }
            }
            catch (Exception ex)
            {
                return buzzerLobbies;
            }
        }

        public void AddLobbyBuzzData(string lobbyCode)
        {
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    var command = new SqlCommand("INSERT INTO LobbyBuzzData(LobbyCode) VALUES(@LobbyCode)", conn);
                    command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                    command.ExecuteNonQuery();
                    conn.Close();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void ChangeBuzzedStateForLobby(string lobbyCode, bool isBuzzed, int userID)
        {
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    var command = new SqlCommand("UPDATE LobbyBuzzData SET IsBuzzed = @isBuzzed, BuzzedUserID = @BuzzedUserID WHERE LobbyCode = @LobbyCode", conn);
                    command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                    command.Parameters.AddWithValue("@IsBuzzed", isBuzzed);
                    command.Parameters.AddWithValue("@BuzzedUserID", userID);
                    command.ExecuteNonQuery();
                    conn.Close();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void ChangeTextLockedStateForBuzzerLobby(string lobbyCode, bool textLocked, int userID)
        {
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    var command = new SqlCommand("UPDATE BuzzerLobby SET TextLocked = @TextLocked WHERE LobbyCode = @LobbyCode AND UserID = @TextLockedUserID", conn);
                    command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                    command.Parameters.AddWithValue("@TextLocked", textLocked);
                    command.Parameters.AddWithValue("@TextLockedUserID", userID);
                    command.ExecuteNonQuery();
                    conn.Close();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void AddBuzzerLobby(string lobbyCode, int userID, int points)
        {
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    var command = new SqlCommand("INSERT INTO BuzzerLobby(LobbyCode, UserID, Points) VALUES(@LobbyCode, @UserID, @Points)", conn);
                    command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Points", points);
                    command.ExecuteNonQuery();
                    conn.Close();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public BuzzerLobby GetBuzzerLobby(string lobbyCode, int userID)
        {
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    var command = new SqlCommand("SELECT * FROM BuzzerLobby WHERE LobbyCode = @LobbyCode AND UserID = @UserID", conn);
                    command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                    command.Parameters.AddWithValue("@UserID", userID);
                    using SqlDataReader reader = command.ExecuteReader();
                    {
                        BuzzerLobby buzzerLobby = new BuzzerLobby();
                        while (reader.Read())
                        {
                            buzzerLobby.LobbyCode = reader.GetValue(reader.GetOrdinal("LobbyCode")).ToString();
                            buzzerLobby.UserID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("UserID")));
                            buzzerLobby.Points = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Points")));
                            buzzerLobby.TextLocked = (bool)(reader.GetValue(reader.GetOrdinal("TextLocked")));
                        }
                        conn.Close();
                        return buzzerLobby;
                    }
                }
            }
            catch (Exception ex) {
                return new BuzzerLobby();
            }
        }

        public void UpdateBuzzerLobbyPoints(string lobbyCode, int userID, int points)
        {
            try
            {
                using var conn = new SqlConnection(DBConnectionString);
                {
                    conn.Open();
                    var command = new SqlCommand("UPDATE BuzzerLobby SET Points = @Points WHERE LobbyCode = @LobbyCode AND UserID = @UserID", conn);
                    command.Parameters.AddWithValue("@Points", points);
                    command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
