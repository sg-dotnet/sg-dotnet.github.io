using System.Collections.Generic;

namespace CommunityWeb.Constants
{
    public static class MaterialType
    {
        public static IDictionary<string, string> MaterialTypeDic;

        static MaterialType()
        {
            MaterialTypeDic = new Dictionary<string, string>()
                {
                    { "PDF Document", "fa-file-pdf-o" },
                    { "PPT Slides", "fa-file-powerpoint-o" },
                    { "Website", "fa-globe" },
                    { "Github Repo", "fa-github" },
                    { "SlideShare", "fa-slideshare" },
                    { "YouTube Video", "fa-youtube-play" }
                };
        }
    }
}