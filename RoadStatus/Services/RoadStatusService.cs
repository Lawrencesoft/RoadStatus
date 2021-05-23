using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RoadStatus.ApiClient;
using System;
using System.Collections.Generic;

namespace RoadStatus.Services
{
    /// <summary>
    /// Road Status Service Implementation
    /// </summary>
    public class RoadStatusService : IRoadStatusService
    {
        /// <summary>
        /// The Client Factory
        /// </summary>
        private readonly IRoadStatusClientFactory _clientFactory;

        /// <summary>
        /// The Logger
        /// </summary>
        private readonly ILogger<RoadStatusService> _log;

        /// <summary>
        /// Road Status Constructor
        /// </summary>
        /// <param name="clientFactory"></param>
        /// <param name="log"></param>
        public RoadStatusService(IRoadStatusClientFactory clientFactory,ILogger<RoadStatusService> log)
        {
            _clientFactory = clientFactory;
            _log = log;
        }

        /// <summary>
        /// This method used to get the Road status from TFL Api service
        /// </summary>
        /// <param name="roadIds"></param>
        public void GetRoadStatus(IList<string> roadIds)
        {
            try
            {
                if (roadIds?.Count > 0)
                {
                    _log.LogInformation($"Started invocation of method '{nameof(RoadStatusService)}.{nameof(GetRoadStatus)}' OrderIds:" + string.Join(",", roadIds));

                    var client = _clientFactory.GetClient();
                    var response = client.GetRoadStatus(roadIds).Result;

                    if (response?.Data?.Count > 0)
                    {
                        foreach (var item in response.Data)
                        {
                            Console.WriteLine($"The status of the {item.DisplayName} is as follows");
                            Console.WriteLine($"Road Status is {item.StatusSeverity}");
                            Console.WriteLine($"Road Status Description is {item.StatusSeverityDescription}");
                        }
                        Environment.ExitCode = 0;
                    }
                    else
                    {
                        Console.WriteLine((response?.Error != null) ? response.Error.message : "You have entered invalid road");
                        Environment.ExitCode = 1;
                    }
                    _log.LogInformation($"Completed invocation of method '{nameof(RoadStatusService)}.{nameof(GetRoadStatus)}' OrderIds:" + string.Join(",", roadIds) + "Response:" + JsonConvert.SerializeObject(response));
                }
                else
                {
                    Console.WriteLine("Please enter valid road");
                    Environment.ExitCode = 1;
                }
            }
            catch (Exception ex)
            {
                Environment.ExitCode = 1;
                _log.LogError($"Exception on invocation of method '{nameof(RoadStatusService)}.{nameof(GetRoadStatus)}' OrderIds:" + string.Join(",", roadIds) + "errorMessage:" + ex.Message);
            }
        }
    }
}
