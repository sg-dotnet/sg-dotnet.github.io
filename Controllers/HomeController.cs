using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using CommunityWeb.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using System.IO;

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
            ViewBag.MeetupEvents = await RetrieveMeetupEventsAsync();

            ViewBag.Materials = await RetrieveMaterialsAsync();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private async Task<IEnumerable<MeetupEvent>> RetrieveMeetupEventsAsync()
        {
            var meetupEvents = Enumerable.Empty<MeetupEvent>();

            using (var client = new HttpClient())
            {
                try
                {
                    var meetupResponse = await client.GetAsync("https://api.meetup.com/NET-Developers-SG/events?key=" + _appSettings.MeetupWebApiKey + "&scroll=recent_past");
                    meetupResponse.EnsureSuccessStatusCode();

                    string stringResponse = await meetupResponse.Content.ReadAsStringAsync();
                    meetupEvents = JsonConvert.DeserializeObject<IEnumerable<MeetupEvent>>(stringResponse);

                    foreach (var meetupEvent in meetupEvents)
                    {
                        long unixDate = meetupEvent.HappensAt;
                        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        meetupEvent.HappensAtDateTime = start.AddMilliseconds(unixDate).ToLocalTime();
                    }
                }
                catch (HttpRequestException ex)
                {

                }
            }

            return meetupEvents;
        }

        private async Task<IEnumerable<Material>> RetrieveMaterialsAsync()
        {
            var materials = Enumerable.Empty<Material>();

            string stringResponse;

            try
            {
                using (StreamReader reader = System.IO.File.OpenText(_pathToJsonDataFile + "materials.json"))
                {
                    stringResponse = await reader.ReadToEndAsync();
                }

                materials = JsonConvert.DeserializeObject<IEnumerable<Material>>(stringResponse);
            }
            catch (HttpRequestException ex)
            {

            }

            return materials.OrderByDescending(m => m.UploadedAt);
        }
    }
}
