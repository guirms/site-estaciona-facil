using Domain.Helper;
using Microsoft.AspNetCore.SignalR;
using Web.Hubs.Default;
using Web.Hubs.Interfaces;

namespace Web.Hubs
{
    public class RelatorioHub : DefaultHub<RelatorioHub>, IRelatorioHub
    {
        private readonly string relatorioGroup;

        public RelatorioHub(IConfiguration configuration, IHubContext<RelatorioHub> hubContext) : base(configuration["SignalR:RelatorioGroup"].GetSafeValue(), hubContext)
        {
            relatorioGroup = configuration["SignalR:RelatorioGroup"].GetSafeValue();
        }

        public async Task OnTesteAsync(string requestTeste)
        {
            await _hubContext.Clients.Groups(relatorioGroup).SendAsync("OnTesteAsync", requestTeste);
        }
    }
}
