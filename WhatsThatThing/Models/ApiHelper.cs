using RestSharp;
using System.Threading.Tasks;

namespace WhatsThatThing.Models
{
    class ApiHelper
    {
      public static async Task<string> ApiCall(string apiKey)
      {
        RestClient client = new RestClient("https://whatsthatthing.cognitiveservices.azure.com/");
        RestRequest request = new RestRequest($"");
      }
    }
}