﻿using Newtonsoft.Json;
using Progenda.Net.Api.Dtos;
using System.Net.Http.Headers;
using System.Text;

namespace Progenda.Net.Api
{
    public class ProgendaApiClient
    {
        private readonly string _email;
        private readonly string _token;
        private readonly string _baseUrl;
        private readonly string _base64Token;
        private readonly HttpClient _httpClient;

        public ProgendaApiClient(string email, string token, string applicationName)
        {
            _email = email;
            _token = token;
            _baseUrl = "https://progenda.be/api/v2/";
            _base64Token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_email}:{_token}"));
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _base64Token);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(applicationName);
        }

        public async Task<List<CenterDetails>> GetCenters()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl);
                HttpResponseMessage response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                CenterResponse centerResponse = JsonConvert.DeserializeObject<CenterResponse>(responseBody);

                List<CenterDetails> centerDetailsList = new List<CenterDetails>();

                if (centerResponse != null && centerResponse.Centers != null)
                {
                    foreach (var center in centerResponse.Centers)
                    {
                        centerDetailsList.Add(center.CenterDetails);
                    }
                }

                return centerDetailsList;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }
    }
}
