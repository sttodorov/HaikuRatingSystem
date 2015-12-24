using System;
using System.Data.Entity;
using HaikuRatingSystem.Models;
using HaikuRatingSystem.Data.Migrations;

namespace HaikuRatingSystem.Data.Repositories
{
    public class HaikuRatingSystemContext : DbContext, IHaikuRatingSystemContext
    {
        public HaikuRatingSystemContext()
            :base("HaikuRatingSystemConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<HaikuRatingSystemContext, Configuration>());
        }

        public IDbSet<Haiku> Haikus { get; set; }

        public IDbSet<Rating> Ratings { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<T> SetEntity<T>() where T : class
        {
            return base.Set<T>();
        }

        public static HaikuRatingSystemContext Create()
        {
            return new HaikuRatingSystemContext();
        }
    }
}