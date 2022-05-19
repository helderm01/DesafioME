using Desafio.ME.Database.Context;
using Desafio.ME.Dominio;
using System.Linq.Expressions;

namespace Desafio.ME.Database.Repositorios
{
    public class RepositorioBase<T> where T : EntidadeBase
    {
        protected readonly MEContext Context;

        public RepositorioBase(MEContext context)
        {
            Context = context;
        }

        public bool Any(int id)
        {
            return Context.Set<T>().Any(x => x.Id == id);
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Any(expression);
        }

        public T Get(int id)
        {
            return Context.Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public void Post(T entidade)
        {
            Context.Set<T>().Add(entidade);
            Context.SaveChanges();
        }

        public void Update(T entidade)
        {
            Context.Set<T>().Update(entidade);
            Context.SaveChanges();
        }

        public void Delete(T entidade)
        {
            Context.Set<T>().Remove(entidade);
            Context.SaveChanges();
        }
    }
}
