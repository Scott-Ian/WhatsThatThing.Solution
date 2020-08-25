using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using ComputerVisionQS;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace WhatsThatThing.Models
{
  public class Client
  {

    public static string subscriptionKey = EnvironmentVariables.ComputerVisionKey;
    public static string endpoint = EnvironmentVariables.ComputerVisionEndpoint;
    public static ComputerVisionClient Authenticate()
    {
      ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey))
        { Endpoint = endpoint };
      return client;
    }
    
    public static byte[] GetImageAsByteArray(string imageFilePath)
    {
      // open a read-only file stream for the specified file
      using (FileStream fileStream =
        new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
        {
          // read the file's contents into a byte array
          BinaryReader binaryReader = new BinaryReader(fileStream);
          return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
  }
}