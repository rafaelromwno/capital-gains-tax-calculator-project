
## 📂 Estrutura por projeto

### **1. MyProject.API** (ASP.NET Core Web API)

```shell
 ├── Controllers/          # Controllers / Endpoints
 ├── Middlewares/          # Middlewares customizados
 ├── Filters/              # Filtros de exceção, validação
 ├── Extensions/           # Métodos de extensão (Swagger, Auth, etc.)
 ├── Configurations/       # Configuração de serviços (Swagger, CORS, etc.)
 ├── Program.cs            # Entry point
 └── appsettings.json` 
```


### **2. MyProject.Application** (Class Library)

```shell
 ├── Interfaces/           # Contratos (ports) usados pela aplicação
 ├── DTOs/                 # Objetos de transferência de dados
 ├── Validators/           # Validações (FluentValidation, etc.)
 ├── Behaviors/            # Pipeline behaviors do MediatR (logging, validation)
 ├── Commands/             # Escrita (CQRS)
 │    ├── Handlers/        # CommandHandlers
 │    └── Requests/        # Command Requests
 ├── Queries/              # Leitura (CQRS)
 │    ├── Handlers/        # QueryHandlers
 │    └── Requests/        # Query Requests
 └── Services/             # Regras de orquestração de casos de uso` 
```

### **3. MyProject.Domain** (Class Library)

```shell
 ├── Entities/             # Entidades principais do negócio
 ├── ValueObjects/         # Objetos de valor (ex.: CPF, Email, Dinheiro)
 ├── Aggregates/           # Agregados de domínio
 ├── Enums/                # Enumerações de negócio
 ├── Events/               # Eventos de domínio
 ├── Exceptions/           # Exceções de domínio
 └── Interfaces/           # Contratos de repositórios (ports)` 
```


### **4. MyProject.Infra** (Class Library)

```shell
 ├── Data/
 │    ├── Context/         # DbContext (EF Core) ou conexões (Dapper)
 │    ├── Configurations/  # Mapeamentos de entidades (Fluent API)
 │    ├── Migrations/      # Arquivos de migração do EF Core
 │    └── Repositories/    # Implementações de repositórios
 ├── IoC/
 │    └── DependencyInjection.cs # Configuração de DI
 ├── Messaging/            # (opcional) Integração com fila/eventos (Kafka, RabbitMQ)
 └── Services/             # Implementações de serviços externos (ex.: Email, API externa)` 
```

### **5. MyProject.UnitTests** (xUnit)

```shell
 ├── Domain/
 │    ├── Entities/        # Testes de entidades
 │    ├── ValueObjects/    # Testes de objetos de valor
 │    └── Services/        # Testes de regras de negócio
 └── Application/
      ├── Commands/        # Testes de CommandHandlers
      └── Queries/         # Testes de QueryHandlers` 
```
### **6. MyProject.IntegrationTests** (xUnit)

```shell
 ├── API/
 │    └── Controllers/     # Testes de endpoints (usando TestServer/WebApplicationFactory)
 ├── Infra/
 │    └── Repositories/    # Testes de repositórios reais (com DB in-memory/testcontainers)
 └── Fixtures/             # Configuração de cenários de teste (db, server, mocks)` 
```

## 🔑 Regras principais

-   **Domain não depende de nada.**    
-   **Application depende apenas do Domain.**    
-   **Infra depende do Application e Domain.**    
-   **API depende de todas (Application + Infra + Domain).**    
-   **Tests dependem da camada que estão testando.**
