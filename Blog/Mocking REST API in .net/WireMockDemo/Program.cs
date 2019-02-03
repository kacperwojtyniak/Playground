using System;
using WireMock.Net.StandAlone;
using WireMock.Settings;

namespace WireMockDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting WireMock.");

            var settings = new FluentMockServerSettings
            {
                ReadStaticMappings = true,
                WatchStaticMappings = true,
                StartAdminInterface = true,
            };

            StandAloneApp.Start(settings);

            //var server = StandAloneApp.Start(settings);
            //server.ReadStaticMappings("CustomMappingFolder\\GetMapping");
            //server.WatchStaticMappings("CustomMappingFolder\\GetMapping");

            Console.WriteLine("WireMock started");
            Console.ReadLine();
        }
    }
}
