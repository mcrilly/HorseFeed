using System.IO;

namespace dotnet_code_challenge.Services
{
    public class FeedDataProcessingFactory : IFeedDataProcessingFactory
    {
        public IFeedDataProcessor ProcessDataFile(string fileExtension)
        {
            //depending upon the file extension, we will process the file as Json or Xml.
            if (fileExtension.Equals(".xml"))
            {
                return new XmlFeedDataProcessor();
            }

            if (fileExtension.Equals(".json"))
            {
                return new JsonFeedDataProcessor();
            }

            throw new FileLoadException($"File: {fileExtension} is not supported");
        }
    }
}