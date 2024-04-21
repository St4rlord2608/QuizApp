using Microsoft.AspNetCore.Hosting;
using QuizApp.Models;
using QuizApp.Repositories;

namespace QuizApp.Services
{
    public class UserService
    {
        public UserService(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            WebHostEnvironment = webHostEnvironment;
            _configuration = config;
            _buzzerRepository = new BuzzerRepository(webHostEnvironment, config);
            _userRepository = new UserRepository(webHostEnvironment, config);
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        private readonly IConfiguration _configuration;
        private BuzzerRepository _buzzerRepository;
        private UserRepository _userRepository;

        public User GetUser(int userID)
        {
            return _userRepository.GetUser(userID);
        }

        public User CreateUser(string username)
        {
            return _userRepository.CreateUser(username);
        }

        public void UpdateUserName(int userID, string username)
        {
            _userRepository.UpdateUserName(userID, username);
        }
    }
}
