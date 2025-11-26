using MeetingReservation.Domain.Services.TenantProvider;

namespace MeetingReservation.API.Providers.Tenant;

public class TenantProvider : ITenantProvider
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public TenantProvider(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public long GetCompanyId()
	{
		// 1. Verificação de segurança: Se não tem contexto (ex: rodando migration ou background job), retorna 0
		var httpContext = _httpContextAccessor.HttpContext;
		if (httpContext == null) return 0;

		// 2. Busca a claim que gravamos no passo anterior
		var companyIdClaim = httpContext.User.FindFirst("CompanyId");

		// 3. Se achou, converte e retorna. Se não, retorna 0.
		if (companyIdClaim is not null && long.TryParse(companyIdClaim.Value, out var companyId))
		{
			return companyId;
		}

		return 0; // Fallback seguro
	}
}

