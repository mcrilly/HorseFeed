namespace dotnet_code_challenge.Services
{
    public interface IFeedDataProcessingFactory
    {
        IFeedDataProcessor ProcessDataFile(string fileExtension);
    }
}
