namespace MoviesDG.Core.DataApi.Models
{
    using System.Text.Json.Serialization;
    public class TrailerDTO
    {
        [JsonPropertyName("results")]
        public ICollection<PathDTO> Trailers { get; set; }
    }
}
