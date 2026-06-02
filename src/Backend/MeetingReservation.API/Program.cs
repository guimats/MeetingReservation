
using MeetingReservation.API.Filters;
using MeetingReservation.API.Providers.Tenant;
using MeetingReservation.API.Token;
using MeetingReservation.Application;
using MeetingReservation.Domain.Security.Tokens;
using MeetingReservation.Domain.Services.TenantProvider;
using MeetingReservation.Infrastructure;
using MeetingReservation.Infrastructure.Extensions;
using MeetingReservation.Infrastructure.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
    });
});

// Configurando Filtro que sera utilizado
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

// adicionando injecoes de dependencia
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();
builder.Services.AddScoped<ITenantProvider, TenantProvider>();

//deixando os nomes dos endpoints em minusculo na URL
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

var jwtSettings = builder.Configuration.GetSection("Settings:Jwt");
var signingKey = jwtSettings["SigningKey"] ?? throw new InvalidOperationException("Settings:Jwt:SigningKey n�o configurado.");

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,

		// Usando a chave configurada para validar a assinatura
		//ValidIssuer = jwtSettings["Issuer"],
		//ValidAudience = jwtSettings["Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
	};
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontendBlazor", policy =>
    {
        // Coloque a URL onde o seu Blazor Web (o projeto hospedeiro) está rodando
        policy.WithOrigins("http://localhost:7250")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PermitirFrontendBlazor");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

ExecutarMigracaoComRetry();

app.Run();

void ExecutarMigracaoComRetry()
{
	using (IServiceScope escopo = app.Services.CreateScope())
	{
		string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
		Console.WriteLine("CONNECTION STRING EM USO:");
		Console.WriteLine(connectionString);

		int tentativaAtual = 1;
		int maximoTentativas = 10;
		bool sucesso = false;

		while (tentativaAtual <= maximoTentativas && !sucesso)
		{
			try
			{
				// Linha onde ocorre o erro atualmente
				DatabaseMigration.Migrate(connectionString, escopo.ServiceProvider);
				sucesso = true;
				Console.WriteLine("Conex�o estabelecida e migra��o conclu�da!");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[Tentativa {tentativaAtual}/{maximoTentativas}] MySQL ainda iniciando. Aguardando 5 segundos...");
				tentativaAtual++;

				if (tentativaAtual > maximoTentativas)
				{
					throw new Exception("N�o foi poss�vel conectar ao MySQL ap�s v�rias tentativas.", ex);
				}

				Thread.Sleep(5000);
			}
		}
	}
}

public partial class Program
{
    protected Program() { }
}