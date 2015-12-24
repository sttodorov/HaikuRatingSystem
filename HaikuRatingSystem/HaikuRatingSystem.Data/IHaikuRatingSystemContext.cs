namespace HaikuRatingSystem.Data.Repositories
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Models;

    public interface IHaikuRatingSystemContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Haiku> Haikus { get; set; }

        IDbSet<Rating> Ratings { get; set; }

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        IDbSet<T> SetEntity<T>() where T : class;

        int SaveChanges();
    }
}