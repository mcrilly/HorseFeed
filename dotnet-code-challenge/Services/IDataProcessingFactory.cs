namespace dotnet_code_challenge.Services
{
    public interface IDataProcessingFactory
    {
        IDataProcessor ProcessDataFile(string fileExtension);
    }
}
