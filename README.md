# MSUsuarios

API REST para gerenciamento de usuários, desenvolvida em .NET 8, com arquitetura em camadas, persistência em MySQL, integração com Docker e pipeline CI/CD no Azure DevOps.

## Sumário

- [Visão Geral](#visão-geral)
- [Funcionalidades](#funcionalidades)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Principais Tecnologias](#principais-tecnologias)
- [Configuração e Execução](#configuração-e-execução)
- [Endpoints](#endpoints)
- [Banco de Dados](#banco-de-dados)
- [CI/CD e Docker](#cicd-e-docker)
- [Contribuição](#contribuição)

---

## Visão Geral

O MSUsuarios é um microserviço para cadastro e consulta de usuários, seguindo boas práticas de arquitetura, separação de responsabilidades e uso de injeção de dependências.

## Funcionalidades

- Cadastro de novos usuários com validação de senha forte e e-mail único
- Listagem de todos os usuários cadastrados
- Persistência em banco MySQL
- Documentação automática via Swagger

## Estrutura do Projeto

```
MSUsuarios/
├── MSUsuarios/                # API principal (controllers, DTOs, configs)
├── MSUsuarios.Application/    # Serviços e regras de negócio
├── MSUsuarios.Domain/         # Entidades e interfaces de domínio
├── MSUsuarios.Infrastructure/ # Persistência, repositórios e contexto EF
├── azure-pipelines.yml        # Pipeline CI/CD Azure DevOps
├── Dockerfile                 # Build e execução em container
└── README.md
```

## Principais Tecnologias

- .NET 8 (ASP.NET Core)
- Entity Framework Core (MySQL)
- Docker
- Azure DevOps Pipelines
- Swagger

## Configuração e Execução

### Pré-requisitos

- .NET 8 SDK
- Docker (opcional, para container)
- MySQL (ou utilize o connection string do Azure)

### Configuração

1. Ajuste a string de conexão em `MSUsuarios/appsettings.json`:
	 ```json
	 "ConnectionStrings": {
		 "DefaultConnection": "Server=...;Database=...;User=...;Password=..."
	 }
	 ```

2. (Opcional) Configure variáveis de ambiente para produção.

### Execução Local

```powershell
dotnet restore
dotnet build
dotnet run --project MSUsuarios/MSUsuarios.csproj
```

Acesse: http://localhost:5000/swagger

### Docker

```powershell
docker build -t msusuarios .
docker run -p 80:80 msusuarios
```

## Endpoints

- `GET /api/Base/GetTodosUsuarios` — Lista todos os usuários
- `POST /api/Base/CadastrarNovoUsuario` — Cadastra novo usuário

## Banco de Dados

- Entidade principal: `Usuario` (IdUsuario, Nome, Email, Senha, Lvl)
- Migrations e configuração via EF Core
- Exemplo de migration inicial em `MSUsuarios.Infrastructure/Migrations/`

## CI/CD e Docker

- Pipeline Azure DevOps: build, restore, test, publish e deploy Docker
- Dockerfile multi-stage para build e execução otimizados

## Contribuição

1. Fork este repositório
2. Crie uma branch: `git checkout -b feature/sua-feature`
3. Commit suas alterações: `git commit -m 'feat: minha feature'`
4. Push: `git push origin feature/sua-feature`
5. Abra um Pull Request

---

Dúvidas? Abra uma issue!

---
