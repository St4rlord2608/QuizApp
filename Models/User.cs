using System.Text.Json;

namespace QuizApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }

        public override string ToString() => JsonSerializer.Serialize<User>(this);
    }
}
