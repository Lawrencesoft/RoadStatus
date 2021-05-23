using RoadStatus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoadStatus.ApiClient.RoadStatus
{
    /// <summary>
    /// Client used to invoke the TFL service to get the Road Status.
    /// </summary>
    public sealed class RoadStatusApiClient : IRoadStatusApiClient
    {
        /// <summary>
        /// TFL Api base address
        /// </summary>
        private readonly string _baseAddress;

        /// <summary>
        /// Http Client
        /// </summary>
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Road Status Api client constructor
        /// </summary>
        /// <param name="baseUri"></param>
        public RoadStatusApiClient(string baseUri)
        {
            _baseAddress = baseUri;
        }

        /// <summary>
        /// To get the Road status from TFL service
        /// </summary>
        /// <param name="roadIds"></param>
        /// <returns></returns>
        public async Task<RoadStatusResponse> GetRoadStatus(IList<string> roadIds)
        {
            if (roadIds == null || roadIds?.Count <= 0) return null;
            var roadStatusResponse = new RoadStatusResponse();
            const string route = "Road/";
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("AppID", "AppKey");

            string requestUri = _baseAddress + route + string.Join(",", roadIds);
            var response = await client.GetAsync(requestUri);
            var contentString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var roadStatus = JsonSerializer.Deserialize<List<Model.RoadStatus>>(contentString);
                roadStatusResponse.Data = roadStatus;
            }
            else
            {
                var error = JsonSerializer.Deserialize<Error>(contentString);
                roadStatusResponse.Error = error;
            }
            return roadStatusResponse;
        }

        /// <summary>
        /// Dispose the client object
        /// </summary>
        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
