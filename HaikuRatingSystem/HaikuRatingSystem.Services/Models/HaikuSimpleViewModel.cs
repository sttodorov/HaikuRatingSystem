namespace HaikuRatingSystem.Services.Models
{
    public class HaikuSimpleViewModel
    {
        public string Text { get; set; }
        
        public static bool IsValid(HaikuSimpleViewModel haiku)
        {
            return haiku.Text.Length > 3 && haiku.Text.Length < 101;
        }
    }
}