using System;
using FundaApiClient;
using FundaApiClient.Settings;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiClientSettings
                .Instance
                .ApiKey = "ac1b0b1572524640a0ecc54de453ea9f";

            var writer = new PropertyCountWriter(
                new FundaDocumentProvider(),
                properties => properties.GetTopAgents());

            Console.WriteLine("Top 10 agents in Amsterdam by property count");

            writer.SetSearchText("/amsterdam");
            writer.WriteToAsync(Console.OpenStandardOutput()).Wait();

            Console.WriteLine();

            Console.WriteLine("Top 10 agents in Amsterdam by count of properties with gardens");

            writer.SetSearchText("/amsterdam/tuin");
            writer.WriteToAsync(Console.OpenStandardOutput()).Wait();

            Console.WriteLine();
        }
    }
}
