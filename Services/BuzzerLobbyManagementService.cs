using Microsoft.Data.SqlClient;
using QuizApp.Models;
using System.Text.Json;
using QuizApp.Repositories;

namespace QuizApp.Services
{
    public class BuzzerLobbyManagementService
    {
        public BuzzerLobbyManagementService(IWebHostEnvironment webHostEnvironment, IConfiguration config) 
        {
            WebHostEnvironment = webHostEnvironment;
            _configuration = config;
            _buzzerRepository = new BuzzerRepository(webHostEnvironment, config);
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        private readonly IConfiguration _configuration;
        private BuzzerRepository _buzzerRepository;
        private Random random = new Random();

        public IEnumerable<Lobby> GetLobbies()
        {
            return _buzzerRepository.GetLobbies();  
        }

        public Lobby GetLobby(string lobbyCode)
        {
            return _buzzerRepository.GetLobby(lobbyCode);
        }

        public string CreateLobbyCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void AddLobby(string lobbyCode)
        {
            _buzzerRepository.AddLobby(lobbyCode, DateTime.Now);
        }

        public void DeleteLobby(string lobbyCode)
        {
            _buzzerRepository.DeleteLobby(lobbyCode);
        }

        public void RefreshLobbyTime(string lobbyCode)
        {
            _buzzerRepository.UpdateLobbyCreationDateTime(lobbyCode, DateTime.Now);
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
        public void AddBuzzerLobby(string lobbyCode, int userID, int points)
        {
            _buzzerRepository.AddBuzzerLobby(lobbyCode, userID, points);
        }

        public BuzzerLobby GetBuzzerLobby(string lobbyCode, int userID)
        {
           return _buzzerRepository.GetBuzzerLobby(lobbyCode, userID);
        }

        public void ChangeBuzzedStateForLobby(string lobbyCode, bool isBuzzed, int userID)
        {
            _buzzerRepository.ChangeBuzzedStateForLobby(lobbyCode, isBuzzed, userID);
        }

        public IEnumerable<BuzzerLobby> GetAllBuzzerLobbiesForLobby(string lobbyCode)
        {
            return _buzzerRepository.GetAllBuzzerLobbiesForLobby(lobbyCode);
        }

        public void UpdateBuzzerLobbyPoints(string lobbyCode, int userID, int points)
        {
            _buzzerRepository.UpdateBuzzerLobbyPoints(lobbyCode, userID, points);
        }
    }
}
