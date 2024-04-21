using Microsoft.AspNetCore.SignalR;

namespace QuizApp.Hubs
{
    public class BuzzHub : Hub
    {
        public async Task ChangeBuzz(string groupName, int userID, string username, bool buzzState)
        {
            await Clients.Group(groupName).SendAsync("ReceiveBuzzChange", userID, username, buzzState);
        }
        public async Task TextInputChange(string groupName, int userID, string username, string text)
        {
            await Clients.Group(groupName).SendAsync("ReceiveTextChange", userID, username, text);
        }

        public async Task TextLock(string groupName, int userID)
        {
            await Clients.Group(groupName).SendAsync("ReceiveTextLock", userID);
        }

        public async Task ResetTextLock(string groupName)
        {
            await Clients.Group(groupName).SendAsync("ReceiveResetTextLock");
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task ChangePoints(string groupName, int userID, int points)
        {
            await Clients.Group(groupName).SendAsync("ReceiveChangePoints", userID, points);
        }

        public async Task JoinedLobby(string groupName, int userID, string username)
        {
            await Clients.Group(groupName).SendAsync("ReceiveJoinLobby", userID, username);
        }
        
    }
}
