using MSUsuarios.Infrastructure.Db;
using MSUsuarios.Infrastructure.Configuracao;
using MSUsuarios.Application.Configuracao;

var builder = WebApplication.CreateBuilder(args);

var configurations = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
        {
            // Configura Swagger UI
            c.SwaggerDoc("v1", new() { Title = "MSUsuarios", Version = "v1" });

            //Documentação XML para os controllers e modelos
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

// Configuração do banco de dados centralizada
builder.Services.AddAppDbContext(configurations);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services
    .AddInfraDependencies()
    .AddApplicationDependencies();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
