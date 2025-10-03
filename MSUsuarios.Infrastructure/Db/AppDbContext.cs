using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSUsuarios.Domain.Entities;

namespace MSUsuarios.Infrastructure.Db
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		}
		public DbSet<Usuario> Usuario { get; set; }
	}

	public static class AppDbContextExtensions
	{
		public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
			}, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped);
			return services;
		}
	}
}
