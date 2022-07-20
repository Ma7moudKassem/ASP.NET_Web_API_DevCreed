
namespace ASP.NET_Web_API_DevCreed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenresAll()
        {
            var genres = await _context.Genres.OrderBy(e=>e.Name).ToListAsync();
            return Ok(genres);
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre(GenreDto dto)
        {
            var genre = new Genre {Name = dto.Name };
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();
            return Ok(genre);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id ,[FromBody] GenreDto dto)
        {
            var genreFromDb = await _context.Genres.SingleOrDefaultAsync(e => e.Id == id);
            if (genreFromDb == null)
                return NotFound($"No genre was found with ID: {id}");

            genreFromDb.Name = dto.Name;
            _context.SaveChanges();
            return Ok(genreFromDb);
        }
    }
}
