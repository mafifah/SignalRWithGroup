using Microsoft.AspNetCore.SignalR;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SignalRWithGroup.Hubs
{
    public class ChatHub : Hub
    {
        public async Task MulaiKoneksi(string divisi)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, divisi); //memasukkan id koneksi ke grup
        }

        public async Task KirimPesan(PesanSignalR psr)
        {
          
           //await Clients.Client(Context.ConnectionId).SendAsync("KirimPesan",psr); //kirim pesan ke diri sendiri
            
           await Clients.OthersInGroup(psr.Divisi).SendAsync("KirimPesan", psr); //kirim pesan ke divisi yang sama
        }

        public async Task KirimPesanBroadcast(PesanSignalR psr)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("KirimPesanBroadcast",psr); //kirim pesan ke diri sendiri
            await Clients.All.SendAsync("KirimPesanBroadcast", psr); //kirim pesan broadcast ke semua orang
        }

        public async Task StopKoneksi(string divisi)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, divisi); //menhapus id koneksi dari grup
        }

        public class PesanSignalR
        {
            public string Klien { get; set; }
            public string Divisi { get; set; }
            public object IdUser { get; set; }
            public long IdForm { get; set; }
            public string NamaForm { get; set; }
            public string StatusAction { get; set; }
            public object PrimaryKey { get; set; }
            public DateTimeOffset WaktuProses { get; set; }
            public string IsiPesan { get; set; }
            public string JenisPesan { get; set; }
        }
    }
}