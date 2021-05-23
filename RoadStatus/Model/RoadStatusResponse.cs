using System.Collections.Generic;

namespace RoadStatus.Model
{
    /// <summary>
    /// This class used to pass the Road status and Error details to Service layer 
    /// </summary>
    public class RoadStatusResponse
    {
        /// <summary>
        /// Road Status Data
        /// </summary>
        public IList<RoadStatus> Data { get; set; }

        /// <summary>
        /// Error Details
        /// </summary>
        public Error Error { get; set; }
    }
}
