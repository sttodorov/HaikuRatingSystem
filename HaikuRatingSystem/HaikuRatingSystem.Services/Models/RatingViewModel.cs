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

        public static bool IsValid(RatingViewModel rating)
        {
            return rating.RatingValue > 0 && rating.RatingValue < 6;
        }
    }
}