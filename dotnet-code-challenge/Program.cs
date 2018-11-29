using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using dotnet_code_challenge.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace dotnet_code_challenge
{
    class Program
    {
        public static ILoggerFactory LoggerFactory { get; set; }
        public static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection()
                .AddLogging();

            //configure services
            Container = ConfigureServices(serviceCollection);

            var serviceProvider = new AutofacServiceProvider(Container);

            LoggerFactory = serviceProvider.GetService<ILoggerFactory>();
            LoggerFactory.AddConsole(LogLevel.Error);

            var logger = LoggerFactory.CreateLogger<Program>();

            logger.LogDebug("Starting application");


            //process all the files in the FeedData folder and return the names
            // of horses sorted by price in ascending order
            var horseService = Container.Resolve<IHorseFeedService>();
            horseService.ReadDataFilesAndOutputDetails();

            Console.WriteLine();
            Console.WriteLine("Press <Enter> to continue ...");
            Console.ReadLine();

            logger.LogDebug("Application completed");
        }


        /// <summary>
        /// Configure the services for the application
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IContainer ConfigureServices(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(services);

            containerBuilder.RegisterType<JsonFeedDataProcessor>().Named<IFeedDataProcessor>(".json");
            containerBuilder.RegisterType<XmlFeedDataProcessor>().Named<IFeedDataProcessor>(".xml");
            containerBuilder.RegisterType<FeedDataProcessingFactory>().As<IFeedDataProcessingFactory>();
            containerBuilder.RegisterType<HorseFeedService>().As<IHorseFeedService>();

            return containerBuilder.Build();
        }

    }
}
