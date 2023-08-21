using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Models.ETL.Transform
{
    public class PostDetailTransformResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();
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
        public string PostAuthorProfile { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
