using Auth0.AspNetCore.Authentication;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Lab5.Models;
using Lab5.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lab5.Controllers
{
    public class AccountController : Controller
    {
        private readonly ManagementApiClient _managementApiClient;
        private readonly UserService _userService;

        public AccountController(ManagementApiClient managementApiClient, UserService userService)
        {
            _managementApiClient = managementApiClient;
            _userService = userService;
        }
        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = User.Claims.ElementAt(4)?.Value;
            var userProfile = await _userService.GetUserProfileAsync(userId);

            // Доступ до метаданих користувача
            var phoneNumber = userProfile.user_metadata?["phone_number"];

            return View(new UserProfileViewModel()
            {
                Username = (string)userProfile.user_metadata?["user_name"],
                EmailAddress = User.Identity.Name,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                FullName = (string)userProfile.user_metadata?["full_name"],
                PhoneNumber = (string)userProfile.user_metadata?["phone_number"]
            });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Підготовка даних для створення користувача
                var user = new UserCreateRequest
                {
                    Email = model.Email,
                    Password = model.Password,
                    Connection = "Username-Password-Authentication", // Ім'я вашого connection у Auth0
                    UserMetadata = new Dictionary<string, object>
                    {
                        { "user_name", model.Username},
                        { "full_name", model.FullName},
                        { "phone_number", model.Phone },
                    }
                };

                try
                {
                    var createdUser = await _managementApiClient.Users.CreateAsync(user);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    // Обробка помилок
                    ModelState.AddModelError(string.Empty, $"Помилка реєстрації: {ex.Message}");
                    return View(model);
                }
            }

            return View(model);
        }
    }
}
