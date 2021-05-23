using System;

namespace RoadStatus.ApiClient
{
    /// <summary>
    /// This class used to build the Http Client object
    /// </summary>
    public interface IRoadStatusClientFactory:IDisposable
    {
        /// <summary>
        /// To get the http client object
        /// </summary>
        /// <returns></returns>
        IRoadStatusApiClient GetClient();
    }
}
