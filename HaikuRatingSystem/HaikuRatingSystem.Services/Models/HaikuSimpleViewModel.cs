namespace HaikuRatingSystem.Services.Models
{
    public class HaikuSimpleViewModel
    {
        public string Text { get; set; }
        
        public bool IsValid()
        {
            return this.Text.Length > 3 && this.Text.Length < 101;
        }
    }
}