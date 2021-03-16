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

        public Client()
        {
            initTimer();
        }

        private void initTimer()
        {
            _timer.Interval = 1000;
            _timer.Elapsed += (sender, e) =>
            {
                
            };
            _timer.Start();
        }

    }

}