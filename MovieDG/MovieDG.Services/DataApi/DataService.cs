namespace MoviesDG.Core.DataApi
{
    using Microsoft.Extensions.Configuration;
    using MoviesDG.Core.DataApi.Models;
    using System.Net.Http.Json;
    using System.Text.Json;

    using static MovieDG.Core.ErrorMessages.ErrorMessageConstants;
    public class DataService : IDataService
    {
        private const string BaseUrl = "https://api.themoviedb.org/3";
        private string key;
        private readonly IConfiguration configuration;
        private readonly HttpClient client = new HttpClient();

        public DataService(IConfiguration configuration)
        {
            this.configuration = configuration;

            this.key = this.configuration.GetSection($"TMDB:ApiKey").Value;
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
                    Console.WriteLine(HttpRequestExceptionErrorMessage);
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine(NotSupportedException);
                }
                catch (JsonException)
                {
                    Console.WriteLine(JsonExceptionErrorMessage);
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
                    Console.WriteLine(HttpRequestExceptionErrorMessage);
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine(NotSupportedException);
                }
                catch (JsonException)
                {
                    Console.WriteLine(JsonExceptionErrorMessage);
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
                    Console.WriteLine(HttpRequestExceptionErrorMessage);
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine(NotSupportedException);
                }
                catch (JsonException)
                {
                    Console.WriteLine(JsonExceptionErrorMessage);
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
                    Console.WriteLine(HttpRequestExceptionErrorMessage);
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine(NotSupportedException);
                }
                catch (JsonException)
                {
                    Console.WriteLine(JsonExceptionErrorMessage);
                }
            }

            return null;
        }
    }
}
