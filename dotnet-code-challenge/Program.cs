using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using dotnet_code_challenge.Services;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_code_challenge
{
    class Program
    {
        //public static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection()
                .AddLogging();

            //configure services
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(serviceCollection);

            containerBuilder.RegisterType<HorseService>().As<IHorseService>();

            var container = containerBuilder.Build();

            var horseService = container.Resolve<IHorseService>(); 
            horseService.ReadDataFilesAndOutputDetails();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
