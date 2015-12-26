using HaikuRatingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace HaikuRatingSystem.Services.Models
{
    public class RatingViewModel
    {
        public int Rating { get; set; }

        public static Expression<Func<Rating, RatingViewModel>> FromRating
        {
            get
            {
                return r => new RatingViewModel()
                {
                    Rating = r.RatingValue
                };

            }
        }

        public static RatingViewModel FromRatingModel(Rating rating)
        {
            return new RatingViewModel()
            {
                Rating = rating.RatingValue
            };
        }

        public bool IsValid()
        {
            return this.Rating > 0 && this.Rating < 6;
        }
    }
}