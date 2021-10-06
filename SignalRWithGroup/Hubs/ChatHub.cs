using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRWithGroup.Hubs
{
    public class ChatHub : Hub
    {
        public async Task MulaiKoneksi(string divisi)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, divisi);
        }

        public async Task KirimPesan(ClientMessage clientMessage)
        {
           await Clients.OthersInGroup(clientMessage.Divisi).SendAsync("KirimPesan", clientMessage);
        }

        public async Task KirimPesanBroadcast(ClientMessage clientMessage)
        {
            await Clients.Others.SendAsync("KirimPesanBroadcast", clientMessage);
        }

        public async Task StopKoneksi(string divisi)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, divisi);
        }

        public class ClientMessage
        {
            public string Divisi { get; set; }
            public string IsiPesan { get; set; }
            public string JenisPesan { get; set; }
            public object Id_PrimaryKey { get; set; }
            public string namaHalaman { get; set; }
        }
    }
}