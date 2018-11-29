using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace dotnet_code_challenge.Models
{
    public class JsonDataModel
    {
        public class Tags
        {
            public string CourseType { get; set; }
            public string Distance { get; set; }
            public string Going { get; set; }
            public string Runners { get; set; }
            public string MeetingCode { get; set; }
            public string TrackCode { get; set; }
            public string Sport { get; set; }
        }


        public class SelectionTags
        {
            [JsonProperty("participant")]
            public int ParticipantId { get; set; }
            public string Name { get; set; }
        }

        public class Selection
        {
            public string Id { get; set; }
            public decimal Price { get; set; }
            [JsonProperty("Tags")]
            public SelectionTags SelectionTags { get; set; }
        }

        public class MarketTags
        {
            public string Places { get; set; }
            [JsonProperty("type")]
            public string MarketType { get; set; }
        }

        public class Market
        {
            public string Id { get; set; }
            public List<Selection> Selections { get; set; }
            [JsonProperty("Tags")]
            public MarketTags MarketTags { get; set; }
        }

        public class ParticipantTags
        {
            public string Weight { get; set; }
            public string Drawn { get; set; }
            public string Jockey { get; set; }
            public string Number { get; set; }
            public string Trainer { get; set; }
        }

        public class Participant
        {
            public int Id { get; set; }
            public string Name { get; set; }
            [JsonProperty("Tags")]
            public ParticipantTags ParticipantTags { get; set; }
        }

        public class RawData
        {
            public string FixtureName { get; set; }
            public string Id { get; set; }
            public DateTime StartTime { get; set; }
            public int Sequence { get; set; }
            public Tags Tags { get; set; }
            public List<Market> Markets { get; set; }
            public List<Participant> Participants { get; set; }
        }

        public class RootObject
        {
            public string FixtureId { get; set; }
            public DateTime Timestamp { get; set; }
            public RawData RawData { get; set; }
        }
    }
}
