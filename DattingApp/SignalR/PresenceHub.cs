using DattingAppApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DattingAppApi.SignalR
{
    [Authorize]
    public class PresenceHub(PresenceTracker tracker) : Hub
    {
        public override async Task OnConnectedAsync()
        {
            if (Context.User == null) throw new HubException("Cannot get current user claim");

            bool isOnline = await tracker.UserConnected(Context.User.GetUserName(),Context.ConnectionId);
            if (isOnline) await Clients.Others.SendAsync("UserIsOnline", Context.User?.GetUserName());

            var currentUsers = await tracker.GetOnlineUsers();
            await Clients.Caller.SendAsync("GetOnlineUSers", currentUsers);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.User == null) throw new HubException("Cannot get current user claim");

            bool isOffline = await tracker.UserDisconnected(Context.User.GetUserName(), Context.ConnectionId);
            if (isOffline) await Clients.Others.SendAsync("UserIsOffline", Context.User?.GetUserName());

            await base.OnDisconnectedAsync(exception);
        }
    }
}
