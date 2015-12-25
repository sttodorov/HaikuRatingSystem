using HaikuRatingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace HaikuRatingSystem.Services.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }

        public double Rating { get; set; }

        public ICollection<HaikuViewModel> Haikus { get; set; }

        public static Expression<Func<User, UserViewModel>> FromUser
        {
            get
            {
                return user => new UserViewModel()
                {
                    UserName = user.UserName,
                    Rating = !user.Haikus.Any() ? 0 : user.Haikus.Average(h => h.Ratings.Average(r => r.RatingValue)),
                    Haikus = user.Haikus.AsQueryable().Select(HaikuViewModel.FromHaiku).ToList()
                };
            }
        }

        public static UserViewModel FromUserModel(User user)
        {
            if (user == null)
                return null;

            return new UserViewModel()
            {
                UserName = user.UserName,
                Rating = !user.Haikus.Any() ? 0 : user.Haikus.Average(h => h.Ratings.Average(r => r.RatingValue)),
                Haikus = user.Haikus.AsQueryable().Select(HaikuViewModel.FromHaiku).ToList()
            };
        }
    }
}