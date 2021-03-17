using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using CryptoWatchAPI.Models;
using System.Collections.Generic;

namespace CryptoWatchAPI.Hubs
{
    public class Client
    {

        private static readonly HttpClient _client = new HttpClient();
        private readonly Timer _timer = new Timer();
        private readonly IHubContext<CryptoHub> _hubContext;


        public Client(IHubContext<CryptoHub> hubContext)
        {
            _hubContext = hubContext;
            initTimer();
        }

        private void initTimer()
        {
            _timer.Interval = 1000;
            _timer.Elapsed += async (sender, e) =>
           {

               var res = await Get();

               if (res != null)
                   await _hubContext.Clients.All.SendAsync("UpdatePrices", res);

           };
            _timer.Start();
        }

        private async Task<List<Stock>> Get()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("https://api.miraiex.com/v2/markets");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var stock = JsonSerializer.Deserialize<List<Stock>>(responseBody, options);

                return stock;
            }
            catch (HttpRequestException e)
            {

            }
            return null;
        }

    }

}