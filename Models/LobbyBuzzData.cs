using System.Text.Json;

namespace QuizApp.Models;

public class LobbyBuzzData
{
    public string? LobbyCode { get; set; }

    public bool IsBuzzed { get; set; }
    public int BuzzedUserID { get; set; }

    public override string ToString() => JsonSerializer.Serialize<LobbyBuzzData>(this);
}