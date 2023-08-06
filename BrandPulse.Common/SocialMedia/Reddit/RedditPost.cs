using User = Reddit.Controllers.User;

namespace BrandPulse.Domain.SocialMedia.Reddit
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
        public RedditUser? User { get; set; }
        public IEnumerable<RedditComment> Comments { get; set; } = Enumerable.Empty<RedditComment>();
        public DateTime Created { get; set; }

    }

    public class RedditUser
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int PostKarma { get; set; }
        public int CommentKarma { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public string ProfileUrl { get; set; } = string.Empty;
    }

    public class RedditComment : RedditPost
    {
        public string Body { get; set; }

        public int? NumReplies { get; set; }
    }
}