using System.IO;

namespace dotnet_code_challenge.Services
{
    public interface IHorseFeedService
    {
        void ReadDataFilesAndOutputDetails();

        FileInfo[] GetAllFilesInFolder(string folderPath);
    }
}
