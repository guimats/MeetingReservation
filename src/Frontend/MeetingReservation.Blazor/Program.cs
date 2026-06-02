using MeetingReservation.Blazor;
using MeetingReservation.Blazor.Auth;
using MeetingReservation.Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["ApiConfig:BaseUrl"]
    ?? throw new InvalidOperationException("ApiConfig:BaseUrl não configurado em wwwroot/appsettings.json.");

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped(sp => new TokenRefreshHandler(
    sp.GetRequiredService<IJSRuntime>(),
    sp.GetRequiredService<AuthenticationStateProvider>(),
    apiBaseUrl
));
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<TokenRefreshHandler>();
    handler.InnerHandler = new HttpClientHandler();
    return new HttpClient(handler) { BaseAddress = new Uri(apiBaseUrl) };
});
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();
