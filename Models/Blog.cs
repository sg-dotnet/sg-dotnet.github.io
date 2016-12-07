using System;

namespace CommunityWeb.Models
{
    public class Blog
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string[] Categories { get; set; }
    }
    
}