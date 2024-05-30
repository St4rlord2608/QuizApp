using QuizApp.Models;
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

        public LobbyBuzzData GetLobbyBuzzData(string lobbyCode)
        {
            return _buzzerRepository.GetLobbyBuzzData(lobbyCode);
        }

        public void AddLobbyBuzzData(string lobbyCode)
        {
            _buzzerRepository.AddLobbyBuzzData(lobbyCode);
        }

        public void DeleteBuzzerLobby(string lobbyCode, int userID)
        { 
            _buzzerRepository.DeleteBuzzerLobby(lobbyCode,  userID);
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

        public void ChangeTextLockedStateForBuzzerLobby(string lobbyCode, bool textLocked, int userID)
        {
            _buzzerRepository.ChangeTextLockedStateForBuzzerLobby(lobbyCode, textLocked, userID);
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
