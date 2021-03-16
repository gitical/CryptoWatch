using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.SignalR;

namespace CryptoWatchAPI.Hubs
{
    public class Client
    {

        private static readonly HttpClient _client = new HttpClient();
        private static readonly Timer _timer = new Timer();
        private readonly IHubContext<CryptoHub> _hubContext;

        public Client(IHubContext<CryptoHub> hubContext)
        {
            _hubContext = hubContext;
            initTimer();
        }

        private void initTimer()
        {
            _timer.Interval = 1000;
            _timer.Elapsed += (sender, e) =>
            {
                _hubContext.Clients.All.SendAsync("UpdatePrices", "");
            };
            _timer.Start();
        }

    }

}