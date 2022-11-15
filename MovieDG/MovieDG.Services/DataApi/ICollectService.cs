namespace MoviesDG.Services.DataApi
{
    public interface ICollectService
    {
        Task<int> AddMoviesToDatabaseAsync(int startIndex, int endIndex);
    }
}
