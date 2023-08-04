namespace BrandPulse.Domain.SocialMedia.Tweeter
{
    public class TweetResponse
    {
        public List<Tweet> statuses { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Tweet
    {
        public string created_at { get; set; }
        public string id_str { get; set; }
        public string full_text { get; set; }
        public bool truncated { get; set; }
        public List<int> display_text_range { get; set; }
        public Entities entities { get; set; }
        public ExtendedEntities extended_entities { get; set; }
        public Metadata metadata { get; set; }
        public string source { get; set; }
        public long? in_reply_to_status_id { get; set; }
        public long? in_reply_to_user_id { get; set; }
        public TwitterUser user { get; set; }
        public int retweet_count { get; set; }
        public int favorite_count { get; set; }
        public bool favorited { get; set; }
        public bool retweeted { get; set; }
        public string lang { get; set; }
    }

    public class Entities
    {
        public List<Hashtag> hashtags { get; set; }
        public List<Media> media { get; set; }
    }

    public class ExtendedEntities
    {
        public List<Media> media { get; set; }
    }

    public class Media
    {
        public string id_str { get; set; }
        public string media_url { get; set; }
        public string media_url_https { get; set; }
        public string url { get; set; }
        public string display_url { get; set; }
        public string expanded_url { get; set; }
        public Sizes sizes { get; set; }
    }

    public class Sizes
    {
        public Size medium { get; set; }
        public Size large { get; set; }
        public Size thumb { get; set; }
        public Size small { get; set; }
    }

    public class Size
    {
        public int w { get; set; }
        public int h { get; set; }
        public string resize { get; set; }
    }

    public class Hashtag
    {
        public string text { get; set; }
        public List<int> indices { get; set; }
    }

    //public class TwitterUser
    //{
    //    public string id_str { get; set; }
    //    public string name { get; set; }
    //    public string screen_name { get; set; }
    //    public int followers_count { get; set; }
    //    public int friends_count { get; set; }
    //    public int listed_count { get; set; }
    //    public int favourites_count { get; set; }
    //    public int statuses_count { get; set; }
    //}

    public class Metadata
    {
        public string result_type { get; set; }
        public string iso_language_code { get; set; }
    }
}


