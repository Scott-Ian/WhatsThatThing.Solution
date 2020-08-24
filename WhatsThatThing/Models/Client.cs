using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace WhatsThatThing.Models
{
  public class Client
  {

    public static string subscriptionKey = EnvironmentVariables.ComputerVisionKey;
    public static string endpoint = EnvironmentVariables.ComputerVisionEndpoint;

    public static ComputerVisionClient Authenticate(string endpoint, string key)
    {
      ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
        { Endpoint = endpoint };
      return client;
    }
  }
}