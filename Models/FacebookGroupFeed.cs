using System.Collections.Generic;
using Newtonsoft.Json;

public class FacebookGroupFeed
{
    [JsonProperty(PropertyName="data")]
    public IEnumerable<FacebookFeed> Feeds { get; set; }
}