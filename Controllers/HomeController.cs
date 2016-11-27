using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommunityWeb.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using CommunityWeb.Util;

namespace CommunityWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly string _pathToJsonDataFile;

        public HomeController(IOptions<AppSettings> appSettings, IHostingEnvironment env)
        {
            _appSettings = appSettings.Value;

            _pathToJsonDataFile = env.ContentRootPath +
                Path.DirectorySeparatorChar.ToString() + "wwwroot" +
                Path.DirectorySeparatorChar.ToString() + "data" +
                Path.DirectorySeparatorChar.ToString();
        }

        public async Task<IActionResult> Index()
        {
            var meetupEvents = (await JsonParser<MeetupEvent>.RetrieveJsonDataFromUrlAsync(
                "https://api.meetup.com/NET-Developers-SG/events?key=" + _appSettings.MeetupWebApiKey + "&scroll=recent_past"));

            foreach (var meetupEvent in meetupEvents)
            {
                long unixDate = meetupEvent.HappensAt;
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                meetupEvent.HappensAtDateTime = start.AddMilliseconds(unixDate).ToLocalTime();
            }

            ViewBag.MeetupEvents = meetupEvents;

            var materials = (await JsonParser<Material>.RetrieveJsonDataFromUrlAsync(
                "https://sg-dotnet.firebaseio.com/materials.json"));

            ViewBag.Materials = materials.OrderByDescending(m => m.UploadedAt);

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
