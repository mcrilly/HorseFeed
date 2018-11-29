using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using dotnet_code_challenge.Models;
using Microsoft.Extensions.Logging;

namespace dotnet_code_challenge.Services
{
    public class HorseFeedService : IHorseFeedService
    {
        private readonly ILogger<HorseFeedService> _logger;
        private readonly IFeedDataProcessingFactory _feedDataProcessingFactory;

        public HorseFeedService(ILogger<HorseFeedService> logger, IFeedDataProcessingFactory feedDataProcessingFactory)
        {
            _logger = logger;
            _feedDataProcessingFactory = feedDataProcessingFactory;
        }

        /// <summary>
        /// Read the data from the input files and output details to the console window
        /// </summary>
        public void ReadDataFilesAndOutputDetails()
        {
            //we will read in the files from the FeedData folder and then locate the horse information
            try
            {
                var folderPath = Path.Combine(Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location), @"FeedData");

                var horses = new List<Horse>();

                var di = new DirectoryInfo(folderPath);
                var files = di.GetFiles();
                foreach (var dataFile in files)
                {
                    //determine the type of file being handled
                    var fileProcessor = _feedDataProcessingFactory.ProcessDataFile(dataFile.Extension);

                    //read in the file data
                    var horseData = fileProcessor.ReadFile(dataFile.FullName);
                    if (horseData.Any())
                    {
                        //if we have some horses, then add them to the collection
                        horses.AddRange(horseData);
                    }
                }

                //output the horse names
                DisplayHorseNameInPriceOrder(horses);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }


        private void DisplayMessage(string message)
        {
            Console.WriteLine();
            Console.WriteLine($"*** {message} ***");
            _logger.LogInformation(message);
        }

        /// <summary>
        /// Display the horse names in price ascending order
        /// </summary>
        /// <param name="horseList"></param>
        private void DisplayHorseNameInPriceOrder(List<Horse> horseList)
        {
            if (!horseList.Any())
            {
                _logger.LogError("No horses found");
                return;
            }

            DisplayMessage("Horses in price order");

            foreach (var horse in horseList.OrderBy(x => x.Price))
            {
                Console.WriteLine($"{horse.Name}");
            }
        }
    }
}
