namespace QuizApp.Models;
using System.Text.Json;

public class LobbySkyjoData
{
    public string? LobbyCode { get; set; }
    
    public int ActivePlayerId { get; set; }
    public int GameState { get; set; }
    
    public int UsedCards { get; set; }
    
    public int LastUsedCard { get; set; }
    
    public string CardMixSeed { get; set; }

    public override string ToString() => JsonSerializer.Serialize<LobbySkyjoData>(this);
}