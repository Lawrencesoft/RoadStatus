using System.Text.Json.Serialization;

namespace RoadStatus.Model
{
    /// <summary>
    /// To capture the Error object from the TFL Api 
    /// </summary>
    public class Error
    {

        /// <summary>
        /// Error Message
        /// </summary>
        [JsonPropertyName("message")]
        public string message { get; set; }

        /// <summary>
        /// Error Type
        /// </summary>
        [JsonPropertyName("$type")]
        public string Type { get; set; }

        /// <summary>
        /// Error Timestamp UTC
        /// </summary>
        [JsonPropertyName("timestampUtc")]
        public string TimestampUtc { get; set; }

        /// <summary>
        /// Exception Type
        /// </summary>
        [JsonPropertyName("exceptionType")]
        public string ExceptionType { get; set; }

        /// <summary>
        /// Error Status Code
        /// </summary>
        [JsonPropertyName("httpStatusCode")]
        public int HttpStatusCode { get; set; }

        /// <summary>
        /// Error Status
        /// </summary>
        [JsonPropertyName("httpStatus")]
        public string HttpStatus { get; set; }

    }
}
