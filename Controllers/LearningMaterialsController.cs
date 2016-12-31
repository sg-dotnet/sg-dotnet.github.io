using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommunityWeb.Util;
using CommunityWeb.Models;

namespace CommunityWeb.Controllers
{
    public class LearningMaterialsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var materials = (await JsonParser<Material>.RetrieveJsonDataFromUrlAsync(
                "https://sg-dotnet.firebaseio.com/materials.json"));
            
            return View(materials.OrderByDescending(m => m.UploadedAt));
        }
    }
}
