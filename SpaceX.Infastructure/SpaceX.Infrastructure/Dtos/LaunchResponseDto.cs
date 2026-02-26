using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.Launches.Dtos
{
    public class LaunchResponseDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("flight_number")]
        public int FlightNumber { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("date_utc")]
        public string DateUtc { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }

        [JsonPropertyName("links")]
        public Links Links { get; set; }
    }

    public class Patch
    {
        [JsonPropertyName("large")]
        public string Large { get; set; }

        [JsonPropertyName("small")]
        public string Small { get; set; }
    }

    public class Links
    {
        [JsonPropertyName("patch")]
        public Patch Patch { get; set; }

        [JsonPropertyName("webcast")]
        public string Webcast { get; set; }

        [JsonPropertyName("youtube_id")]
        public string YoutubeId { get; set; }

        [JsonPropertyName("article")]
        public string Article { get; set; }
    }
}
