using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml.Linq;
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
                articles = articles.Concat(await MsdnFeedReader.ParseFeedAsync(source.FeedsUrl));
            }

            ViewBag.BlogCategories = (await JsonParser<Blog>.RetrieveJsonDataFromUrlAsync(
                "https://sg-dotnet.firebaseio.com/blogs.json"));
            
            return View(articles.ToList());
        }
    }
}
