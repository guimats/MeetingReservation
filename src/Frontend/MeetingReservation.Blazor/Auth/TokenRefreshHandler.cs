using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MeetingReservation.Blazor.Auth;

public class TokenRefreshHandler : DelegatingHandler
{
    private readonly IJSRuntime _jsRuntime;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly string _refreshEndpoint;

    public TokenRefreshHandler(
        IJSRuntime jsRuntime,
        AuthenticationStateProvider authStateProvider,
        string apiBaseUrl)
    {
        _jsRuntime = jsRuntime;
        _authStateProvider = authStateProvider;
        _refreshEndpoint = $"{apiBaseUrl.TrimEnd('/')}/token/refresh-token";
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Buffer body para poder reenviar a requisição original após renovar o token
        byte[]? bodyBytes = null;
        List<KeyValuePair<string, IEnumerable<string>>>? contentHeaders = null;
        if (request.Content is not null)
        {
            bodyBytes = await request.Content.ReadAsByteArrayAsync(cancellationToken);
            contentHeaders = request.Content.Headers.ToList();
        }

        var accessToken = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "accessToken");
        if (!string.IsNullOrWhiteSpace(accessToken))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            return response;

        // Tenta renovar o token
        var refreshToken = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "refreshToken");
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            await ForceLogoutAsync();
            return response;
        }

        var refreshRequest = new HttpRequestMessage(HttpMethod.Post, _refreshEndpoint)
        {
            Content = JsonContent.Create(new RequestNewTokenJson { RefreshToken = refreshToken })
        };
        var refreshResponse = await base.SendAsync(refreshRequest, cancellationToken);

        if (!refreshResponse.IsSuccessStatusCode)
        {
            await ForceLogoutAsync();
            return response;
        }

        var tokens = await refreshResponse.Content
            .ReadFromJsonAsync<ResponseTokensJson>(cancellationToken: cancellationToken);

        if (tokens is null)
        {
            await ForceLogoutAsync();
            return response;
        }

        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "accessToken", tokens.AccessToken);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", tokens.RefreshToken);
        ((CustomAuthenticationStateProvider)_authStateProvider).NotifyUserLogin(tokens.AccessToken);

        // Reenvia a requisição original com o novo token
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
        if (bodyBytes is not null)
        {
            request.Content = new ByteArrayContent(bodyBytes);
            foreach (var h in contentHeaders!)
                request.Content.Headers.TryAddWithoutValidation(h.Key, h.Value);
        }

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task ForceLogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "accessToken");
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "refreshToken");
        ((CustomAuthenticationStateProvider)_authStateProvider).NotifyUserLogout();
    }
}
