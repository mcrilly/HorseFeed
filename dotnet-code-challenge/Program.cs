using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using dotnet_code_challenge.Services;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_code_challenge
{
    class Program
    {
        public static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection()
                .AddLogging();

            //configure services
            Container = ConfigureServices(serviceCollection);

            //process all the files in the FeedData folder and return the names
            // of horse sorted by price in ascending order
            var horseService = Container.Resolve<IHorseFeedService>();
            horseService.ReadDataFilesAndOutputDetails();

            Console.WriteLine();
            Console.WriteLine("Press <Enter> to continue ...");
            Console.ReadLine();
        }


        public static IContainer ConfigureServices(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(services);

            containerBuilder.RegisterType<FeedDataProcessingFactory>().As<IFeedDataProcessingFactory>();
            containerBuilder.RegisterType<HorseFeedService>().As<IHorseFeedService>();

            return containerBuilder.Build();
        }

    }
}
