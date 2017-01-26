using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

public class DocumentDbConnector{
    
    private DocumentClient _client;

    public DocumentDbConnector(string documentDbEndpointUrl, string documentDbPrimaryKey,
        string databaseId, string collectionId)
    {
        _client = new DocumentClient(new Uri(documentDbEndpointUrl), documentDbPrimaryKey);
        CreateDatabaseIfNotExistsAsync(databaseId).Wait();
        CreateCollectionIfNotExistsAsync(databaseId, collectionId).Wait();
    }

    public DocumentClient DocumentClient 
    { 
        get
        {
            return _client;
        }
    }

    private async Task CreateDatabaseIfNotExistsAsync(string databaseId)
    {
        try
        {
            await _client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId));
        }
        catch (DocumentClientException e)
        {
            if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await _client.CreateDatabaseAsync(new Database { Id = databaseId });
            }
            else
            {
                throw;
            }
        }
    }

    private async Task CreateCollectionIfNotExistsAsync(string databaseId, string collectionId)
    {
        try
        {
            await _client.ReadDocumentCollectionAsync(
                UriFactory.CreateDocumentCollectionUri(databaseId, collectionId));
        }
        catch (DocumentClientException e)
        {
            if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await _client.CreateDocumentCollectionAsync(
                    UriFactory.CreateDatabaseUri(databaseId),
                    new DocumentCollection { Id = collectionId },
                    new RequestOptions { OfferThroughput = 1000 });
            }
            else
            {
                throw;
            }
        }
    }
}

