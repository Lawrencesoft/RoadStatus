using System.Text.Json.Serialization;

namespace RoadStatus.Model
{
    /// <summary>
    /// To capture the Road status details from TFL Api
    /// </summary>
    public class RoadStatus
    {
        /// <summary>
        /// Road Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Display Name
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Road Group
        /// </summary>
        [JsonPropertyName("group")]
        public string Group { get; set; }

        /// <summary>
        /// Road Status Severity
        /// </summary>
        [JsonPropertyName("statusSeverity")]
        public string StatusSeverity { get; set; }

        /// <summary>
        /// Road Status Severity Description
        /// </summary>
        [JsonPropertyName("statusSeverityDescription")]
        public string StatusSeverityDescription { get; set; }

        /// <summary>
        /// Bounds
        /// </summary>
        [JsonPropertyName("bounds")]
        public string Bounds { get; set; }

        /// <summary>
        /// Envelope
        /// </summary>
        [JsonPropertyName("envelope")]
        public string Envelope { get; set; }

        /// <summary>
        /// Status Aggregation Start Date
        /// </summary>
        [JsonPropertyName("statusAggregationStartDate")]
        public string StatusAggregationStartDate { get; set; }

        /// <summary>
        /// Status Aggregation End Date
        /// </summary>
        [JsonPropertyName("statusAggregationEndDate")]
        public string StatusAggregationEndDate { get; set; }

        /// <summary>
        /// Road Status URL
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
