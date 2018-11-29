using System.Collections.Generic;
using System.IO;
using System.Linq;
using dotnet_code_challenge.Models;
using Newtonsoft.Json;

namespace dotnet_code_challenge.Services
{
    public class JsonFeedDataProcessor : IFeedDataProcessor
    {
        /// <summary>
        /// Read in the content of the json file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<Horse> ReadFile(string fileName)
        {
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

            return jsonHorses;
        }
    }
}
