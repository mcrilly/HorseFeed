using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using dotnet_code_challenge.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace dotnet_code_challenge.Services
{
    public class JsonFeedDataProcessor : IFeedDataProcessor
    {
        private readonly ILogger<JsonFeedDataProcessor> _logger;

        public JsonFeedDataProcessor(ILogger<JsonFeedDataProcessor> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Read in the content of the json file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<Horse> ReadFile(string fileName)
        {
            try
            {
                _logger.LogInformation("About to read json file");

                var jsonHorses = new List<Horse>();

                var fileContent = File.ReadAllText(fileName);
                if (!string.IsNullOrEmpty(fileContent))
                {
                    // parse the file and return the collection
                    var fileData = JsonConvert.DeserializeObject<JsonDataModel.RootObject>(fileContent);
                    foreach (var rawDataParticipant in fileData.RawData.Participants)
                    {
                        var id = rawDataParticipant.Id;
                        var price = fileData.RawData.Markets.SelectMany(x => x.Selections).Single(y => y.SelectionTags.ParticipantId == id).Price;
                        var horse = new Horse
                        {
                            Name = rawDataParticipant.Name,
                            Id = rawDataParticipant.Id,
                            Price = price
                        };
                        jsonHorses.Add(horse);
                    }
                }
                _logger.LogInformation("Finished reading json file");

                return jsonHorses;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"There was an error reading the json file: {e.Message}");
            }
            return new List<Horse>();
        }
    }
}
