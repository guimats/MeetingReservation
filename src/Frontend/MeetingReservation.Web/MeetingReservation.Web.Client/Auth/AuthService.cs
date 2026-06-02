using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Web.Client.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MeetingReservation.Web.Client.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> LoginAsync(RequestLoginJson request)
        {
            var response = await _httpClient.PostAsJsonAsync("login", request);

            if (response.IsSuccessStatusCode)
                return false;
            
            var tokenResponse = await response.Content.ReadFromJsonAsync<ResponseTokensJson>();

            if (tokenResponse == null)
                return false;

            // Salva os tokens no LocalStorage usando JavaScript Interop nativo
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "accessToken", tokenResponse.AccessToken);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", tokenResponse.RefreshToken);

            // Injeta o token no cabeçalho do HttpClient para as próximas requisições
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokenResponse.AccessToken);

            // Notifica o sistema de autenticação do Blazor que o login ocorreu
            ((CustomAuthenticationStateProvider)_authStateProvider).NotifyUserLogin(tokenResponse.AccessToken);

            return true;
        }

        public async Task LogoutAsync()
        {
            // Limpa os tokens do navegador
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "accessToken");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "refreshToken");

            // Remove do HttpClient
            _httpClient.DefaultRequestHeaders.Authorization = null;

            // Notifica o Blazor
            ((CustomAuthenticationStateProvider)_authStateProvider).NotifyUserLogout();
        }
    }
}
