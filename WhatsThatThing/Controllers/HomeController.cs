using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
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

        public async Task<IActionResult> WebDetails(string imageUrl) //string language
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
            Path.GetFileName(imageUrl);
            ImageAnalysis results = await client.AnalyzeImageAsync(imageUrl, features);
            return View(results);
        }

        public async Task<IActionResult> LocalDetails(string imageFilePath)
        {
            ViewBag.ImageFilePath = imageFilePath;
            // tried to document this quickly last minute... i failed...
            string uriBase = Client.endpoint + "vision/v3.0/analyze";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add(
                "Ocp-Apim-Subscription-Key", Client.subscriptionKey);
            string requestParamenters = "visualFeatures=Categories,Description,Color";
            string uri = uriBase + "?" + requestParamenters;
            HttpResponseMessage response;
            byte[] byteData = Client.GetImageAsByteArray(imageFilePath);
            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
            }
            string contentString = await response.Content.ReadAsStringAsync();
            string results = JToken.Parse(contentString).ToString();
            return View(results);
        }
    }
}
