namespace TermPulse.Domain.SocialMedia.Tweeter
{
    public class TweetResponse
    {
        //public List<Tweet> statuses { get; set; }
        //public Metadata metadata { get; set; }

        public Data data { get; set; }
    }

    public class Data
    {
        public SearchByRawQuery search_by_raw_query { get; set; }
    }

    public class SearchByRawQuery
    {
        public SearchTimeline search_timeline { get; set; }
    }

    public class SearchTimeline
    {
        public Timeline timeline { get; set; }
    }

    public class Timeline
    {
        public List<Instruction> instructions { get; set; }
    }

    public class Instruction
    {
        public List<Entry> entries { get; set; }
    }

    public class Entry
    {
        public Content content { get; set; }
    }

    public class Content
    {
        public ItemContent itemContent { get; set; }
    }

    public class ItemContent
    {
        public TweetResults tweet_results { get; set; }
    }

    public class TweetResults
    {
        public Result result { get; set; }
    }

    public class Result
    {
        public Tweet legacy { get; set; }
        public Core core { get; set; }
    }

    public class Core
    {
        public UserResults user_results { get; set; }
    }

    public class UserResults
    {
        public UserResult result { get; set; }
    }

    public class UserResult
    {
        public TwitterUser legacy { get; set; }  // This maps to the user's legacy object.
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

    public class Metadata
    {
        public string result_type { get; set; }
        public string iso_language_code { get; set; }
    }
}


