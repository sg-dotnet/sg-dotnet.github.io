namespace CommunityWeb.Models
{
    public class AppSettings
    {
        public string MeetupWebApiKey { get; set; }

        public string DotnetsgDocumentDbEndpoint { get; set; }

        public string DotnetsgDocumentDbPrimaryKey { get; set; }

        public string DocumentDbDatabaseFeeds { get; set; }

        public string DocumentDbCollectionFacebookGroupFeeds { get; set; }
    }
}