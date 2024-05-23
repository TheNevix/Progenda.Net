using Newtonsoft.Json;
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

        public ProgendaApiClient(string email, string token)
        {
            _email = email;
            _token = token;
            _baseUrl = "https://progenda.be/api/v2/";
            _base64Token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_email}:{_token}"));
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _base64Token);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CenterResponse> GetCenters()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "centers");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                CenterResponse centerResponse = JsonConvert.DeserializeObject<CenterResponse>(responseBody);
                return centerResponse;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }
    }
}
