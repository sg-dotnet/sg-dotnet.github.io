using System;

namespace CommunityWeb.Models
{
    public class BlogFeedsSource
    {
        public string Name { get; set; }

        public string FeedsUrl { get; set; }

        public string[] DefaultCategories { get; set; }
    }
    
}