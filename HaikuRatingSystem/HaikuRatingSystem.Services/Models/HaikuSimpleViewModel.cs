using HaikuRatingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace HaikuRatingSystem.Services.Models
{
    public class HaikuSimpleViewModel
    {
        public string Text { get; set; }
        public static Expression<Func<Haiku, HaikuSimpleViewModel>> FromHaiku
        {
            get
            {
                return h => new HaikuSimpleViewModel
                {
                    Text = h.Text
                };

            }
        }

        public bool IsValid()
        {
            return this.Text.Length > 3 && this.Text.Length < 101;
        }
    }
}