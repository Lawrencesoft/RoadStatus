using RoadStatus.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadStatus.ApiClient
{
    /// <summary>
    /// Client used to invoke the TFL service to get the Road Status.
    /// </summary>
    public interface IRoadStatusApiClient:IDisposable
    {
        /// <summary>
        /// To get the Road status from TFL service
        /// </summary>
        /// <param name="roadIds"></param>
        /// <returns></returns>
        Task<RoadStatusResponse> GetRoadStatus(IList<string> roadIds);
    }
}
