using MB_API.Data.Entities;
using Microsoft.AspNetCore.SignalR;

namespace MB_API.Hubs
{
    public class ResultsHub : Hub
    {
        // This method will be called from your controller to send the new result
        public async Task SendNewResult(ResultEntity result)
        {
            await Clients.All.SendAsync("ReceiveNewResult", result);
        }

        public async Task SendNewCheckPoints(List<RaceCheckPointEntity> raceCheckPoints)
        {
            await Clients.All.SendAsync("ReceiveNewCheckPoint", raceCheckPoints);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} is joined!");
        }   
    }
}
