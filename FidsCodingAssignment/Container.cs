using FidsCodingAssignment.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text.Json;

namespace FidsCodingAssignment
{
    // Fixed json file data
    public class Container
    {
        public static void Register(IConfiguration config, AppSettings settings, IServiceCollection services)
        {
            ConfigureData(settings, services);
            ConfigureSettings(settings, services);
        }

        private static void ConfigureData(AppSettings settings, IServiceCollection services)
        {
            var binPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;

            var fullPath = @$"{binPath}{settings.DataLocation}";

            if (!File.Exists(fullPath))
                throw new Exception("File data does not exist.");

            var data = File.ReadAllText(fullPath);

            JsonDocument.Parse(data);

            services.AddSingleton(data);
        }

        private static void ConfigureSettings(AppSettings settings, IServiceCollection services)
        {
            services.AddSingleton(new FlightSetting()
            {
                BoardingWindowInMinutes = settings.BoardingWindowInMinutes.Value,
                FlightAtGateInMinutes = settings.FlightAtGateInMinutes.Value
            });
        }
    }
}
