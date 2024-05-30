using Microsoft.Data.SqlClient;
using QuizApp.Models;

namespace QuizApp.Repositories;

public class GeneralLobbyRepository
{
    public GeneralLobbyRepository(IWebHostEnvironment webHostEnvironment, IConfiguration config)
    {
        WebHostEnvironment = webHostEnvironment;
        _configuration = config;
        DBConnectionString = _configuration.GetConnectionString("SQL_CONNECTIONSTRING")!;
    }

    public IWebHostEnvironment WebHostEnvironment { get; }
    private readonly IConfiguration _configuration;

    private string DBConnectionString;
    
    public IEnumerable<Lobby> GetLobbies()
    {
        List<Lobby> lobbies = new List<Lobby>();
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("Select * FROM Lobby", conn);
                using SqlDataReader reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        Lobby lobby = new Lobby();
                        lobby.LobbyCode = reader.GetValue(reader.GetOrdinal("LobbyCode")).ToString();
                        lobby.CreationDateTime = (DateTime)reader.GetValue(reader.GetOrdinal("CreationDateTime"));
                        lobby.HostUserID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HostUserID")));
                        lobby.LobbyType = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("LobbyType")));
                        lobbies.Add(lobby);

                    }
                    conn.Close();
                    return lobbies;
                }
                    
            }
                
        }catch (Exception ex)
        {
            return lobbies;
        }
    }
    
    public Lobby GetLobby(string lobbyCode)
    {
        Lobby lobby = new Lobby();
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("Select * FROM Lobby WHERE LobbyCode = @LobbyCode", conn);
                command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                using SqlDataReader reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        lobby.LobbyCode = reader.GetValue(reader.GetOrdinal("LobbyCode")).ToString();
                        lobby.CreationDateTime = (DateTime)reader.GetValue(reader.GetOrdinal("CreationDateTime"));
                        lobby.HostUserID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HostUserID")));
                        lobby.LobbyType = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("LobbyType")));
                    }
                    conn.Close();
                }

            }

        }
        catch (Exception ex)
        {
        }
        return lobby;
    }
    
    public void DeleteLobby(string lobbyCode)
    {
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("DELETE FROM BuzzerLobby WHERE LobbyCode = @LobbyCode", conn);
                command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM LobbyBuzzData WHERE LobbyCode = @LobbyCode";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM SkyjoLobby WHERE LobbyCode = @LobbyCode";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM LobbySkyjoData WHERE LobbyCode = @LobbyCode";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM Lobby WHERE LobbyCode = @LobbyCode";
                command.ExecuteNonQuery();
                conn.Close() ;
            }

        }
        catch (Exception ex)
        {
                
        }
    }
    
    public void AddLobby(string lobbyCode, DateTime creationDateTime, int hostUserID, int lobbyType)
    {
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("INSERT INTO Lobby(LobbyCode, CreationDateTime, HostUserID, LobbyType) VALUES(@LobbyCode, @CreationDateTime, @HostUserID, @LobbyType)", conn);
                command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                command.Parameters.AddWithValue("@CreationDateTime", creationDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                command.Parameters.AddWithValue("@HostUserID", hostUserID);
                command.Parameters.AddWithValue("@LobbyType", lobbyType);
                command.ExecuteNonQuery();
                conn.Close();
            }

        }
        catch (Exception ex)
        {

        }
    }
    
    public void UpdateLobbyCreationDateTime(string lobbyCode, DateTime creationDateTime)
    {
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("UPDATE Lobby SET CreationDateTime = @CreationDateTime WHERE LobbyCode = @LobbyCode", conn);
                command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                command.Parameters.AddWithValue("@CreationDateTime", creationDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                command.ExecuteNonQuery();
                conn.Close();
            }

        }
        catch (Exception ex)
        {

        }
    }
}