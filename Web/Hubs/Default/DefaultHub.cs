using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs.Default
{
    public class DefaultHub<T> : Hub<T> where T : Hub
    {
        private readonly string _groupName;
        protected readonly IHubContext<T> _hubContext;

        public DefaultHub(string groupName, IHubContext<T> hubContext)
        {
            _groupName = groupName;
            _hubContext = hubContext;
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
