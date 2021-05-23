using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadStatus.Services
{
    /// <summary>
    /// Road Status Service Interface
    /// </summary>
    public interface IRoadStatusService
    {
        /// <summary>
        /// This method used to get the Road status from TFL Api service
        /// </summary>
        /// <param name="roadIds"></param>
        void GetRoadStatus(IList<string> roadIds);
    }
}
