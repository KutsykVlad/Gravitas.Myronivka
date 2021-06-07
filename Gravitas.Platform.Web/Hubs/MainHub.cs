using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using NLog;

namespace Gravitas.Platform.Web.SignalR
{
    [HubName("MainHub")]
    public class MainHub : Hub
    {
        public async Task<dynamic> Reload(string groupId) => string.IsNullOrWhiteSpace(groupId)
            ? await Clients.All.reload()
            : await Clients.Group(groupId).reload(groupId);

        public async Task<dynamic> StopSpinner(string groupId) => string.IsNullOrWhiteSpace(groupId)
            ? await Clients.All.stopSpinner()
            : await Clients.Group(groupId).stopSpinner();

        public async Task<dynamic> StartSpinner(string groupId) => string.IsNullOrWhiteSpace(groupId)
            ? await Clients.All.startSpinner()
            : await Clients.Group(groupId).startSpinner();

        public async Task<dynamic> UpdateDriverCheckIn(string groupId) => await Clients.All.updateDriverCheckIn();
        
        public async Task<dynamic> UpdateProcessingMessage(string groupId) => string.IsNullOrWhiteSpace(groupId)
            ? await Clients.All.updateProcessingMessage()
            : await Clients.Group(groupId).updateProcessingMessage(groupId);

        public Task AddClient(string groupId) => groupId == null ? null : Groups.Add(Context.ConnectionId, groupId);
    }
}