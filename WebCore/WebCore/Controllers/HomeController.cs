using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            ViewData["Message"] = "Short information about myself:";

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

        public IActionResult ShowSubfolder(string folder, string subfolder)
        {
            if (folder == null) throw new ArgumentNullException("folder");
            if (subfolder == null) throw new ArgumentNullException("subfolder");
            ViewData["Message"] = "Toys action 1.";
            // make octopus --> Octopus
            var subfolder_ = subfolder.Substring(0, 1).ToUpper() + subfolder.Substring(1);
            ViewData["Title"] = $"Showing {subfolder_} collection of {folder} group"; // not used currently; replace by the next two lines
            ViewData["Folder"] = folder;
            ViewData["Subfolder"] = subfolder_;

            var imageFiles = new List<FileData>();
            // if folder = Accessories, and subfolder = bag, the result will be
            // H:\sergeimgithub\galina\WebCore\WebCore\wwwroot\images20191123\Accessories\bag
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images20191123", folder, subfolder);
            foreach (var file in Directory.GetFiles(dir))
            {
                // strip off the leading part not including images20191123\Accessories\bag
                // also change backslash to forward slash
                var i = file.IndexOf("images20191123");
                var relativeFileName = ("/" + file.Substring(i)).Replace("\\", "/");
                // /images20191123/Accessories/bag/bag1.JPG
                imageFiles.Add(new FileData { wwwRootFileName = relativeFileName, });
            }
            return View(imageFiles);
        }

        public IActionResult Hats()
        {
            ViewData["Message"] = "fix me 114.";

            return View();
        }

        public IActionResult Fox()
        {
            ViewData["Message"] = "Fox 115.";

            var serializedData = PictureNamesInFolder("images20210703", "Fox");
            return View(serializedData);
        }

        public IActionResult Pigs()
        {
            ViewData["Message"] = "Fox 115.";
            var serializedData = PictureNamesInFolder("images20210703", "Pigs");
            return View(serializedData);
        }

        public IActionResult HomeImages()
        {
            ViewData["Message"] = "fix me 118.";

            return View();
        }

        public IActionResult ShawlsAndScarves()
        {
            ViewData["Message"] = "ShawlsAndScarves";

            return View();
        }

        public IActionResult Top()
        {
            ViewData["Message"] = "Top";

            return View();
        }

        public IActionResult Toys()
        {
            ViewData["Message"] = "Toys action.";

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
            ViewData["Title"] = "Contact";
            ViewData["Message"] = "Contact information";

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

        /// <summary>
        /// Get list of picture names in given folder off the wwwroot.
        /// </summary>
        /// <param name="images">For example "images20210703"</param>
        /// <param name="folder">for example "Pigs"</param>
        /// <returns>Models.Serialized</returns>
        private Models.Serialized PictureNamesInFolder(string images, string folder)
        {
            var testData = new TestData();
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", images, folder);
            foreach (var file in Directory.GetFiles(dir))
            {
                var i = file.IndexOf("images");
                // the relative name will look like: /images20191123/Accessories/bag/bag1.JPG
                var relativeFileName = ("/" + file.Substring(i)).Replace("\\", "/");
                testData.Lines.Add(relativeFileName);
            }
            var ser = new Models.Serialized();
            var serializedData = Newtonsoft.Json.JsonConvert.SerializeObject(testData);
            ser.Data = serializedData;
            return ser;
        }
    }
}
