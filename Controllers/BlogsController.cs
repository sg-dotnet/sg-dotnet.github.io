using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml.Linq;
using CommunityWeb.Util;

namespace CommunityWeb.Controllers
{
    public class BlogsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var articles = Enumerable.Empty<MsdnBlogFeedItem>();
            
            articles = articles.Concat(await MsdnFeedReader.ParseFeedAsync("https://blogs.msdn.microsoft.com/dotnet/feed"));

            articles = articles.Concat(await MsdnFeedReader.ParseFeedAsync("https://blogs.msdn.microsoft.com/martinkearn/feed"));

            articles = articles.Concat(await MsdnFeedReader.ParseFeedAsync("https://blogs.msdn.microsoft.com/orleans/feed"));
            
            return View(articles.ToList());
        }
    }
}
