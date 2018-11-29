using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using dotnet_code_challenge.Models;
using Microsoft.Extensions.Logging;

namespace dotnet_code_challenge.Services
{
    public class XmlFeedDataProcessor : IFeedDataProcessor
    {
        private readonly ILogger<XmlFeedDataProcessor> _logger;

        public XmlFeedDataProcessor(ILogger<XmlFeedDataProcessor> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Read the contents of the xml file and return the horses found
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<Horse> ReadFile(string fileName)
        {
            try
            {
                _logger.LogInformation("About to read xml file");

                var xmlHorses = new List<Horse>();

                var doc = new XmlDocument();
                doc.Load(fileName);

                //read in the loaded xml and return the horses
                xmlHorses.AddRange(ParseXmlFile(doc));

                _logger.LogInformation("Finished reading xml file");

                return xmlHorses;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There was an error reading the xml file: {exception.Message}");
            }

            return new List<Horse>();
        }


        /// <summary>
        /// Find the horses within the xml document and return them if all the data is valid
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public List<Horse> ParseXmlFile(XmlDocument doc)
        {
            var xmlHorses = new List<Horse>();
            if (doc.SelectNodes("//horses") != null)
            {
                //containing horses and prices
                var xmlNodeList = doc.SelectNodes("//horses");

                if (xmlNodeList.Count == 2)
                {
                    //this is the horse details
                    var horseSerializer = new XmlSerializer(typeof(XmlDataModel.Horses));
                    var horseNodes = (XmlDataModel.Horses)horseSerializer.Deserialize(new XmlNodeReader(xmlNodeList.Item(0)));

                    //item 2 contains the horse prices
                    var priceSerializer = new XmlSerializer(typeof(XmlDataModel.HorsePrices));
                    var horsePrices = (XmlDataModel.HorsePrices)priceSerializer.Deserialize(new XmlNodeReader(xmlNodeList.Item(1)));

                    foreach (var raceHorse in horseNodes.Horse)
                    {
                        //if any of these conditions are true, then we will not process this horse
                        if (!(raceHorse.Number > 0) || string.IsNullOrEmpty(raceHorse.Name) || horsePrices.Horse.All(x => x.Number != raceHorse.Number))
                            continue;

                        var id = raceHorse.Number;
                        var price = horsePrices.Horse.Single(y => y.Number == id).Price;
                        var horse = new Horse
                        {
                            Name = raceHorse.Name,
                            Id = raceHorse.Number,
                            Price = price
                        };
                        xmlHorses.Add(horse);
                    }
                }
            }

            return xmlHorses;
        }
    }
}
