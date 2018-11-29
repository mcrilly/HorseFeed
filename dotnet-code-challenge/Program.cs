using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using dotnet_code_challenge.Services;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_code_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection()
                .AddLogging();

            //configure services
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(serviceCollection);

            containerBuilder.RegisterType<FeedDataProcessingFactory>().As<IFeedDataProcessingFactory>();
            containerBuilder.RegisterType<HorseFeedService>().As<IHorseFeedService>();

            var container = containerBuilder.Build();

            var horseService = container.Resolve<IHorseFeedService>(); 
            horseService.ReadDataFilesAndOutputDetails();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
