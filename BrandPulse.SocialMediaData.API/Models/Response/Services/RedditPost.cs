using Reddit.Controllers;

namespace BrandPulse.SocialMediaData.API.Models.Response.Services
{
    public class RedditPost
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Fullname { get; set; }
        public string Subreddit { get; set; }
        public string Author { get; set; }
        public int DownVotes { get; set; }
        public int UpVotes { get; set; }
        public double UpvoteRatio { get; set; }
        public int Score { get; set; }
        public IEnumerable<RedditComment> Comments { get; set; }
        public DateTime Created { get; set; }

    }

    public class RedditComment : RedditPost
    {
        public string Body { get; set; }

        public int? NumReplies { get; set; }
    }
}