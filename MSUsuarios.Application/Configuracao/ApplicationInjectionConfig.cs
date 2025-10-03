using Microsoft.Extensions.DependencyInjection;
using MSUsuarios.Application.Services;

namespace MSUsuarios.Application.Configuracao
{
    public static class ApplicationInjectionConfig
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<UsuarioService>();
            return services;
        }
    }
}