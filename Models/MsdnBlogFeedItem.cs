using System;
using System.Collections.Generic;

public class MsdnBlogFeedItem
{
    public string Link { get; set; } 
    public string Title { get; set; } 
    public string Description { get; set; } 
    public DateTime PublishDate { get; set; } 
    public List<string> Categories { get; set; }
}