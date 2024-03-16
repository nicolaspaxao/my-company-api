using System.Linq.Expressions;

namespace CompanyAPI.Repositories.__base
{
    public interface IRepository<T>{
        IQueryable<T> Get ();
        Task<T> GetById ( Expression<Func<T , bool>> precidate );
        void Add ( T entity );
        void Update ( T entity );
        void Delete ( T entity );
    }
}
