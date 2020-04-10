using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCore.Models;

namespace WebCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Knit0()
        {
            ViewData["Message"] = "Knit0 action.";

            return View();
        }

        public IActionResult Accessories()
        {
            ViewData["Message"] = "Toys action.";

            return View();
        }

        public IActionResult AccessoriesBag()
        {
            ViewData["Message"] = "Toys action.";

            return View();
        }

        public IActionResult AccessoriesGeneric()
        {
            ViewData["Message"] = "Toys action.";
            var imageFiles = new List<FileData>();
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images20191123", "Accessories", "bag");
            foreach (var file in Directory.GetFiles(dir))
            {
                var i = file.IndexOf("images20191123");
                var relativeFileName = ("/" + file.Substring(i)).Replace("\\", "/");

                imageFiles.Add(new FileData { ImageFile = relativeFileName, });
            }
            return View(imageFiles);
        }

        public IActionResult AccessoriesDog()
        {
            ViewData["Message"] = "fix me 111.";

            return View();
        }
        
        public IActionResult AccessoriesGlove ()
        {
            ViewData["Message"] = "fix me 112.";

            return View();
        }

        public IActionResult AccessoriesSock()
        {
            ViewData["Message"] = "fix me 113.";

            return View();
        }

        public IActionResult Hats()
        {
            ViewData["Message"] = "fix me 114.";

            return View();
        }

        public IActionResult HatsChild()
        {
            ViewData["Message"] = "fix me 115.";

            return View();
        }

        public IActionResult HatsLady()
        {
            ViewData["Message"] = "fix me 116.";

            return View();
        }

        public IActionResult HatsMen()
        {
            ViewData["Message"] = "fix me 117.";

            return View();
        }

        public IActionResult HomeImages()
        {
            ViewData["Message"] = "fix me 118.";

            return View();
        }

        public IActionResult HomeImagesBlanket()
        {
            ViewData["Message"] = "HomeImagesBlanket";

            return View();
        }

        public IActionResult HomeImagesDecor()
        {
            ViewData["Message"] = "HomeImagesDecor";

            return View();
        }

        public IActionResult HomeImagesPillow()
        {
            ViewData["Message"] = "HomeImagesPillow";

            return View();
        }

        public IActionResult ShawlsAndScarves()
        {
            ViewData["Message"] = "ShawlsAndScarves";

            return View();
        }

        public IActionResult Shawl()
        {
            ViewData["Message"] = "Shawl";

            return View();
        }

        public IActionResult Scarf()
        {
            ViewData["Message"] = "Scarf";

            return View();
        }

        public IActionResult Top()
        {
            ViewData["Message"] = "Top";

            return View();
        }

        public IActionResult TopAll()
        {
            ViewData["Message"] = "TopAll";

            return View();
        }

        public IActionResult Toys()
        {
            ViewData["Message"] = "Toys action.";

            return View();
        }

        public IActionResult ToysOctopuses()
        {
            ViewData["Message"] = "Toys - Octopuses";

            return View();
        }

        public IActionResult ToysOpossums()
        {
            ViewData["Message"] = "Toys - Opossums";

            return View();
        }

        public IActionResult ToysTurtles()
        {
            ViewData["Message"] = "Toys - Turtles";

            return View();
        }

        public IActionResult ToysFerret()
        {
            ViewData["Message"] = "Toys - Ferrets";

            return View();
        }

        public IActionResult Room()
        {
            ViewData["Message"] = "gfsdg gfdsg fddfsg gfgsfd.";

            return View();
        }

        public IActionResult Guns()
        {
            ViewData["Message"] = "gfsdg gfdsg fddfsg gfgsfd.";

            return View();
        }

        public IActionResult Mazen()
        {
            ViewData["Message"] = "gfsdg gfdsg fddfsg gfgsfd.";

            return View();
        }

        public IActionResult Food()
        {
            ViewData["Message"] = "gfsdg gfdsg fddfsg gfgsfd.";

            return View();
        }

        public IActionResult Seattle()
        {
            ViewData["Message"] = "gfsdg gfdsg fddfsg gfgsfd.";

            return View();
        }

        public IActionResult Yard()
        {
            ViewData["Message"] = "gfsdg gfdsg fddfsg gfgsfd.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
