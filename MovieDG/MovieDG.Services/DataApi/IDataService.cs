namespace MoviesDG.Services.DataApi
{
    using MoviesDG.Services.DataApi.Models;
    public interface IDataService
    {
        Task<MovieDTO> GetMovieDataAsync(int movieId);

        Task<ActorDTO> GetActorAsync(int actorId);

        Task<TrailerDTO> GetMovieTrailersAsync(int movieId);

        Task<CastAndCrewDTO> GetCastAndCrewAsync(int movieId);
    }
}
