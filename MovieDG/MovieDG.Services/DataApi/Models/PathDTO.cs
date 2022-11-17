namespace MoviesDG.Core.DataApi.Models
{
    using System.Text.Json.Serialization;
    public class PathDTO
    {
        public string Name { get; set; }

        public string Site { get; set; }

        public string Type { get; set; }

        [JsonPropertyName("key")]
        public string Path { get; set; }

        public bool Official { get; set; }
    }
}
