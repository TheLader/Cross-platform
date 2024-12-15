using Lab5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Lab5.Controllers
{
    [Authorize]
    public class DirectoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DirectoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Customers()
        {
            var customers = await FetchDataFromApi<CustomerViewModel>("https://localhost:44345/api/Customers");
            return View(customers);
        }

        public async Task<IActionResult> Services()
        {
            var services = await FetchDataFromApi<ServiceViewModel>("https://localhost:44345/api/Services");
            return View(services);
        }

        private async Task<List<T>> FetchDataFromApi<T>(string apiUrl)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<T>>();
                return data ?? new List<T>();
            }

            throw new HttpRequestException($"Помилка при зверненні до API: {response.ReasonPhrase}");
        }
    }
}
