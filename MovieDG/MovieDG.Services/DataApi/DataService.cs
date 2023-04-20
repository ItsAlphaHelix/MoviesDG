namespace MoviesDG.Core.DataApi
{
    using Azure.Identity;
    using Microsoft.Extensions.Configuration;
    using MoviesDG.Core.DataApi.Models;
    using System.Net.Http.Json;
    using System.Text.Json;
    public class DataService : IDataService
    {
        private const string BaseUrl = "https://api.themoviedb.org/3";
        private string key;
        private readonly IConfiguration configuration;
        private readonly HttpClient client = new HttpClient();

        public DataService(IConfiguration configuration)
        {
            this.configuration = configuration;
            //var keyVaultUrl = new Uri(builder.Configuration.GetSection("KeyVaultURl").Value!);
            //var azureCredential = new DefaultAzureCredential();

            //builder.Configuration.AddAzureKeyVault(keyVaultUrl, azureCredential);

            //var keyVaultUrl = new Uri(configuration.GetSection("TMDB").Value!);
            //var azureCredential = new DefaultAzureCredential();

            this.key = this.configuration.GetSection($"TMDBKey").Value;
        }
        public async Task<MovieDTO> GetMovieDataAsync(int movieId)
        {
            using HttpResponseMessage response = await this.client.GetAsync($"{BaseUrl}/movie/{movieId}?api_key={key}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using HttpContent content = response.Content;

                    return await content.ReadFromJsonAsync<MovieDTO>();
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine("An error occurred.");
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine("The content type is not supported.");
                }
                catch (JsonException)
                {
                    Console.WriteLine("Invalid JSON.");
                }
            }

            return null;
        }
        public async Task<TrailerDTO> GetMovieTrailersAsync(int movieId)
        {
            using HttpResponseMessage response = await this.client.GetAsync($"{BaseUrl}/movie/{movieId}/videos?api_key={this.key}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using HttpContent content = response.Content;

                    return await content.ReadFromJsonAsync<TrailerDTO>();
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine("An error occurred.");
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine("The content type is not supported.");
                }
                catch (JsonException)
                {
                    Console.WriteLine("Invalid JSON.");
                }
            }

            return null;
        }
        public async Task<ActorDTO> GetActorAsync(int actorId)
        {
            using HttpResponseMessage response = await this.client.GetAsync($"{BaseUrl}/person/{actorId}?api_key={this.key}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using HttpContent content = response.Content;

                    return await content.ReadFromJsonAsync<ActorDTO>();
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine("An error occurred.");
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine("The content type is not supported.");
                }
                catch (JsonException)
                {
                    Console.WriteLine("Invalid JSON.");
                }
            }

            return null;
        }
        public async Task<CastAndCrewDTO> GetCastAndCrewAsync(int movieId)
        {
            using HttpResponseMessage response = await this.client.GetAsync($"{BaseUrl}/movie/{movieId}/credits?api_key={this.key}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using HttpContent content = response.Content;

                    return await content.ReadFromJsonAsync<CastAndCrewDTO>();
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine("An error occurred.");
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine("The content type is not supported.");
                }
                catch (JsonException)
                {
                    Console.WriteLine("Invalid JSON.");
                }
            }

            return null;
        }
    }
}
