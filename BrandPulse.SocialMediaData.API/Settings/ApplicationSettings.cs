namespace BrandPulse.SocialMediaData.API.Settings
{
    public class ApplicationSettings
    {
        public YouTubeSettings YouTubeSettings { get; set; }

        public RedditSettings RedditSettings { get; set; }

        public TwitterSettings TwitterSettings { get; set; }

        public int MaxResults { get; set; }

        public int MaxComments { get; set; }
    }
}
