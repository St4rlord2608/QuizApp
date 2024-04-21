using System.Text.Json;

namespace QuizApp.Models
{
    public class BuzzerLobby
    {
        public string? LobbyCode { get; set; }
        public int UserID { get; set; }
        public int Points { get; set; }
        public string Text { get; set; } = "";
        public bool TextLocked { get; set; }
        public override string ToString() => JsonSerializer.Serialize<BuzzerLobby>(this);
    }
}
