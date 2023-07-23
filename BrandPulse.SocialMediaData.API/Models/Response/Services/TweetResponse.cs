namespace BrandPulse.SocialMediaData.API.Models.Response.Services
{
    public class TweetResponse
    {
        public List<Tweet> statuses { get; set; }
        // Include other fields as needed...
    }
    public class Tweet
    {
        public string created_at { get; set; }
        public string id_str { get; set; }
        public string full_text { get; set; }
        // Include other fields as needed...
    }
}
