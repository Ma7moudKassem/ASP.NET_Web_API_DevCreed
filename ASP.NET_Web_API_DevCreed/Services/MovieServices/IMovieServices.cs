namespace ASP.NET_Web_API_DevCreed.Services.MovieServices
{
    public interface IMovieServices
    {
        Task<IEnumerable<Movie>> GetAll(byte genreId = 0);
        Task<Movie> GetById(int id);
        Task<Movie> Add(Movie movie);
        void Update(Movie movie);
        void Delete(Movie movie);
    }
}
