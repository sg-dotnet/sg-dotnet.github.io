using Newtonsoft.Json;

namespace CommunityWeb.Models
{
    public class MeetupLocation
    {
        [JsonProperty(PropertyName="name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName="lat")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName="lon")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName="address_1")]
        public string Address1 { get; set; }

        [JsonProperty(PropertyName="address_2")]
        public string Address2 { get; set; }

        [JsonProperty(PropertyName="address_3")]
        public string Address3 { get; set; }

        [JsonProperty(PropertyName="localized_country_name")]
        public string Country { get; set; }

        public override string ToString()
        {
            return Name + "<br />" + 
                (string.IsNullOrWhiteSpace(Address1) ? "" : Address1.Trim()) + ", " +
                (string.IsNullOrWhiteSpace(Address2) ? "" : Address2.Trim() + ", ") +
                (string.IsNullOrWhiteSpace(Address3) ? "" : Address3.Trim() + ", ") +
                Country;
        }
    }
    
}