namespace MoviesDG.Services.DataApi.Models
{
    using System.Text.Json.Serialization;
    public class CastDTO
    {
        [JsonPropertyName("id")]
        public int ActorId { get; set; }

        [JsonPropertyName("character")]
        public string CharacterName { get; set; }
    }
}
