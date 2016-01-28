namespace HaikuRatingSystem.Services.Models
{
    using HaikuRatingSystem.Models;

    public class RatingViewModel
    {
        public int RatingValue { get; set; }
        
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