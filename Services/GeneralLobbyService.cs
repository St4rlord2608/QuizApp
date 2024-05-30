using QuizApp.Models;
using QuizApp.Repositories;

namespace QuizApp.Services;

public class GeneralLobbyService
{
    public GeneralLobbyService(IWebHostEnvironment webHostEnvironment, IConfiguration config) 
    {
        WebHostEnvironment = webHostEnvironment;
        _configuration = config;
        _generalLobbyRepository = new GeneralLobbyRepository(webHostEnvironment, config);
    }

    public enum LobbyTypeEnum
    {
        Buzzer,
        Skyjo
    }

    public IWebHostEnvironment WebHostEnvironment { get; }
    private readonly IConfiguration _configuration;
    private GeneralLobbyRepository _generalLobbyRepository;
    private Random random = new Random();
    
    public IEnumerable<Lobby> GetLobbies()
    {
        return _generalLobbyRepository.GetLobbies();  
    }
    
    public string CreateLobbyCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public Lobby GetLobby(string lobbyCode)
    {
        return _generalLobbyRepository.GetLobby(lobbyCode);
    }
    
    public void AddLobby(string lobbyCode, int hostUserID, int lobbyType)
    {
        _generalLobbyRepository.AddLobby(lobbyCode, DateTime.Now, hostUserID, lobbyType);
    }
    
    public void RefreshLobbyTime(string lobbyCode)
    {
        _generalLobbyRepository.UpdateLobbyCreationDateTime(lobbyCode, DateTime.Now);
    }
    
    public void DeleteLobby(string lobbyCode)
    {
        _generalLobbyRepository.DeleteLobby(lobbyCode);
    }
    
    public void CheckForExpiredLobbies()
    {
        var lobbies = GetLobbies().ToList();
        foreach( Lobby lobby in lobbies)
        {
            if(DateTime.Now > lobby.CreationDateTime.AddMinutes(30))
            {
                DeleteLobby(lobby.LobbyCode);
            }
        }
    }
}