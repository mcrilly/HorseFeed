namespace dotnet_code_challenge.Services
{
    public class DataProcessingFactory : IDataProcessingFactory
    {
        public IDataProcessor ProcessDataFile(string fileExtension)
        {
            //depending upon the file extension, we will process the file as Json or Xml.
            return new XmlDataProcessor();
        }
    }
}