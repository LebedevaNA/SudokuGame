using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Sudoku.Application.Hubs
{
    public class GameStateHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}