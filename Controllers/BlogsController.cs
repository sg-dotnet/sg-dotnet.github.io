using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommunityWeb.Util;
using CommunityWeb.Models;

namespace CommunityWeb.Controllers
{
    public class BlogsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var articles = Enumerable.Empty<MsdnBlogFeedItem>();

            var blogFeedsSources = (await JsonParser<BlogFeedsSource>.RetrieveJsonDataFromUrlAsync(
                "https://sg-dotnet.firebaseio.com/blogfeedssources.json"));
            
            foreach(var source in blogFeedsSources)
            {
                var articlesInFeeds = await MsdnFeedReader.ParseFeedAsync(source.FeedsUrl);

                if (source.DefaultCategories != null)
                {
                    foreach(var article in articlesInFeeds)
                    {
                        foreach(string defaultCategory in source.DefaultCategories)
                        {
                            if (!article.Categories.Contains(defaultCategory))
                            {
                                article.Categories.Add(defaultCategory);
                            }
                        }
                    }
                }

                articles = articles.Concat(articlesInFeeds);
            }

            ViewBag.BlogCategories = (await JsonParser<Blog>.RetrieveJsonDataFromUrlAsync(
                "https://sg-dotnet.firebaseio.com/blogs.json"));
            
            return View(articles.ToList());
        }
    }
}
