using Microsoft.Extensions.DependencyInjection;
using MSUsuarios.Infrastructure.Repository;

namespace MSUsuarios.Infrastructure.Configuracao
{
    public static class InfraInjectionConfig
    {
        public static IServiceCollection AddInfraDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            return services;
        }
    }
}