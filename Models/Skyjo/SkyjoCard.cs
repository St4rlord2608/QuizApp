namespace QuizApp.Models;
using System.Text.Json;

public class SkyjoCard
{
    public string? LobbyCode { get; set; }
    public int PlayerId { get; set; }
    public int Value { get; set; }
    public int Position { get; set; }
    public bool IsTurned { get; set; }
    public override string ToString() => JsonSerializer.Serialize<SkyjoCard>(this);
}