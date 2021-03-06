﻿using HaikuRatingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HaikuRatingSystem.Services.Models
{
    public class UserViewModel
    {
        public string Username { get; set; }

        public double Rating { get; set; }

        public ICollection<HaikuViewModel> Haikus { get; set; }

        public static Expression<Func<User, UserViewModel>> FromUser
        {
            get
            {
                var emptyHaikuList = new List<HaikuViewModel>();
                return user => new UserViewModel()
                {
                    Username = user.Username,
                    Rating = !user.Haikus.Any() ? 0 : !user.Haikus.Where(u => u.Ratings.Any()).Any() ? 0 : user.Haikus.Where(u => u.Ratings.Any()).Average(h => h.Ratings.Average(r => r.RatingValue)),
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
                Username = user.Username,
                Rating = !user.Haikus.Any() ? 0 : !user.Haikus.Where(u => u.Ratings.Any()).Any() ? 0 : user.Haikus.Where(u => u.Ratings.Any()).Average(h => h.Ratings.Average(r => r.RatingValue)),
                Haikus = user.Haikus.AsQueryable().Select(HaikuViewModel.FromHaiku).ToList()
            };
        }
    }
}