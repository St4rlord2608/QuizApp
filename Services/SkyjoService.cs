using QuizApp.Models;
using QuizApp.Repositories;

namespace QuizApp.Services;

public class SkyjoService
{
    public SkyjoService(IWebHostEnvironment webHostEnvironment, IConfiguration config)
    {
        WebHostEnvironment = webHostEnvironment;
        _configuration = config;
        SkyjoRepository = new SkyjoRepository(webHostEnvironment, config);
    }

    public IWebHostEnvironment WebHostEnvironment { get; }
    private SkyjoRepository SkyjoRepository;
    private readonly IConfiguration _configuration;
    private Random random = new Random();
    public List<SkyjoCard> CardStack = new List<SkyjoCard>();

    public List<SkyjoCard> InitCardList()
    {
        for (int i = 1; i <= 10; i++)
        {
            for (int j = -1; j <= 12; j++)
            {
                if (j == 0)
                {
                    continue;
                }

                CardStack.Add(new SkyjoCard() { Value = j });
            }
        }

        for (int i = 1; i <= 15; i++)
        {
            CardStack.Add(new SkyjoCard() { Value = 0 });
        }

        for (int i = 1; i <= 5; i++)
        {
            CardStack.Add(new SkyjoCard() { Value = -2 });
        }

        return CardStack;
    }

    public List<SkyjoCard> CreateCardStack(LobbySkyjoData lobbySkyjoData)
    {
        bool cardMixSeedGiven = true;
        string[] keys = [];
        Random rng = new Random();
        int n = CardStack.Count;
        if (lobbySkyjoData.CardMixSeed == "")
        {
            cardMixSeedGiven = false;
        }
        else
        {
            keys = lobbySkyjoData.CardMixSeed.Split(",");
        }

        while (n > 1)
        {
            n--;
            int k = 0;
            if (!cardMixSeedGiven)
            {
                k = rng.Next(n + 1);
                lobbySkyjoData.CardMixSeed += k + ",";
            }
            else
            {
                k = Convert.ToInt32(keys[keys.Length - n -1]);
            }

            (CardStack[k], CardStack[n]) = (CardStack[n], CardStack[k]);
        }

        if (!cardMixSeedGiven) SkyjoRepository.UpdateLobbySkyjoData(lobbySkyjoData);
        return CardStack;
    }

    public void AddSkyjoLobby(string lobbyCode, int userID, int points)
    {
        SkyjoRepository.AddSkyjoLobby(lobbyCode, userID, points);
    }

    public IEnumerable<SkyjoLobby> GetAllSkyjoLobbiesForLobby(string lobbyId)
    {
        return SkyjoRepository.GetAllSkyjoLobbiesForLobby(lobbyId);
    }

    public SkyjoLobby GetSkyjoLobby(string lobbyId, int userID)
    {
        return SkyjoRepository.GetSkyjoLobby(lobbyId, userID);
    }

    public LobbySkyjoData GetLobbySkyjoData(string lobbyId)
    {
        return SkyjoRepository.GetLobbySkyjoData(lobbyId);
    }

    public void AddLobbySkyjoData(string lobbyCode)
    {
        SkyjoRepository.AddLobbySkyjoData(lobbyCode);
    }

    public void UpdateLobbySkyjoData(LobbySkyjoData lobbySkyjoData)
    {
        SkyjoRepository.UpdateLobbySkyjoData(lobbySkyjoData);
    }

    public void AddSkyjoCard(SkyjoCard skyjoCard)
    {
        SkyjoRepository.AddSkyjoCard(skyjoCard);
    }

    public void UpdateSkyjoCard(SkyjoCard skyjoCard)
    {
        SkyjoRepository.UpdateSkyjoCard(skyjoCard);
    }

    public void DeleteAllSkyjoCardsForPlayer(string lobbyCode, int userId)
    {
        SkyjoRepository.DeleteAllSkyjoCardsForPlayer(lobbyCode, userId);
    }

    public SkyjoCard GetSkyjoCard(string lobbyCode, int userId, int position)
    {
        return SkyjoRepository.GetSkyjoCard(lobbyCode, userId, position);
    }

    public List<SkyjoCard> GetAllSkyjoCardsForPlayer(string lobbyCode, int userId)
    {
        return SkyjoRepository.GetAllSkyjoCardsForPlayer(lobbyCode, userId);
    }
}