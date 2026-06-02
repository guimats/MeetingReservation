using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace MeetingReservation.Web.Client.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        // Método chamado automaticamente pelo Blazor para saber se o usuário está logado
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                // Tenta buscar o token do LocalStorage do navegador
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "accessToken");

                if (string.IsNullOrWhiteSpace(token))
                {
                    // Retorna um usuário anônimo
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                // Se tem token, cria a identidade do usuário baseada nele
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        // Chamado pelo nosso AuthService quando o login dá certo
        public void NotifyUserLogin(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            // Dispara o evento que atualiza a tela instantaneamente
            NotifyAuthenticationStateChanged(authState);
        }

        // Chamado pelo nosso AuthService quando o usuário clica em Sair
        public void NotifyUserLogout()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));

            NotifyAuthenticationStateChanged(authState);
        }

        // Método auxiliar para quebrar a string do JWT e ler as informações de dentro dele
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs == null)
            {
                return new List<Claim>();
            }

            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
        }

        // Auxiliar matemático para lidar com o preenchimento do Base64 do JWT
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
