using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using RoadStatus.ApiClient;
using RoadStatus.Model;
using RoadStatus.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Assert = NUnit.Framework.Assert;

namespace RoadService.Tests
{
    [TestClass]
    public class RoadServiceTests
    {
        // mocked dependencies
        private readonly IRoadStatusApiClient _roadStatusApiClient;
        private readonly IRoadStatusClientFactory _roadStatusClientFactory;
        private readonly ILogger<RoadStatusService> _log;

        // class being tested
        private readonly IRoadStatusService _roadService;
        public RoadServiceTests()
        {
            _roadStatusApiClient = Substitute.For<IRoadStatusApiClient>();
            _roadStatusClientFactory = Substitute.For<IRoadStatusClientFactory>();
            _log = Substitute.For<ILogger<RoadStatus.Services.RoadStatusService>>();

            _roadService = new RoadStatusService(_roadStatusClientFactory, _log);
        }

        [TestMethod]
        public void GetRoadStatus_ValidRoadId_ShouldReturnSuccessResponse()
        {
            //Arrange
            string[] request = new string[] { "A1" };
            var roadStatus = new RoadStatus.Model.RoadStatus { DisplayName = "A1", StatusSeverity = "Good", StatusSeverityDescription = "No Exceptional Delays" };
            var roadStatusResponse = new RoadStatusResponse() { Data = new List<RoadStatus.Model.RoadStatus>() { roadStatus } };
            _roadStatusApiClient.GetRoadStatus(request).Returns(roadStatusResponse);
            _roadStatusClientFactory.GetClient().Returns(_roadStatusApiClient);
            
            var output = new StringWriter();
            Console.SetOut(output);

            //Act
            _roadService.GetRoadStatus(request);

            //Assert
            Assert.That(output.ToString(), Is.EqualTo("The status of the A1 is as follows\r\nRoad Status is Good\r\n" +
                "Road Status Description is No Exceptional Delays\r\n"));
        }

        [TestMethod]
        public void GetRoadStatus_InvalidRoadId_ShouldReturnFailureResponse()
        {
            //Arrange
            string[] request = new string[] { "InvalidRoadId" };
            var roadStatus = new RoadStatus.Model.Error { message = "The following road id is not recognised: InvalidRoadId" };
            var roadStatusResponse = new RoadStatusResponse() { Error = roadStatus };
            _roadStatusApiClient.GetRoadStatus(request).Returns(roadStatusResponse);
            _roadStatusClientFactory.GetClient().Returns(_roadStatusApiClient);

            var output = new StringWriter();
            Console.SetOut(output);

            //Act
            _roadService.GetRoadStatus(request);

            //Assert
            Assert.That(output.ToString(), Is.EqualTo("The following road id is not recognised: InvalidRoadId\r\n"));
        }

        [TestMethod]
        public void GetRoadStatus_MultipleValidRoadId_ShouldReturnSuccessResponse()
        {
            //Arrange
            string[] request = new string[] { "A1","A2" };
            var roadStatusA1 = new RoadStatus.Model.RoadStatus { DisplayName = "A1", StatusSeverity = "Good", StatusSeverityDescription = "No Exceptional Delays" };
            var roadStatusA2 = new RoadStatus.Model.RoadStatus { DisplayName = "A2", StatusSeverity = "Good", StatusSeverityDescription = "No Exceptional Delays" };
            var roadStatusResponse = new RoadStatusResponse() { Data = new List<RoadStatus.Model.RoadStatus>() { roadStatusA1,roadStatusA2 } };
            _roadStatusApiClient.GetRoadStatus(request).Returns(roadStatusResponse);
            _roadStatusClientFactory.GetClient().Returns(_roadStatusApiClient);

            var output = new StringWriter();
            Console.SetOut(output);

            //Act
            _roadService.GetRoadStatus(request);

            //Assert
            Assert.That(output.ToString(), Is.EqualTo("The status of the A1 is as follows\r\nRoad Status is Good\r\n" +
                "Road Status Description is No Exceptional Delays\r\nThe status of the A2 is as follows\r\nRoad Status is Good\r\n" +
                "Road Status Description is No Exceptional Delays\r\n"));
        }

        [TestMethod]
        public void GetRoadStatus_EmptyRoadId_ShouldReturnAllRoadStatusResponses()
        {
            //Arrange
            string[] request = new string[] { "" };
            var roadStatusA1 = new RoadStatus.Model.RoadStatus { DisplayName = "A1", StatusSeverity = "Good", StatusSeverityDescription = "No Exceptional Delays" };
            var roadStatusA2 = new RoadStatus.Model.RoadStatus { DisplayName = "A2", StatusSeverity = "Good", StatusSeverityDescription = "No Exceptional Delays" };
            var roadStatusA3 = new RoadStatus.Model.RoadStatus { DisplayName = "A3", StatusSeverity = "Good", StatusSeverityDescription = "No Exceptional Delays" };
            var roadStatusResponse = new RoadStatusResponse() { Data = new List<RoadStatus.Model.RoadStatus>() { roadStatusA1, roadStatusA2, roadStatusA3 } };

            _roadStatusApiClient.GetRoadStatus(request).Returns(roadStatusResponse);
            _roadStatusClientFactory.GetClient().Returns(_roadStatusApiClient);

            var output = new StringWriter();
            Console.SetOut(output);

            //Act
            _roadService.GetRoadStatus(request);

            //Assert
            Assert.That(output.ToString(), Is.EqualTo("The status of the A1 is as follows\r\nRoad Status is Good\r\n" +
                "Road Status Description is No Exceptional Delays\r\nThe status of the A2 is as follows\r\nRoad Status is Good\r\n" +
                "Road Status Description is No Exceptional Delays\r\nThe status of the A3 is as follows\r\nRoad Status is Good\r\n" +
                "Road Status Description is No Exceptional Delays\r\n"));
        }

        [TestMethod]
        public void GetRoadStatus_NullRoadId_ShouldReturnFailureResponse()
        {
            //Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            //Act
            _roadService.GetRoadStatus(null);

            //Assert
            Assert.That(output.ToString(), Is.EqualTo("Please enter valid road\r\n"));
        }
    }
}
