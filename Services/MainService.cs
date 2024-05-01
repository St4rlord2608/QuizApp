using Microsoft.AspNetCore.Hosting;
using QuizApp.Repositories;

namespace QuizApp.Services
{
    public class MainService
    {

        public MainService(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            WebHostEnvironment = webHostEnvironment;
            _configuration = config;
            _mainRepository = new MainRepository(webHostEnvironment, config);
        }
        public IWebHostEnvironment WebHostEnvironment { get; }
        private readonly IConfiguration _configuration;
        private MainRepository _mainRepository;
        public bool DBIsConnected()
        {
            return _mainRepository.DBIsConnected();
        }
    }
}
