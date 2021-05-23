using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadStatus.ApiClient;
using RoadStatus.ApiClient.RoadStatus;

namespace RoadService.Tests
{
    [TestClass]
    public class RoadStatusApiClientTests
    {
        // class being tested
        private readonly IRoadStatusApiClient _roadStatusApiClient;

        public RoadStatusApiClientTests()
        {
            _roadStatusApiClient = new RoadStatusApiClient("https://api.tfl.gov.uk/");
        }

        [TestMethod]
        public void GetRoadStatus_ValidRoadId_ShouldReturnSuccessResponse()
        {
            //Arrange
            string[] request = new string[] { "A1" };

            //Act
            var result = _roadStatusApiClient.GetRoadStatus(request).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
            Assert.AreEqual(result.Data.Count,1);
            Assert.AreEqual(result.Data[0].DisplayName, "A1");
            Assert.AreEqual(result.Data[0].StatusSeverity, "Good"); //If this value keep on changing, should modify this test.
            Assert.AreEqual(result.Data[0].StatusSeverityDescription, "No Exceptional Delays");//If this value keep on changing, should modify this test.
        }

        [TestMethod]
        public void GetRoadStatus_InvalidRoadId_ShouldReturnFailureResponse()
        {
            //Arrange
            string[] request = new string[] { "InvalidRoadId" };

            //Act
            var result = _roadStatusApiClient.GetRoadStatus(request).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Error);
            Assert.IsNull(result.Data);
            Assert.AreEqual(result.Error.message, "The following road id is not recognised: InvalidRoadId");
        }

        [TestMethod]
        public void GetRoadStatus_MultipleValidRoadId_ShouldReturnSuccessResponse()
        {
            //Arrange
            string[] request = new string[] { "A1", "A2" };

            //Act
            var result = _roadStatusApiClient.GetRoadStatus(request).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
            Assert.AreEqual(result.Data.Count, 2);
            Assert.AreEqual(result.Data[0].DisplayName, "A1");
            Assert.AreEqual(result.Data[0].StatusSeverity, "Good"); //If this value keep on changing, should modify this test.
            Assert.AreEqual(result.Data[0].StatusSeverityDescription, "No Exceptional Delays");//If this value keep on changing, should modify this test.
            Assert.AreEqual(result.Data[1].DisplayName, "A2");
            Assert.AreEqual(result.Data[1].StatusSeverity, "Good"); //If this value keep on changing, should modify this test.
            Assert.AreEqual(result.Data[1].StatusSeverityDescription, "No Exceptional Delays");//If this value keep on changing, should modify this test.

        }

        [TestMethod]
        public void GetRoadStatus_EmptyRoadId_ShouldReturnAllRoadIdsResponses()
        {
            //Arrange
            string[] request = new string[] { "" };

            //Act
            var result = _roadStatusApiClient.GetRoadStatus(request).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Data.Count > 0);
        }

        [TestMethod]
        public void GetRoadStatus_NullRoadId_ShouldReturnFailureResponse()
        {
            //Arrange
            string[] request = null;

            //Act
            var result = _roadStatusApiClient.GetRoadStatus(request).Result;

            //Assert
            Assert.IsNull(result);
        }
    }
}
