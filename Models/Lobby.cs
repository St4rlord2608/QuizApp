using System.Text.Json;

namespace QuizApp.Models
{
    public class Lobby
    {
        public string? LobbyCode { get; set; }
        public DateTime CreationDateTime { get; set; }
        
        public int HostUserID { get; set; }
        public int LobbyType { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Lobby>(this);
    }
}
