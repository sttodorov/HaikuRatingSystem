namespace HaikuRatingSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Rating
    {
        public int RatingId { get; set; }

        public Rating()
        {
            this.DateCreated = DateTime.Now;
        }

        public virtual Haiku Haiku { get; set; }

        [Required]
        [Range(1,5)]
        public int RatingValue { get; set; }

        public DateTime DateCreated { get;  set; }
    }
}
