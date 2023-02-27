using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs.Default
{
    public class DefaultHub<T> : Hub<T> where T : class
    {
        private readonly string _groupName;
        public DefaultHub(string groupName)
        {
            _groupName = groupName;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, _groupName);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _groupName);
        }
    }
}
