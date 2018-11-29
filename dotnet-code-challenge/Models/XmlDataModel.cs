using System.Collections.Generic;
using System.Xml.Serialization;

namespace dotnet_code_challenge.Models
{
    public class XmlDataModel
    {
        [XmlRoot(ElementName = "track")]
        public class Track
        {
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
            [XmlAttribute(AttributeName = "TranslatedName")]
            public string TranslatedName { get; set; }
            [XmlAttribute(AttributeName = "club")]
            public string Club { get; set; }
            [XmlAttribute(AttributeName = "location")]
            public string Location { get; set; }
            [XmlAttribute(AttributeName = "country")]
            public string Country { get; set; }
            [XmlAttribute(AttributeName = "state")]
            public string State { get; set; }
            [XmlAttribute(AttributeName = "condition")]
            public string Condition { get; set; }
        }

        [XmlRoot(ElementName = "distance")]
        public class Distance
        {
            [XmlAttribute(AttributeName = "metres")]
            public string Metres { get; set; }
        }

        [XmlRoot(ElementName = "statistic")]
        public class Statistic
        {
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
            [XmlAttribute(AttributeName = "total")]
            public string Total { get; set; }
            [XmlAttribute(AttributeName = "firsts")]
            public string Firsts { get; set; }
            [XmlAttribute(AttributeName = "seconds")]
            public string Seconds { get; set; }
            [XmlAttribute(AttributeName = "thirds")]
            public string Thirds { get; set; }
        }

        [XmlRoot(ElementName = "statistics")]
        public class Statistics
        {
            [XmlElement(ElementName = "statistic")]
            public Statistic Statistic { get; set; }
        }

        [XmlRoot(ElementName = "trainer")]
        public class Trainer
        {
            [XmlElement(ElementName = "statistics")]
            public Statistics Statistics { get; set; }
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
        }

        [XmlRoot(ElementName = "jockey")]
        public class Jockey
        {
            [XmlElement(ElementName = "statistics")]
            public Statistics Statistics { get; set; }
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
        }

        [XmlRoot(ElementName = "weight")]
        public class Weight
        {
            [XmlAttribute(AttributeName = "allocated")]
            public string Allocated { get; set; }
            [XmlAttribute(AttributeName = "total")]
            public string Total { get; set; }
        }

        [XmlRoot(ElementName = "horse")]
        public class Horse
        {
            [XmlElement(ElementName = "number")]
            public int Number { get; set; }
            [XmlElement(ElementName = "trainer")]
            public Trainer Trainer { get; set; }
            [XmlElement(ElementName = "training_location")]
            public string Training_location { get; set; }
            [XmlElement(ElementName = "owners")]
            public string Owners { get; set; }
            [XmlElement(ElementName = "colours")]
            public string Colours { get; set; }
            [XmlElement(ElementName = "current_blinker_ind")]
            public string Current_blinker_ind { get; set; }
            [XmlElement(ElementName = "prizemoney_won")]
            public string Prizemoney_won { get; set; }
            [XmlElement(ElementName = "jockey")]
            public Jockey Jockey { get; set; }
            [XmlElement(ElementName = "barrier")]
            public string Barrier { get; set; }
            [XmlElement(ElementName = "weight")]
            public Weight Weight { get; set; }
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
            [XmlAttribute(AttributeName = "country")]
            public string Country { get; set; }
            [XmlAttribute(AttributeName = "age")]
            public string Age { get; set; }
            [XmlAttribute(AttributeName = "sex")]
            public string Sex { get; set; }
            [XmlAttribute(AttributeName = "colour")]
            public string Colour { get; set; }
            [XmlAttribute(AttributeName = "foaling_date")]
            public string Foaling_date { get; set; }
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
            [XmlElement(ElementName = "last_four_starts")]
            public string Last_four_starts { get; set; }
            [XmlElement(ElementName = "last_ten_starts")]
            public string Last_ten_starts { get; set; }
        }

        [XmlRoot(ElementName = "horses")]
        public class Horses
        {
            [XmlElement(ElementName = "horse")]
            public List<Horse> Horse { get; set; }
        }


        [XmlRoot(ElementName = "horse")]
        public class HorsePrice
        {
            [XmlAttribute(AttributeName = "number")]
            public int Number { get; set; }
            [XmlAttribute(AttributeName = "Price")]
            public decimal Price { get; set; }
        }

        [XmlRoot(ElementName = "horses")]
        public class HorsePrices
        {
            [XmlElement(ElementName = "horse")]
            public List<HorsePrice> Horse { get; set; }
        }

    }
}
