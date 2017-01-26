using System;
using Newtonsoft.Json;

public class FacebookFeed
{
    [JsonProperty(PropertyName="id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName="story")]
    public string Story { get; set; }

    [JsonProperty(PropertyName="message")]
    public string Message { get; set; }

    [JsonProperty(PropertyName="name")]
    public string ArticleName { get; set; }

    [JsonProperty(PropertyName="description")]
    public string ArticleDescription { get; set; }

    [JsonProperty(PropertyName="link")]
    public string ArticleUrl { get; set; }

    [JsonProperty(PropertyName="from")]
    public FacebookProfile Author { get; set; }

    [JsonProperty(PropertyName="updated_time")]
    public DateTimeOffset UpdatedTime { get; set; }
}