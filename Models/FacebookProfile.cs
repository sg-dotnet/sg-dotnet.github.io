using Newtonsoft.Json;

public class FacebookProfile
{
    [JsonProperty(PropertyName="id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName="name")]
    public string Name { get; set; }
}