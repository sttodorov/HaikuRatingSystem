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
        public int RatingValue { get; set; }

        public static Expression<Func<Rating, RatingViewModel>> FromRating
        {
            get
            {
                return r => new RatingViewModel()
                {
                    RatingValue = r.RatingValue
                };

            }
        }

        public static RatingViewModel FromRatingModel(Rating rating)
        {
            return new RatingViewModel()
            {
                RatingValue = rating.RatingValue
            };
        }

        public bool IsValid()
        {
            return this.RatingValue > 0 && this.RatingValue < 6;
        }
    }
}