using System;
using System.IO;
using Autofac;
using Microsoft.Extensions.Logging;

namespace dotnet_code_challenge.Services
{
    public class FeedDataProcessingFactory : IFeedDataProcessingFactory
    {
        private readonly ILogger<FeedDataProcessingFactory> _logger;

        public FeedDataProcessingFactory(ILogger<FeedDataProcessingFactory> logger)
        {
            _logger = logger;
        }

        public IFeedDataProcessor ProcessDataFile(string fileExtension)
        {
            try
            {
                _logger.LogInformation("Determine the file extension to process");

                //depending upon the file extension, we will process the file as Json or Xml.
                return Program.Container.ResolveNamed<IFeedDataProcessor>(fileExtension);
            }
            catch (Exception ex)
            {
                throw new FileLoadException($"File: {fileExtension} is not supported");
            }
        }
    }
}