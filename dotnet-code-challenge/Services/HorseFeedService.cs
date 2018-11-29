using System.Collections.Generic;
using System.IO;
using System.Linq;
using dotnet_code_challenge.Models;

namespace dotnet_code_challenge.Services
{
    public class HorseFeedService : IHorseFeedService
    {
        private readonly IFeedDataProcessingFactory _feedDataProcessingFactory;

        public HorseFeedService(IFeedDataProcessingFactory feedFeedDataProcessingFactory)
        {
            _feedDataProcessingFactory = feedFeedDataProcessingFactory;
        }
        /// <summary>
        /// Read the data from the input files and output details to the console window
        /// </summary>
        public void ReadDataFilesAndOutputDetails()
        {
            //we will read in the files from the FeedData folder and then locate the horse information
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
                horses.AddRange(horseData);
            }
        }

    }
}
