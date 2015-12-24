namespace HaikuRatingSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Rating
    {
        public Rating()
        {
            this.RatingId = Guid.NewGuid().ToString();
            this.DateCreated = DateTime.Now;
        }

        public string RatingId { get; set; }

        public virtual User User{ get; set; }

        public virtual Haiku Haiku { get; set; }

        [Required]
        [Range(1,5)]
        public int RatingValue { get; set; }

        public DateTime DateCreated { get;  set; }
    }
}
