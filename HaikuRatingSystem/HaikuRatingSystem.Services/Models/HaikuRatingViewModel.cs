using HaikuRatingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace HaikuRatingSystem.Services.Models
{
    public class HaikuRatingViewModel
    {
        public double Rating { get; set; }

        public static Expression<Func<Haiku, HaikuRatingViewModel>> FromHaiku
        {
            get
            {
                return h => FromHaikuModel(h);
            }
        }

        public static HaikuRatingViewModel FromHaikuModel(Haiku haiku)
        {
            return new HaikuRatingViewModel
            {
                Rating = haiku.Ratings.Average(r => r.RatingValue)
            };
        }
    }
}