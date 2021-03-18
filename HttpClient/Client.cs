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
        private readonly IHubContext<CryptoHub> _hubContext;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public Client(IHubContext<CryptoHub> hubContext, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _hubContext = hubContext;
            _httpClient = _httpClientFactory.CreateClient("miraiex");

            Task.Run(async () =>
            {
                while (true)
                {
                    await _hubContext.Clients.All.SendAsync("GetServerTime", DateTimeOffset.UtcNow);
                    await Task.Delay(1000);
                }
            });

            Task.Run(async () =>
            {
                while (true)
                {
                    var stocks = await GetAsync<List<Stock>>("/v2/markets");
                    if (stocks != null)
                    {
                        await _hubContext.Clients.All.SendAsync("UpdatePrices", stocks);
                        await Task.Delay(1000);
                    }
                }
            });
        }

        private async Task<T> GetAsync<T>(string path)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(path);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var deserializedVal = responseBody.Deserialize<T>();

                return deserializedVal;
            }
            catch (HttpRequestException e)
            {

            }

            return default(T);
        }
    }
}