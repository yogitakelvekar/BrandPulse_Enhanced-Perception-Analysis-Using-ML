using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Domain.Entities
{
    public class PostDetail
    {
        public Guid Id { get; set; }
        public string SearchTermId { get; set; } = string.Empty;
        public int PlatformId { get; set; }
        public string PostId { get; set; } = string.Empty;
        public string PostTitle { get; set; } = string.Empty;
        public string PostDescription { get; set; } = string.Empty;
        public string PostUrl { get; set; } = string.Empty;
        public string PostThumbnail { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public string PostAuthor { get; set; } = string.Empty;
        public string PostAuthorAvatar { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
