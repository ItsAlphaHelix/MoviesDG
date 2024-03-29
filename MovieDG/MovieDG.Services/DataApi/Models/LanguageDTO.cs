﻿namespace MoviesDG.Core.DataApi.Models
{
    using System.Text.Json.Serialization;
    public class LanguageDTO
    {
        [JsonPropertyName("iso_639_1")]
        public string ISO { get; set; }

        [JsonPropertyName("english_name")]
        public string Name { get; set; }
    }
}
