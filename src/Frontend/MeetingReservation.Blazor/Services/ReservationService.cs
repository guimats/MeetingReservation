using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace MeetingReservation.Blazor.Services;

public class ReservationService : IReservationService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;

    public ReservationService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
    }

    public async Task<List<ResponseShortReservationJson>> FilterReservationsAsync(RequestFilterReservationsJson filter)
    {
        var response = await _httpClient.PostAsJsonAsync("reservation/filter", filter);

        if (!response.IsSuccessStatusCode)
            return [];

        var result = await response.Content.ReadFromJsonAsync<ResponseReservationsJson>();
        return result?.Reservations?.ToList() ?? [];
    }

    public async Task<bool> CreateReservationAsync(RequestReservationJson request)
    {
        request.UserId = await GetCurrentUserIdAsync();

        var response = await _httpClient.PostAsJsonAsync("reservation", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteReservationAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"reservation/{id}");
        return response.IsSuccessStatusCode;
    }

    private async Task<long> GetCurrentUserIdAsync()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)
                   ?? user.FindFirst("sub")
                   ?? user.FindFirst("nameid");

        if (idClaim is not null && long.TryParse(idClaim.Value, out var userId))
            return userId;

        return 0;
    }
}
