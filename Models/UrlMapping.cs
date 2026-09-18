namespace minimal_api_shorterUrl.Models
{
    public class UrlMapping
    {
        public Guid IdUrl { get; set; }
        public string LongUrl { get; set; } = string.Empty;
        public string ShortCode { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}