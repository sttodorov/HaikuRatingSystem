using HaikuRatingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace HaikuRatingSystem.Services.Models
{
    public class HaikuViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; }

        public static Expression<Func<Haiku, HaikuViewModel>> FromHaiku
        {
            get
            {
                return h => new HaikuViewModel
                {
                    Id = h.HaikuId,
                    Text = h.Content,
                    Rating = h.Ratings.Average(r => r.RatingValue)
                };

            }
        }
    }
}