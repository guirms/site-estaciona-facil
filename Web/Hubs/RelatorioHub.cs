using Domain.Helper;
using Web.Hubs.Default;
using Web.Hubs.Interfaces;

namespace Web.Hubs
{
    public class RelatorioHub : DefaultHub<IRelatorioHub>, IRelatorioHub
    {
        private readonly string relatorioGroup;
        public RelatorioHub(IConfiguration configuration) : base(configuration["SignalR:RelatorioGroup"].GetSafeValue())
        {
            relatorioGroup = configuration["SignalR:RelatorioGroup"].GetSafeValue();
        }

        public async Task OnTesteAsync(string requestTeste)
        {
            await Clients.OthersInGroup(relatorioGroup).OnTesteAsync(requestTeste);
        }
    }
}
