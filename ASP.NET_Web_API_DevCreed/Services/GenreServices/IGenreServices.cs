namespace ASP.NET_Web_API_DevCreed.Services.GenreServices
{
    public interface IGenreServices
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(byte id);
        Task<Genre> Add(Genre genre);
        void Update(Genre genre);
        void Delete(Genre genre);
        Task<bool> IsVailedGenre(byte id);
    }
}
