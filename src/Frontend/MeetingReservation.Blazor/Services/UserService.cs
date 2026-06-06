using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace MeetingReservation.Blazor.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;

    public UserService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
    }

    public async Task<List<ResponseUserProfileJson>> FilterUsersAsync(RequestFilterUsersJson filter)
    {
        var response = await _httpClient.PostAsJsonAsync("user/filter", filter);

        if (!response.IsSuccessStatusCode)
            return [];

        var result = await response.Content.ReadFromJsonAsync<ResponseUsersJson>();
        return result?.Users?.ToList() ?? [];
    }

    public async Task<bool> CreateUserAsync(RequestRegisterUserJson request)
    {
        request.CompanyId = await GetCurrentCompanyIdAsync();

        var response = await _httpClient.PostAsJsonAsync("user", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteUserAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"user/{id}");
        return response.IsSuccessStatusCode;
    }

    private async Task<long> GetCurrentCompanyIdAsync()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        var companyIdClaim = user.FindFirst("CompanyId");

        if (companyIdClaim is not null && long.TryParse(companyIdClaim.Value, out var companyId))
            return companyId;

        return 0;
    }
}
