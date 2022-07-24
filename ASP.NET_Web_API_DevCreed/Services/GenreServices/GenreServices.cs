using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Web_API_DevCreed.Services.GenreServices
{
    public class GenreServices : IGenreServices
    {
        private readonly ApplicationDbContext _context;
        public GenreServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAll()=> 
                                    await _context.Genres.OrderBy(e=>e.Name).ToListAsync();
        public async Task<Genre> GetById(byte id)=> await _context.Genres.SingleOrDefaultAsync(e=>e.Id == id);
        
        public async Task<Genre> Add(Genre genre)
        {
           await _context.Genres.AddAsync(genre);
           _context.SaveChanges();
            return genre;
        }

        public void Update(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();
        }

        public void Delete(Genre genre)
        {
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }

        public Task<bool> IsVailedGenre(byte id)=> _context.Genres.AnyAsync(e => e.Id == id);
    }
}
