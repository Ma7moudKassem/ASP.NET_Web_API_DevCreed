namespace ASP.NET_Web_API_DevCreed.Services.MovieServices
{
    public class MovieServices : IMovieServices
    {
        private readonly ApplicationDbContext _context;
        public MovieServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0) => await _context.Movies
            .Where(e=>e.GenreId == genreId || genreId ==0 )
            .OrderByDescending(e => e.Rate)
            .Include(e => e.Genre)
            .ToListAsync();
        public async Task<Movie> GetById(int id) => await _context.Movies.Include(e=>e.Genre).SingleOrDefaultAsync(e=>e.Id==id);
        public async Task<Movie> Add(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            _context.SaveChanges();

            return movie;
        }
        public async void Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
        }
        public void Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
        }
    }
}
