namespace HaikuRatingSystem.Data
{
    using HaikuRatingSystem.Data.Repositories;
    using Models;

    public interface IHaikuData
    {
        IGenericRepository<User> Users { get; }

        IGenericRepository<Haiku> Haikus { get; }

        IGenericRepository<Rating> Ratings { get; }

        void SaveChanges();
    }
}
