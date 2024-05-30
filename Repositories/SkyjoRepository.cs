using Microsoft.Data.SqlClient;
using QuizApp.Models;

namespace QuizApp.Repositories;

public class SkyjoRepository
{
    public SkyjoRepository(IWebHostEnvironment webHostEnvironment, IConfiguration config)
    {
        WebHostEnvironment = webHostEnvironment;
        _configuration = config;
        DBConnectionString = _configuration.GetConnectionString("SQL_CONNECTIONSTRING")!;
    }

    public IWebHostEnvironment WebHostEnvironment { get; }
    private readonly IConfiguration _configuration;

    private string DBConnectionString;
    
    public LobbySkyjoData GetLobbySkyjoData(string lobbyCode)
    {
        LobbySkyjoData lobbySkyjoData = new LobbySkyjoData();
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("Select * FROM LobbySkyjoData WHERE LobbyCode = @LobbyCode", conn);
                command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                using SqlDataReader reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        lobbySkyjoData.LobbyCode = reader.GetValue(reader.GetOrdinal("LobbyCode")).ToString();
                        lobbySkyjoData.ActivePlayerId =
                            Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ActivePlayerId")));
                        lobbySkyjoData.GameState = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("GameState")));
                        lobbySkyjoData.UsedCards = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("UsedCards")));
                        lobbySkyjoData.LastUsedCard = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("LastUsedCard")));
                        lobbySkyjoData.CardMixSeed = reader.GetValue(reader.GetOrdinal("CardMixSeed")).ToString();
                    }
                    conn.Close();
                }

            }

        }
        catch (Exception ex)
        {
        }
        return lobbySkyjoData;
    }
    
    public void DeleteSkyjoLobby(string lobbyCode, int userID)
    {
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("DELETE FROM SkyjoLobby WHERE LobbyCode = @LobbyCode AND UserID = @UserID", conn);
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
    
    public IEnumerable<SkyjoLobby> GetAllSkyjoLobbiesForLobby(string lobbyCode)
    {
        List<SkyjoLobby> skyjoLobbies = new List<SkyjoLobby>();
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("Select * FROM SkyjoLobby WHERE LobbyCode = @LobbyCode", conn);
                command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                using SqlDataReader reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        SkyjoLobby skyjoLobby = new SkyjoLobby();
                        skyjoLobby.LobbyCode = reader.GetValue(reader.GetOrdinal("LobbyCode")).ToString();
                        skyjoLobby.UserId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("UserId")));
                        skyjoLobby.Points = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Points")));
                        skyjoLobbies.Add(skyjoLobby);

                    }
                    conn.Close();
                    return skyjoLobbies;
                }

            }
        }
        catch (Exception ex)
        {
            return skyjoLobbies;
        }
    }
    
    public void AddLobbySkyjoData(string lobbyCode)
    {
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("INSERT INTO LobbySkyjoData(LobbyCode) VALUES(@LobbyCode)", conn);
                command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                command.ExecuteNonQuery();
                conn.Close();
            }

        }
        catch (Exception ex)
        {

        }
    }
    
    public void AddSkyjoLobby(string lobbyCode, int userID, int points)
    {
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("INSERT INTO SkyjoLobby(LobbyCode, UserID, Points) VALUES(@LobbyCode, @UserID, @Points)", conn);
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
    
    public SkyjoLobby GetSkyjoLobby(string lobbyCode, int userID)
    {
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("SELECT * FROM SkyjoLobby WHERE LobbyCode = @LobbyCode AND UserID = @UserID", conn);
                command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                command.Parameters.AddWithValue("@UserID", userID);
                using SqlDataReader reader = command.ExecuteReader();
                {
                    SkyjoLobby skyjoLobby = new SkyjoLobby();
                    while (reader.Read())
                    {
                        skyjoLobby.LobbyCode = reader.GetValue(reader.GetOrdinal("LobbyCode")).ToString();
                        skyjoLobby.UserId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("UserId")));
                        skyjoLobby.Points = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Points")));
                    }
                    conn.Close();
                    return skyjoLobby;
                }
            }
        }
        catch (Exception ex) {
            return new SkyjoLobby();
        }
    }
    
    public void UpdateSkyjoLobbyPoints(string lobbyCode, int userID, int points)
    {
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("UPDATE SkyjoLobby SET Points = @Points WHERE LobbyCode = @LobbyCode AND UserID = @UserID", conn);
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
    
    public void UpdateLobbySkyjoData(LobbySkyjoData lobbySkyjoData)
    {
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("UPDATE LobbySkyjoData SET GameState = @GameState, UsedCards = @UsedCards, LastUsedCard = @LastUsedCard, CardMixSeed = @CardMixSeed " +
                                             "WHERE LobbyCode = @LobbyCode", conn);
                command.Parameters.AddWithValue("@GameState", lobbySkyjoData.GameState);
                command.Parameters.AddWithValue("@UsedCards", lobbySkyjoData.UsedCards);
                command.Parameters.AddWithValue("@LastUsedCard", lobbySkyjoData.LastUsedCard);
                command.Parameters.AddWithValue("@CardMixSeed", lobbySkyjoData.CardMixSeed);
                command.Parameters.AddWithValue("@LobbyCode", lobbySkyjoData.LobbyCode);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void AddSkyjoCard(SkyjoCard skyjoCard)
    {
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("INSERT INTO SkyjoCard(LobbyCode, PlayerId, Value, Position, IsTurned) " +
                                             "VALUES(@LobbyCode, @UserID, @Value, @Position, @IsTurned)", conn);
                command.Parameters.AddWithValue("@LobbyCode", skyjoCard.LobbyCode);
                command.Parameters.AddWithValue("@UserID", skyjoCard.PlayerId);
                command.Parameters.AddWithValue("@Value", skyjoCard.Value);
                command.Parameters.AddWithValue("@Position", skyjoCard.Position);
                command.Parameters.AddWithValue("@IsTurned", skyjoCard.IsTurned);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void UpdateSkyjoCard(SkyjoCard skyjoCard)
    {
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("UPDATE SkyjoCard SET Value = @Value, IsTurned = @IsTurned WHERE LobbyCode = @LobbyCode AND PlayerId = @PlayerId AND Position = @Position", conn);
                command.Parameters.AddWithValue("@LobbyCode", skyjoCard.LobbyCode);
                command.Parameters.AddWithValue("@UserID", skyjoCard.PlayerId);
                command.Parameters.AddWithValue("@Value", skyjoCard.Value);
                command.Parameters.AddWithValue("@Position", skyjoCard.Position);
                command.Parameters.AddWithValue("@IsTurned", skyjoCard.IsTurned);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void DeleteAllSkyjoCardsForPlayer(string lobbyCode, int userId)
    {
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("DELETE FROM SkyjoCard WHERE LobbyCode = @LobbyCode AND PlayerId = @UserID", conn);
                command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                command.Parameters.AddWithValue("@UserID", userId);
                command.ExecuteNonQuery();
                conn.Close();
            }

        }
        catch (Exception ex)
        {

        }
    }

    public SkyjoCard GetSkyjoCard(string lobbyCode, int userId, int position)
    {
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("SELECT * FROM SkyjoCard WHERE LobbyCode = @LobbyCode AND UserID = @UserID AND Position = @Position", conn);
                command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@Position", position);
                using SqlDataReader reader = command.ExecuteReader();
                {
                    SkyjoCard skyjoCard = new SkyjoCard();
                    while (reader.Read())
                    {
                        skyjoCard.LobbyCode = reader.GetValue(reader.GetOrdinal("LobbyCode")).ToString();
                        skyjoCard.PlayerId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PlayerId")));
                        skyjoCard.Value = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Value")));
                        skyjoCard.Position = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Position")));
                        skyjoCard.IsTurned = (bool)reader.GetValue(reader.GetOrdinal("IsTurned"));
                    }
                    conn.Close();
                    return skyjoCard;
                }
            }
        }
        catch (Exception ex) {
            return new SkyjoCard();
        }
    }

    public List<SkyjoCard> GetAllSkyjoCardsForPlayer(string lobbyCode, int userId)
    {
        List<SkyjoCard> skyjoCards = new List<SkyjoCard>();
        try
        {
            using var conn = new SqlConnection(DBConnectionString);
            {
                conn.Open();
                var command = new SqlCommand("SELECT * FROM SkyjoCard WHERE LobbyCode = @LobbyCode AND UserID = @UserID", conn);
                command.Parameters.AddWithValue("@LobbyCode", lobbyCode);
                command.Parameters.AddWithValue("@UserID", userId);
                using SqlDataReader reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        SkyjoCard skyjoCard = new SkyjoCard();
                        skyjoCard.LobbyCode = reader.GetValue(reader.GetOrdinal("LobbyCode")).ToString();
                        skyjoCard.PlayerId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PlayerId")));
                        skyjoCard.Value = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Value")));
                        skyjoCard.Position = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Position")));
                        skyjoCard.IsTurned = (bool)reader.GetValue(reader.GetOrdinal("IsTurned"));
                        skyjoCards.Add(skyjoCard);
                    }
                    conn.Close();
                    return skyjoCards;
                }
            }
        }
        catch (Exception ex) {
            return skyjoCards;
        }
    }
}