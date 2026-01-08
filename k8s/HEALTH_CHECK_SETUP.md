# Health Check Endpoints para HPA

Para que o HPA funcione corretamente com health checks (liveness e readiness probes), a aplicação precisa implementar endpoints de saúde.

## 📍 Endpoint Necessário

**Path**: `/healthz`  
**Method**: GET  
**Response**: HTTP 200 OK

## 🔧 Implementação em ASP.NET Core

Adicione ao seu `Program.cs`:

```csharp
// Health Check
builder.Services.AddHealthChecks();

// ... outras configurações

var app = builder.Build();

// Configure o middleware de health checks
app.MapHealthChecks("/healthz");

// ... outros endpoints
app.MapControllers();
app.Run();
```

## 📦 Com Pacote de Health Checks

Se usar o pacote completo:

```bash
dotnet add package Microsoft.Extensions.Diagnostics.HealthChecks
dotnet add package Microsoft.Extensions.Diagnostics.HealthChecks.SqlServer
```

Então em `Program.cs`:

```csharp
builder.Services.AddHealthChecks()
    .AddSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        name: "database",
        tags: new[] { "db" }
    );

var app = builder.Build();

app.MapHealthChecks("/healthz");
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("ready")
});
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("live")
});

app.MapControllers();
app.Run();
```

## ✅ Teste o Endpoint

```bash
# Localmente
curl http://localhost:80/healthz

# No pod do Kubernetes
kubectl exec -it <pod-name> -- curl http://localhost:80/healthz

# Ou
kubectl port-forward deployment/msusuarios 8080:80
curl http://localhost:8080/healthz
```

## 📊 Response Esperado

```json
{
    "status": "Healthy",
    "checks": {
        "database": {
            "status": "Healthy",
            "description": "Database is responsive",
            "duration": "00:00:00.0123456"
        }
    }
}
```

## 🚀 Verificar Health Check no Kubernetes

```bash
# Ver status dos probes
kubectl describe pod -l app=msusuarios

# Deve mostrar algo como:
# Liveness:       http-get http://:80/healthz delay=30s timeout=1s period=10s
# Readiness:      http-get http://:80/healthz delay=10s timeout=1s period=5s
```

---

**Nota**: O endpoint `/healthz` é chamado continuamente pelo Kubernetes para verificar a saúde do pod. Certifique-se de que ele responda rapidamente (<1 segundo).
