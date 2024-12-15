using Newtonsoft.Json;
using RestSharp;
using System.Text;

namespace Lab5
{
    public class Auth0TokenService
    {
        private readonly string _domain;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _audience;

        public Auth0TokenService(IConfiguration configuration)
        {
            _domain = configuration["Auth0:Domain"];
            _clientId = configuration["Auth0:ClientId"];
            _clientSecret = configuration["Auth0:ClientSecret"];
            _audience = $"https://{_domain}/api/v2/";
        }

        public async Task<string> GetManagementApiTokenAsync()
        {
            var client = new HttpClient();
            var data = new
            {
                grant_type = "client_credentials",
                client_id = _clientId,
                client_secret = _clientSecret,
                audience = _audience
            };

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://{_domain}/oauth/token", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                dynamic tokenResponse = JsonConvert.DeserializeObject(responseContent);
                return tokenResponse.access_token;
            }
            else
            {
                throw new HttpRequestException($"Failed to obtain token: {responseContent}");
            }
        }
    }
}
