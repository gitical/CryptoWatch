using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace CryptoWatchAPI.Hubs
{
    public class CryptoHub : Hub
    {

        private static readonly System.Timers.Timer _timer = new System.Timers.Timer();


    }

}