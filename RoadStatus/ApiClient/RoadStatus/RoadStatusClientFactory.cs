using RoadStatus.ApiClient.RoadStatus;
using System.Configuration;

namespace RoadStatus.ApiClient
{
    /// <summary>
    /// This class used to build the Http Client object
    /// </summary>
    public class RoadStatusClientFactory : IRoadStatusClientFactory
    {
        /// <summary>
        /// Road Status Api client
        /// </summary>
        private static IRoadStatusApiClient _client;

        /// <summary>
        /// TFL Api base URL
        /// </summary>
        private readonly string _baseUri;

        /// <summary>
        /// Road Status Client Factory Constructor
        /// </summary>
        public RoadStatusClientFactory()
        {
            _baseUri = ConfigurationManager.AppSettings["TFLBaseURI"];
        }

        /// <summary>
        /// To dispose the http client
        /// </summary>
        public void Dispose()
        {
            _client?.Dispose();
            _client = null;
        }

        /// <summary>
        /// To get the http client object
        /// </summary>
        /// <returns></returns>
        public IRoadStatusApiClient GetClient() => _client ?? (_client = CreateNewClient());

        /// <summary>
        /// To create the new http client
        /// </summary>
        /// <returns></returns>
        private IRoadStatusApiClient CreateNewClient() => new RoadStatusApiClient(_baseUri);

    }
}
