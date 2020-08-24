using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComupterVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Linq;
using ComputerVisionQS;

// ComputerVisionKey
// ComputerVisionEndpoint

namespace computer_vision_quickstart
{
    class Program
    {
        // Environment Variable Injection
        static string subscriptionKey = ComputerVisionKey;
        static string endpoint = ComputerVisionEndpoint;

        //URL of Image to analyze
        private const string ANALYZE_URL_IMAGE = "https://www.thesprucepets.com/thmb/3-ISVJpCrp9TUfeRdH1mfzJlHGg=/960x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/golden-retriever-puppy-in-grass-923135452-5c887d4146e0fb00013365ba.jpg";
        static void Main(string[] args)
        {
            // Instantiate client
            ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);
        }
        
        // Authenticates the computer vision client
        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
                { Endpoint = endpoint };
            return client;
        }

        /* 
        * ANALYZE IMAGE - URL IMAGE
        * Extracts captions, categories, tags, objects, faces, racy/adult content,
        * brands, celebrities, landmarks, color scheme, and image types.
        */
        public static async Task AnalyzeImageUrl(ComputerVisionClient client, string imageUrl)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("ANALYZE IMAGE - URL");
            Console.WriteLine();

            //Creating a list that defines the features to be extracted from the image.
            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
            {
                VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                VisualFeatureTypes.Objects
            };

        }

    }
}
