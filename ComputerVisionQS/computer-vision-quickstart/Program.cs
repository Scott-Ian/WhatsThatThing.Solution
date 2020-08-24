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
        static string subscriptionKey = ComputerVisionKey;
        static string endpoint = ComputerVisionEndpoint;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
