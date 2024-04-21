using System.Text.Json;

namespace QuizApp.Models
{
    public class Lobby
    {
        public string? LobbyCode { get; set; }
        public DateTime CreationDateTime { get; set; }

        public bool IsBuzzed { get; set; }
        public int BuzzedUserID { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Lobby>(this);
    }
}
