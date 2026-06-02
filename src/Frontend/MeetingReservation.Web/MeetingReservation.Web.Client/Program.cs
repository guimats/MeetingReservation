using MeetingReservation.Web.Client.Auth;
using MeetingReservation.Web.Client.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace MeetingReservation.Web.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var apiBaseUrl = builder.Configuration.GetSection("ApiConfig:BaseUrl").Value ?? throw new Exception("URL da API não configurada.");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(apiBaseUrl)
            });

            builder.Services.AddScoped<IAuthService, AuthService>();
            //builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}
