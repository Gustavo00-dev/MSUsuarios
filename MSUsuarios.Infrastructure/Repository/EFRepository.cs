
using Microsoft.EntityFrameworkCore;
using MSUsuarios.Infrastructure.Db;

namespace MSUsuarios.Domain.Interfaces
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected DbSet<T> _dbSet;

        public EFRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Alterar(T entidade)
        {
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        public void Cadastrar(T entidade)
        {
            _dbSet.Add(entidade);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            _dbSet.Remove(_dbSet.Find(id));
            _context.SaveChanges();
        }

        public T ObterPorId(int id)
        {
            return _dbSet.Find(id);
        }

        public IList<T> ObterTodos()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }
    }
}