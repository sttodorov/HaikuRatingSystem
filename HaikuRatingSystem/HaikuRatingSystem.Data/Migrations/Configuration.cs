namespace HaikuRatingSystem.Data.Migrations
{
    using HaikuRaitingSystem.Common;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HaikuRatingSystem.Data.Repositories.HaikuRatingSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(HaikuRatingSystem.Data.Repositories.HaikuRatingSystemContext context)
        {
            var simpleUser = (new User() { UserName = "xstox", PasswordHash = Encryptor.GenerateHash("1234") });
            var simpleHaiku = new Haiku() { Content = "My First haiku is better than Haiku", Author = simpleUser};
            if (!context.Users.Any())
            {
                context.Users.Add(simpleUser);
            }
            if (!context.Haikus.Any())
            {
                context.Haikus.Add(simpleHaiku);
            }
            if (!context.Ratings.Any())
            {
                context.Ratings.Add(new Rating() { Haiku = simpleHaiku, RatingValue = 4});
            }
            context.SaveChanges();
        }
    }
}
