# MeetingReservation

Sistema de reserva de salas de reunião desenvolvido em .NET, seguindo os princípios de **SOLID** e **DDD (Domain-Driven Design)**. A API REST está funcional e pronta para uso, enquanto o frontend em Blazor WebAssembly está em desenvolvimento ativo.

---

## Visão Geral

O MeetingReservation permite que empresas gerenciem suas salas de reunião e as reservas de seus colaboradores. O sistema é **multi-tenant**, ou seja, cada empresa possui seu próprio espaço isolado de dados, garantindo segurança e privacidade.

### Funcionalidades da API

- Gerenciamento de empresas (cadastro e configuração)
- Gerenciamento de usuários com controle de perfis (Usuário e Administrador)
- Cadastro e consulta de salas de reunião
- Criação, edição, filtragem e exclusão de reservas
- Autenticação via JWT com suporte a refresh token
- Isolamento de dados por empresa (multi-tenancy)

---

## Arquitetura

O projeto segue **Clean Architecture**, com separação clara de responsabilidades em camadas independentes:

```
src/
├── Backend/
│   ├── MeetingReservation.API            # Camada de apresentação (controllers, filtros, DI)
│   ├── MeetingReservation.Application    # Casos de uso e regras de negócio
│   ├── MeetingReservation.Domain         # Entidades, interfaces e contratos do domínio
│   └── MeetingReservation.Infrastructure # Acesso a dados, segurança e migrações
├── Frontend/
│   └── MeetingReservation.Blazor         # Frontend em Blazor WebAssembly (em desenvolvimento)
└── Shared/
    ├── MeetingReservation.Communication  # DTOs compartilhados entre API e frontend
    └── MeetingReservation.Exceptions     # Exceções de domínio centralizadas
```

### Responsabilidades das camadas

| Camada | Responsabilidade |
|---|---|
| **API** | Entrada HTTP, autenticação, roteamento, documentação Swagger |
| **Application** | Casos de uso, validações com FluentValidation, orquestração |
| **Domain** | Entidades, interfaces de repositório, enums e serviços de domínio |
| **Infrastructure** | EF Core, repositórios, migrações, geração e validação de tokens JWT, criptografia |
| **Communication** | Contratos de request/response compartilhados |
| **Exceptions** | Hierarquia de exceções de domínio |

### Padrões aplicados

- **Repository Pattern** com segregação de interfaces (`IReadOnlyRepository`, `IWriteOnlyRepository`, `IUpdateOnlyRepository`)
- **Unit of Work** para controle transacional
- **Use Case Pattern** — um caso de uso por operação
- **Dependency Injection** via extensões organizadas por camada
- **Multi-tenancy** por `CompanyId` extraído do token JWT

---

## Tecnologias

### Backend
- **.NET 9** / ASP.NET Core
- **Entity Framework Core 9** + **Dapper**
- **MySQL 8** via Pomelo
- **FluentMigrator** para migrações de banco
- **FluentValidation** para validações
- **JWT Bearer** para autenticação
- **BCrypt.Net** para hash de senhas
- **Swagger / Swashbuckle** para documentação da API

### Frontend
- **.NET 10** / Blazor WebAssembly *(em desenvolvimento)*

### Testes
- **xUnit**, **Shouldly**, **coverlet**

### DevOps
- **Docker** + **Docker Compose**

---

## Domínio

As entidades principais do sistema são:

- **Company** — representa a empresa/tenant
- **User** — colaborador vinculado a uma empresa, com papel de `User` ou `Admin`
- **Room** — sala de reunião pertencente a uma empresa
- **Reservation** — reserva de uma sala, com data/hora de início e fim, e número de participantes
- **RefreshToken** — token de renovação de sessão armazenado no banco

---

## Como executar

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) e Docker Compose

### Com Docker (recomendado)

```bash
docker-compose -f Docker/docker-compose.yml up --build
```

Isso sobe o banco de dados MySQL e a API automaticamente. As migrações são aplicadas na inicialização.

A API estará disponível em: `http://localhost:8081`

### Localmente

1. Configure a string de conexão em `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=meetingreservation;User=root;Password=sua_senha;"
  },
  "Settings": {
    "Jwt": {
      "SigningKey": "sua_chave_secreta_com_pelo_menos_32_caracteres",
      "ExpirationTime": 1500,
      "Issuer": "MeetingReservationApi",
      "Audience": "MeetingReservationClients"
    }
  }
}
```

2. Execute a API:

```bash
dotnet run --project src/Backend/MeetingReservation.API
```

A documentação Swagger estará disponível em: `http://localhost:<porta>/swagger`

---

## Testes

```bash
dotnet test
```

Os testes cobrem casos de uso, validações e endpoints da API.

---

## Status do Projeto

| Componente | Status |
|---|---|
| API REST | Funcional |
| Autenticação JWT + Refresh Token | Funcional |
| Multi-tenancy | Funcional |
| Docker / Docker Compose | Configurado |
| Frontend Blazor WebAssembly | Em desenvolvimento |

---

## Contribuindo

1. Faça um fork do repositório
2. Crie uma branch para sua feature: `git checkout -b feature/minha-feature`
3. Commit suas alterações: `git commit -m 'feat: adiciona minha feature'`
4. Push para a branch: `git push origin feature/minha-feature`
5. Abra um Pull Request

---

## Licença

Este projeto está sob a licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.
