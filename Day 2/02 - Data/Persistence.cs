using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Helpers;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    /// <summary>
    /// This is the data layer that interacts with CosmodDB and creates the connection.
    /// </summary>
    public class Persistence
    {
        // Set up the variables needed to manage the Cosmos DB data store.
        private string _databaseId;
        private string _collectionId;
        private Uri _endpointUri;
        private string _primaryKey;
        private DocumentClient _client;

        public Persistence(Uri endpointUri, string primaryKey)
        {
            _databaseId = "urlshortener";
            _collectionId = "urlcontainer";
            _endpointUri = endpointUri;
            _primaryKey = primaryKey;
        }

        public async Task EnsureSetupAsync()
        {
            if (_client == null)
            {
                _client = new DocumentClient(_endpointUri, _primaryKey);
            }

            await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _databaseId });
            var databaseUri = UriFactory.CreateDatabaseUri(_databaseId);

            await _client.CreateDocumentCollectionIfNotExistsAsync(databaseUri, new DocumentCollection() { Id = _collectionId });
        }

        public async Task SaveUrlAsync(ShortUrl url)
        {
            await EnsureSetupAsync();

            var documentCollectionUri = UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId);
            await _client.UpsertDocumentAsync(documentCollectionUri, url);
        }

        public async Task<ShortUrl> GetByIdAsync(int Id)
        {
            await EnsureSetupAsync();

            var documentCollectionUri = UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId);

            var result = _client.CreateDocumentQuery<ShortUrl>(documentCollectionUri)
                .Where(su => su.UrlId == Id).AsEnumerable().FirstOrDefault();

            return result;
        }

        public async Task<ShortUrl> GetUrlByPathAsync(string path)
        {
            await EnsureSetupAsync();

            var documentCollectionUri = UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId);

            var result = _client.CreateDocumentQuery<ShortUrl>(documentCollectionUri)
                .Where(su => su.UrlId == ShortUrlHelper.Decode(path)).AsEnumerable().FirstOrDefault();

            return result;
        }

    }
}
