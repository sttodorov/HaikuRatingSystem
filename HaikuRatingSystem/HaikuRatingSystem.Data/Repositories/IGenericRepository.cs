namespace HaikuRatingSystem.Data.Repositories
{
    using System.Linq;

    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> All();

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        T Delete(T entity);

        void Detach(T entity);

        void SaveChanges();
    }
}
