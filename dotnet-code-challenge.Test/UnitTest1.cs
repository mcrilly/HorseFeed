using System.IO;
using System.Xml;
using dotnet_code_challenge.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace dotnet_code_challenge.Test
{
    public class UnitTest1
    {
        private readonly Mock<ILoggerFactory> _loggerFactory;
        private readonly IHorseFeedService _horseService;

        private readonly IFeedDataProcessingFactory _dataProcessingFactory;

        public UnitTest1()
        {
            var mockDataProcessingFactory = new Mock<IFeedDataProcessingFactory>();
            _loggerFactory = new Mock<ILoggerFactory>();
            var loggerMock = new Mock<ILogger<HorseFeedService>>();

            _horseService = new HorseFeedService(loggerMock.Object, mockDataProcessingFactory.Object);
            var dataLogger = new Mock<ILogger<FeedDataProcessingFactory>>();
            _dataProcessingFactory = new FeedDataProcessingFactory(dataLogger.Object);
        }

        [Fact]
        public void Test1()
        {
            //unit tests ...
        }

        /// <summary>
        /// Test that a non-existent folder returns an empty collection of files
        /// </summary>
        [Fact]
        public void GetAllFilesInFolderTest()
        {
            var allFilesInFolder = _horseService.GetAllFilesInFolder(@"folderdoesnotexist");
            Assert.Empty(allFilesInFolder);
        }

        [Fact]
        public void ParseXmlFileWithMissingHorseData()
        {
            var mockXmlLogger = _loggerFactory.Object.CreateLogger<XmlFeedDataProcessor>();
            var document = new XmlDocument();
            
            document.LoadXml(TestData.MissingHorseNameXml);
            var xmlFeedDataProcessor = new XmlFeedDataProcessor(mockXmlLogger);
            var parseFileResponse = xmlFeedDataProcessor.ParseXmlFile(document);
            Assert.Equal(1, parseFileResponse.Count);
            Assert.Equal("Coronel", parseFileResponse[0].Name);
        }


        [Fact]
        public void ParseXmlFileWithMissingPriceData()
        {
            var mockXmlLogger = _loggerFactory.Object.CreateLogger<XmlFeedDataProcessor>();
            var document = new XmlDocument();

            document.LoadXml(TestData.MissingHorsePriceXml);
            var xmlFeedDataProcessor = new XmlFeedDataProcessor(mockXmlLogger);
            var parseFileResponse = xmlFeedDataProcessor.ParseXmlFile(document);
            Assert.Equal(1, parseFileResponse.Count);
            Assert.Equal("Fred", parseFileResponse[0].Name);
        }


        [Fact]
        public void ParseFileExtensionNotSupported()
        {
            const string fileExtension = ".unused";
            var exception = Assert.Throws<FileLoadException>(() => _dataProcessingFactory.ProcessDataFile(fileExtension));
            Assert.Equal(exception.Message, $"File: {fileExtension} is not supported");
        }

    }
}
