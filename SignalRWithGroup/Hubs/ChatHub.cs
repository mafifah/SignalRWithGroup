using Microsoft.AspNetCore.SignalR;
using System.Data;
using System.Threading.Tasks;

namespace SignalRWithGroup.Hubs
{
    public class ChatHub : Hub
    {
        public async Task OnConnect(string divisi)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, divisi);
        }

        public async Task SendMessage(ClientMessage clientMessage)
        {
           await Clients.OthersInGroup(clientMessage.Divisi).SendAsync("SendMessage", clientMessage);
        }

        public async Task SendMessage(string divisi,string namaTabel, DataRow dr)
        {
            await Clients.OthersInGroup(divisi).SendAsync("SendMessage", namaTabel,dr);
        }

        public async Task BroadcastMessage(ClientMessage clientMessage)
        {
            await Clients.Others.SendAsync("BroadcastMessage", clientMessage);
        }

        public async Task OnDisconnect(string divisi)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, divisi);
        }

        public class ClientMessage
        {
            public string Message { get; set; }
            public string Divisi { get; set; }
            public string Method { get; set; }
            public long IdKaryawan { get; set; }
        }
    }
}
