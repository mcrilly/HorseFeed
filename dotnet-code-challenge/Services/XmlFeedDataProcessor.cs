using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using dotnet_code_challenge.Models;

namespace dotnet_code_challenge.Services
{
    public class XmlFeedDataProcessor : IFeedDataProcessor
    {
        /// <summary>
        /// Read the contents of the xml file and return the horses found
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<Horse> ReadFile(string fileName)
        {
            var xmlHorses = new List<Horse>();

            var doc = new XmlDocument();
            doc.Load(fileName);

            if (doc.SelectNodes("//horses") != null)
            {
                //containing horses and prices
                var horses = doc.SelectNodes("//horses");

                if (horses.Count == 2)
                {
                    //this is the horse details
                    var horseSerializer = new XmlSerializer(typeof(XmlDataModel.Horses));
                    var horseNodes = (XmlDataModel.Horses)horseSerializer.Deserialize(new XmlNodeReader(horses.Item(0)));

                    //item 2 contains the horse prices
                    var priceSerializer = new XmlSerializer(typeof(XmlDataModel.HorsePrices));
                    var horsePrices = (XmlDataModel.HorsePrices)priceSerializer.Deserialize(new XmlNodeReader(horses.Item(1)));

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
