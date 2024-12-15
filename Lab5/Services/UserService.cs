using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Lab5.Services
{
    public class UserService
    {
        private readonly Auth0TokenService _tokenService;
        private readonly string _domain;

        public UserService(Auth0TokenService tokenService, string domain)
        {
            _tokenService = tokenService;
            _domain = domain;
        }

        public async Task<UserProfile> GetUserProfileAsync(string userId)
        {
            var token = await _tokenService.GetManagementApiTokenAsync();

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://{_domain}/api/v2/users/{userId}");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var userProfile = JsonConvert.DeserializeObject<UserProfile>(responseContent);
                return userProfile;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve user profile: {responseContent}");
            }
        }
    }

}
