using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace MeetingReservation.Blazor.Services;

public class RoomService : IRoomService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;

    public RoomService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
    }

    public async Task<List<ResponseShortRoomJson>> FilterRoomsAsync(RequestFilterRoomsJson filter)
    {
        var response = await _httpClient.PostAsJsonAsync("room/filter", filter);

        if (!response.IsSuccessStatusCode)
            return [];

        var result = await response.Content.ReadFromJsonAsync<ResponseRoomsJson>();
        return result?.Rooms?.ToList() ?? [];
    }

    public async Task<bool> CreateRoomAsync(RequestRoomJson request)
    {
        request.UserId = await GetCurrentUserIdAsync();

        var response = await _httpClient.PostAsJsonAsync("room", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteRoomAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"room/{id}");
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
