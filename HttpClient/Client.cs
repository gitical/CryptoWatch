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

               var res = await Get<List<Stock>>();

               if (res != null)
                   await _hubContext.Clients.All.SendAsync("UpdatePrices", res, DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss"));

           };
            _timer.Start();
        }

        private async Task<T> Get<T>()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("https://api.miraiex.com/v2/markets");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var stock = Deserialize<T>(responseBody);

                return stock;
            }
            catch (HttpRequestException e)
            {
                System.Console.WriteLine(e);
            }

            return default(T);
        }

        private T Deserialize<T>(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(json, options);
        }

    }

}