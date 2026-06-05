using MeetingReservation.Blazor.Auth;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace MeetingReservation.Blazor.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(
        HttpClient httpClient,
        IJSRuntime jsRuntime,
        AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
        _authStateProvider = authStateProvider;
    }

    public async Task<bool> LoginAsync(RequestLoginJson request)
    {
        var response = await _httpClient.PostAsJsonAsync("login", request);

        if (!response.IsSuccessStatusCode)
            return false;

        var loginResponse = await response.Content.ReadFromJsonAsync<ResponseRegisteredUserJson>();

        if (loginResponse?.Tokens is null)
            return false;

        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "accessToken", loginResponse.Tokens.AccessToken);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", loginResponse.Tokens.RefreshToken);

        ((CustomAuthenticationStateProvider)_authStateProvider).NotifyUserLogin(loginResponse.Tokens.AccessToken);

        return true;
    }

    public async Task<bool> RegisterCompanyAsync(RequestRegisterCompanyJson request)
    {
        var response = await _httpClient.PostAsJsonAsync("company", request);

        if (!response.IsSuccessStatusCode)
            return false;

        var registerResponse = await response.Content.ReadFromJsonAsync<ResponseRegisteredUserJson>();

        if (registerResponse?.Tokens is null)
            return false;

        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "accessToken", registerResponse.Tokens.AccessToken);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", registerResponse.Tokens.RefreshToken);

        ((CustomAuthenticationStateProvider)_authStateProvider).NotifyUserLogin(registerResponse.Tokens.AccessToken);

        return true;
    }

    public async Task LogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "accessToken");
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "refreshToken");
        ((CustomAuthenticationStateProvider)_authStateProvider).NotifyUserLogout();
    }
}
