using System;
using Newtonsoft.Json;

namespace CommunityWeb.Models
{
    public class MeetupEvent
    {
        [JsonProperty(PropertyName="id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName="name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName="created")]
        public Int64 CreatedAt { get; set; }

        [JsonProperty(PropertyName="time")]
        public Int64 HappensAt { get; set; }

        public DateTime HappensAtDateTime { get; set; }

        [JsonProperty(PropertyName="yes_rsvp_count")]
        public int NumberOfRsvpCount { get; set; }

        [JsonProperty(PropertyName="link")]
        public string MeetupPageUrl { get; set; }

        [JsonProperty(PropertyName="description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName="venue")]
        public MeetupLocation Venue { get; set; }

        [JsonProperty(PropertyName="visibility")]
        public string Visibility { get; set; }
    }
    
}