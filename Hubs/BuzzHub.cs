using Microsoft.AspNetCore.SignalR;

namespace QuizApp.Hubs
{
    public class BuzzHub : Hub
    {
        public async Task ChangeBuzz(string groupName, int userID, string username, bool buzzState)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveBuzzChange", userID, username, buzzState);
        }
        public async Task TextInputChange(string groupName, int userID, string username, string text)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveTextChange", userID, username, text);
        }

        public async Task TextForPlayersChange(string groupName, string text)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveTextForPlayersChange", text);
        }

        public async Task TextLock(string groupName, int userID)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveTextLock", userID);
        }

        public async Task ResetTextLock(string groupName)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveResetTextLock");
        }

        public async Task ResetSingleTextLock(string groupName, int userID)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveResetSingleTextLock", userID);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task ChangePoints(string groupName, int userID, int points, bool wasBuzzed, bool correct)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveChangePoints", userID, points, wasBuzzed, correct);
        }

        public async Task JoinedLobby(string groupName, int userID, string username)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveJoinLobby", userID, username);
        }

        public async Task LeaveLobby(string groupName, int userID)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveLeaveLobby", userID);
        }

        public async Task ReJoinLoby(string groupName, int userID)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveReJoinLobby", userID);
        }

        public async Task KickPlayer(string groupName, int userID)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveKickPlayer", userID);
        }

        public async Task AskForPlayers(string groupName)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveAskForPlayers", Context.ConnectionId);
        }

        public async Task AnswerOnAskForPlayers(string connectionId, int userId)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveAnswerOnAskForPlayers", userId);
        }
    }
}
