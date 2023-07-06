using Reddit.Controllers;

namespace BrandPulse.SocialMediaData.API.Models.Response
{
    public class PostData
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Url { get; set; }
        public List<CommentData> Comments { get; set; }
    }

    public class CommentData
    {
        public string Author { get; set; }
        public string Body { get; set; }
    }
}