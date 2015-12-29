using HaikuRatingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace HaikuRatingSystem.Services.Models
{
    public class HaikuCreationViewModel
    {
        public int Id { get; set; }
        public DateTime DatePublished { get; set; }

        public static Expression<Func<Haiku, HaikuCreationViewModel>> FromHaiku
        {
            get
            {
                return h => new HaikuCreationViewModel
                {
                    Id = h.HaikuId,
                    DatePublished = h.DatePublished
                };

            }
        }
    }
}