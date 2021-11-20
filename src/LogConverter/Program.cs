using LogConverter.Data.Interfaces;
using LogConverter.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;

namespace LogConverter
{
    public static class Program
    {
        private static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<WebClient>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ILogConverterService, LogConverterService>();
            services.AddScoped<ILogReaderService, LogReaderService>();
        }

        private static ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureService();

            return serviceCollection.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            var serviceProvider = GetServiceProvider();
            var _logConverterService = serviceProvider.GetService<ILogConverterService>();

            Console.WriteLine("======================");

            Console.WriteLine("\nPlease inform log url");
            Console.Write("> ");
            string logUrl = Console.ReadLine();

            Console.WriteLine("\nPlease inform new log output directory");
            Console.Write("> ");
            string logOutputDirectory = Console.ReadLine();

            Console.WriteLine("\n\nStarting log migration...");
            _logConverterService.ConvertLog(logUrl, logOutputDirectory);

            Console.ReadKey();
        }
    }
}
