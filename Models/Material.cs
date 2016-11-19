using System;
using System.Collections.Generic;

namespace CommunityWeb.Models
{
    public class Material
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public DateTime UploadedAt { get; set; }

        public string ThumbnailImageUrl { get; set; }

        public IEnumerable<MaterialAuthor> Authors { get; set; }

        public IEnumerable<MaterialLink> Links { get; set; }
    }
    
}