namespace HaikuRatingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Haiku
    {
        public int HaikuId { get; set; }

        public Haiku()
        {
            this.Ratings = new HashSet<Rating>();
            this.IsReported = false;
            this.DateCreated = DateTime.Now;
            this.DateReported = new DateTime(1900, 1, 1);
        }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Content { get; set; }

        [Required]
        public virtual User Author { get; set; }

        public bool IsReported { get; set; }

        public DateTime DateReported { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}