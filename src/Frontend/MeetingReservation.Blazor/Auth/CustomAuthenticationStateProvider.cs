using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace MeetingReservation.Blazor.Auth;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;

    public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
        => _jsRuntime = jsRuntime;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "accessToken");
            if (string.IsNullOrWhiteSpace(token))
                return Anonymous();

            return new AuthenticationState(
                new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }
        catch
        {
            return Anonymous();
        }
    }

    public void NotifyUserLogin(string token)
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
        => NotifyAuthenticationStateChanged(Task.FromResult(Anonymous()));

    private static AuthenticationState Anonymous()
        => new(new ClaimsPrincipal(new ClaimsIdentity()));

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var kvps = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return kvps?.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)) ?? [];
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        base64 = (base64.Length % 4) switch
        {
            2 => base64 + "==",
            3 => base64 + "=",
            _ => base64
        };
        return Convert.FromBase64String(base64);
    }
}
