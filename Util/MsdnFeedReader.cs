using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommunityWeb.Util
{
    public static class MsdnFeedReader
    {
        public static async Task<IEnumerable<MsdnBlogFeedItem>> ParseFeedAsync(string feedUrl)
        {
            var microsoftDotNetArticles = new List<MsdnBlogFeedItem>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(feedUrl);
                var responseMessage = await client.GetAsync(feedUrl);
                var responseString = await responseMessage.Content.ReadAsStringAsync();

                XDocument doc = XDocument.Parse(responseString);
                var feedItems = from item in doc.Root.Descendants()
                                    .First(i => i.Name.LocalName == "channel").Elements()
                                    .Where(i => i.Name.LocalName == "item")
                                select new MsdnBlogFeedItem
                                {
                                    Description = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                    Categories = (from category in item.Elements().Where(x => x.Name.LocalName == "category") select category.Value).ToList(),
                                    Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                    PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                                    Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                                };
                microsoftDotNetArticles = feedItems.ToList();
            }

            return microsoftDotNetArticles;
        }

        private static DateTime ParseDate(string date)
        {
            DateTime result;

            return (DateTime.TryParse(date, out result)) ? result : DateTime.MinValue;
        }
    }
}