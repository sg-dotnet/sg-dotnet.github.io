using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using CommunityWeb.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace CommunityWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppSettings _appSettings;

        public HomeController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync("https://api.meetup.com/NET-Developers-SG/events?key=" + _appSettings.MeetupWebApiKey + "&scroll=recent_past");
                    response.EnsureSuccessStatusCode();

                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var meetupEvents = JsonConvert.DeserializeObject<IEnumerable<MeetupEvent>>(stringResponse);

                    foreach(var meetupEvent in meetupEvents)
                    {
                        long unixDate = meetupEvent.HappensAt;
                        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        meetupEvent.HappensAtDateTime = start.AddMilliseconds(unixDate).ToLocalTime();
                    }

                    ViewBag.MeetupEvents = meetupEvents;
                }
                catch (HttpRequestException ex)
                {
                    
                }
            }

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
    }
}
