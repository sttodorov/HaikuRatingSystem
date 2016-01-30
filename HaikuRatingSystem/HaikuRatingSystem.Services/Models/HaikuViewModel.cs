using HaikuRatingSystem.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace HaikuRatingSystem.Services.Models
{
    public class HaikuViewModel
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public DateTime DatePublished { get; set; }

        public string Text { get; set; }

        public double Rating { get; set; }

        public static Expression<Func<Haiku, HaikuViewModel>> FromHaiku
        {
            get
            {
                return h => new HaikuViewModel
                {
                    Id = h.HaikuId,
                    AuthorName = h.Author.Username,
                    Text = h.Text,
                    DatePublished = h.DatePublished,
                    Rating = !h.Ratings.Any() ? 0 : h.Ratings.Average(r => r.RatingValue)
                };
            }
        }
        public static HaikuViewModel FromHaikuModel(Haiku h)
        {
            return new HaikuViewModel
            {
                Id = h.HaikuId,
                AuthorName = h.Author.Username,
                Text = h.Text,
                DatePublished = h.DatePublished,
                Rating = !h.Ratings.Any() ? 0 : h.Ratings.Average(r => r.RatingValue)
            };
        }
    }
}