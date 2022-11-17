namespace MoviesDG.Core.DataApi
{
    public interface ICollectService
    {
        Task<int> AddMoviesToDatabaseAsync(int startIndex, int endIndex);
    }
}
