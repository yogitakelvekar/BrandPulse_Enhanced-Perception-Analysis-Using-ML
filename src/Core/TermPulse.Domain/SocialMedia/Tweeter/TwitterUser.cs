using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Domain.SocialMedia.Tweeter
{
    public class TwitterUser
    {
        public long id { get; set; }
        public string id_str { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public EntityContainer entities { get; set; }
        public bool @protected { get; set; }
        public int followers_count { get; set; }
        public int fast_followers_count { get; set; }
        public int normal_followers_count { get; set; }
        public int friends_count { get; set; }
        public int listed_count { get; set; }
        public string created_at { get; set; }
        public int favourites_count { get; set; }
        public object utc_offset { get; set; }
        public object time_zone { get; set; }
        public bool geo_enabled { get; set; }
        public bool verified { get; set; }
        public int statuses_count { get; set; }
        public int media_count { get; set; }
        public object lang { get; set; }
        public Status status { get; set; }
        public bool contributors_enabled { get; set; }
        public bool is_translator { get; set; }
        public bool is_translation_enabled { get; set; }
        public string profile_background_color { get; set; }
        public string profile_background_image_url { get; set; }
        public string profile_background_image_url_https { get; set; }
        public bool profile_background_tile { get; set; }
        public string profile_image_url { get; set; }
        public string profile_image_url_https { get; set; }
        public string profile_banner_url { get; set; }
        public string profile_link_color { get; set; }
        public string profile_sidebar_border_color { get; set; }
        public string profile_sidebar_fill_color { get; set; }
        public string profile_text_color { get; set; }
        public bool profile_use_background_image { get; set; }
        public bool has_extended_profile { get; set; }
        public bool default_profile { get; set; }
        public bool default_profile_image { get; set; }
        public List<long> pinned_tweet_ids { get; set; }
        public List<string> pinned_tweet_ids_str { get; set; }
        public bool has_custom_timelines { get; set; }
        public bool can_media_tag { get; set; }
        public bool followed_by { get; set; }
        public bool following { get; set; }
        public bool follow_request_sent { get; set; }
        public bool notifications { get; set; }
        public string advertiser_account_type { get; set; }
        public List<string> advertiser_account_service_levels { get; set; }
        public string business_profile_state { get; set; }
        public string translator_type { get; set; }
        public List<object> withheld_in_countries { get; set; }
        public bool require_some_consent { get; set; }
    }
    public class Url
    {
        public string url { get; set; }
        public string expanded_url { get; set; }
        public string display_url { get; set; }
        public List<int> indices { get; set; }
    }

    public class UserEntities
    {
        public List<Url> urls { get; set; }
    }

    public class EntityContainer
    {
        public UserEntities url { get; set; }
        public UserEntities description { get; set; }
    }

    public class UserMention
    {
        public string screen_name { get; set; }
        public string name { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public List<int> indices { get; set; }
    }

    public class StatusEntities
    {
        public List<object> hashtags { get; set; }
        public List<object> symbols { get; set; }
        public List<UserMention> user_mentions { get; set; }
        public List<object> urls { get; set; }
    }

    public class Status
    {
        public string created_at { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }
        public bool truncated { get; set; }
        public StatusEntities entities { get; set; }
        public string source { get; set; }
        public long? in_reply_to_status_id { get; set; }
        public string in_reply_to_status_id_str { get; set; }
        public long in_reply_to_user_id { get; set; }
        public string in_reply_to_user_id_str { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public object geo { get; set; }
        public object coordinates { get; set; }
        public object place { get; set; }
        public object contributors { get; set; }
        public bool is_quote_status { get; set; }
        public int retweet_count { get; set; }
        public int favorite_count { get; set; }
        public bool favorited { get; set; }
        public bool retweeted { get; set; }
        public string lang { get; set; }
        public object supplemental_language { get; set; }
    }
}
