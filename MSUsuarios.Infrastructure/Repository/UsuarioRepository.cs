using MSUsuarios.Domain.Entities;
using MSUsuarios.Domain.Interfaces;
using MSUsuarios.Infrastructure.Db;

namespace MSUsuarios.Infrastructure.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {

    }

    public class UsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        {

        }
    }
}