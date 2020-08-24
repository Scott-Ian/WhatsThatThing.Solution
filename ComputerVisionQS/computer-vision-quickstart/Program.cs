using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
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
        static string subscriptionKey = EnvironmentVariables.ComputerVisionKey;
        static string endpoint = EnvironmentVariables.ComputerVisionEndpoint;

        //URL of Image to analyze
        private const string ANALYZE_URL_IMAGE = "https://www.thesprucepets.com/thmb/3-ISVJpCrp9TUfeRdH1mfzJlHGg=/960x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/golden-retriever-puppy-in-grass-923135452-5c887d4146e0fb00013365ba.jpg";
        private const string READ_TEXT_URL_IMAGE = "https://media.npr.org/assets/img/2016/04/17/handwritten-note_wide-941ca37f3638dca912c8b9efda05ee9fefbf3147.jpg?s=1400";
        static void Main(string[] args)
        {
            // Instantiate client
            ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);
            AnalyzeImageUrl(client, ANALYZE_URL_IMAGE).Wait();
            // Extract text from a URL image using the Read API
            ReadFileUrl(client, READ_TEXT_URL_IMAGE).Wait();
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
            
            Console.WriteLine($"Analyzing the image {Path.GetFileName(imageUrl)}");
            Console.WriteLine();
            // Analyze inputted image URL
            ImageAnalysis results = await client.AnalyzeImageAsync(imageUrl, features);

            // Summarizes the image content
            Console.WriteLine("Summary:");
            foreach (var caption in results.Description.Captions)
            {
                Console.WriteLine($"{caption.Text} with confidence {caption.Confidence}");
            }
            Console.WriteLine();

            // Display categories the image is divided into
            Console.WriteLine("Categories:");
            foreach (var category in results.Categories)
            {
                Console.WriteLine($"{category.Name} with confidence {category.Score}");
            }
            Console.WriteLine();

            // Image tags and their confidence score
            Console.WriteLine("Tags: ");
            foreach ( var tag in results.Tags)
            {
                Console.WriteLine($"{tag.Name} {tag.Confidence}");
            }
            Console.WriteLine();

            //Objects
            Console.WriteLine("Objects: ");
            foreach (var obj in results.Objects)
            {
                Console.WriteLine($"{obj.ObjectProperty} with confidence {obj.Confidence} at location {obj.Rectangle.X}, " + $"{obj.Rectangle.X + obj.Rectangle.W}, {obj.Rectangle.Y}, {obj.Rectangle.Y + obj.Rectangle.H}");
            }
            Console.WriteLine();

            // Well-known brands
            Console.WriteLine("Brands: ");
            foreach (var brand in results.Brands)
            {
                Console.WriteLine($"Logo of {brand.Name} with confidence {brand.Confidence} at location {brand.Rectangle.X}, " + $"{brand.Rectangle.X + brand.Rectangle.W}, {brand.Rectangle.Y}, {brand.Rectangle.Y + brand.Rectangle.H}");
            }
            Console.WriteLine();

            // Returns detected faces and their attributes
            Console.WriteLine("Faces:");
            foreach (var face in results.Faces)
            {
                Console.WriteLine($"A {face.Gender} of age {face.Age} at location {face.FaceRectange.Left}, "+ $"{face.FaceRectangel.Left}, {face.FaceRectange.Top + face.FaceRectangle.Width}, " + $"{face.FaceRectangle.Top + face.FaceRectangle.Height}");
            }
            Console.WriteLine();

            // Adult, racy, or gory content, if any:
            Console.WriteLine("Adult:");
            Console.WriteLine($"Has adult content: {results.Adult.IsAdultContent} with confidence { results.Adult.AdultScore}");
            Console.WriteLine($"Has racy content: {results.Adult.IsRacyContent} with confidence {results.Adult.RacyScore}");
            Console.WriteLine();

            // Identifies the color scheme
            Console.WriteLine("Color Scheme:");
            Console.WriteLine("Is black and white?: " + results.Color.IsBWImg);
            Console.WriteLine("Accent color: " + results.Color.DominantColorBackground);
            Console.WriteLine("Dominant foreground color: " + results.Color.DominantColorForeground);
            Console.WriteLine("Dominant colors: " + string.Join(",", results.Color.DominantColors));
            Console.WriteLine();

            // Detects the image types
            Console.WriteLine("Image Type:");
            Console.WriteLine("Clip Art Type: " + results.ImageType.ClipArtType);
            Console.WriteLine("Line Drawing Type: " + results.ImageType.LineDrawingType);
            Console.WriteLine();

            // Identifies celeberties in image, if any
            Console.WriteLine("Celeberties: ");
            foreach (var catergory in results.Categories)
            {
                if (catergory.Detail?.Celeberties != null)
                {
                    foreach (var celeb in catergory.Detail.Celeberties)
                    {
                        Console.WriteLine($"{celeb.Name} with confidence {celeb.Confidence} at location {celeb.FaceRectangle.Left}, " + $"{celeb.FaceRectangle.Top}, {celeb.FaceRectangle.Height}, {celeb.FaceRectangle.Width}");
                    }
                }
            }
            Console.WriteLine();

            // Popular landmarks in image, if any
            Console.WriteLine("Landmarks:");
            foreach (var category in results.Categories)
            {
                if (category.Detail?.Landmarks != null)
                {
                    foreach (var landmark in category.Detail.Landmarks)
                    {
                        Console.WriteLine($"{landmark.Name} with confidence {landmark.Confidence}");
                    }
                }
            }
            Console.WriteLine();

        }

        /*
        * READ FILE - URL
        * Extracts text
        */
        public static async Task ReadFileUrl(ComputerVisionClient client, string urlFile)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("READ FILE FROM URL");
            Console.WriteLine();

            // Read text from URL
            var textHeaders = await client.ReadAsync(urlFile, language: "en");
            // After request, get operation location
            string operationLocation = textHeaders.OperationLocation;
            Thread.Sleep(2000);

            // Retrieve the URI where the extracted text will be stored from the Operation-Location Header
            // We only need the ID and not the full URL
            const int numberOfCharsInOperationId = 36;
            string operationId = operationLocation.SubString(operationLocation.Length - numberOfCharsInOperationId);

            // Extract text
            ReadOperationResults results;
            Console.WriteLine($"Extracting text from URL file {Path.GetFileName(urlFile)}...");
            Console.WriteLine();
            do
            {
                results = await client.GetReadResultAsync(Guid.Parse(operationId));
            }
            while ((results.Status == OperationStatusCodes.Running || results.Status == OperationStatusCodes.NotStarted));

            // Display the found text
            Console.WriteLine();
            var textUrlFileResults = results.AnalyzeResult.ReadResults;
            foreach (ReadResult page in textUrlFileResults)
            {
                foreach (Line line in page.Lines)
                {
                    Console.WriteLine(line.Text);
                }
            }
            Console.WriteLine();
        }

    }
}
