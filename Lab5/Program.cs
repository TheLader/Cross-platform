using Auth0.AspNetCore.Authentication;
using Auth0.ManagementApi;
using Lab5.Services;
using Lab5.Support;

namespace Lab5
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string domain = builder.Configuration["Auth0:Domain"];
            var auth0TokenService = new Auth0TokenService(builder.Configuration);
            string managementApiToken = await auth0TokenService.GetManagementApiTokenAsync();

            builder.Services.AddSingleton(new ManagementApiClient(managementApiToken, new Uri($"https://{domain}/api/v2")));
            builder.Services.AddSingleton(new UserService(auth0TokenService, domain));

            builder.Services.AddControllersWithViews();
            builder.Services.AddAuth0WebAppAuthentication(options =>
            {
                options.Domain = builder.Configuration["Auth0:Domain"];
                options.ClientId = builder.Configuration["Auth0:ClientId"];
            });

            // Configure the HTTP request pipeline.
            builder.Services.ConfigureSameSiteNoneCookies();
            builder.Services.AddHttpClient();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}
