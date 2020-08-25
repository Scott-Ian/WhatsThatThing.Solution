using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WhatsThatThing.Models;
using WhatsThatThing;

namespace WhatsThatThing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        public IActionResult DefinitelyNotIndex(string imageUrl)
        {
            return RedirectToAction("Details", imageUrl);
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Details(string imageUrl) //string language
        {
            ViewBag.ImageUrl = imageUrl;
            ComputerVisionClient client = Client.Authenticate();
            List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
            {
                VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                VisualFeatureTypes.Objects
            };
            //
            Path.GetFileName(imageUrl);
            ImageAnalysis results = await client.AnalyzeImageAsync(imageUrl, features);
            return View(results);
        }
    }
}
