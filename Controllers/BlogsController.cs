using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml.Linq;

namespace CommunityWeb.Controllers
{
    public class BlogsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var microsoftDotNetArticles = new List<MsdnBlogFeedItem>();
            var microsoftDotNetArticlesFeedUrl = "https://blogs.msdn.microsoft.com/dotnet/feed";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(microsoftDotNetArticlesFeedUrl);
                var responseMessage = await client.GetAsync(microsoftDotNetArticlesFeedUrl);
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
            
            return View(microsoftDotNetArticles);
        }

        private DateTime ParseDate(string date)
        {
            DateTime result;

            return (DateTime.TryParse(date, out result)) ? result : DateTime.MinValue;
        }
    }
}
