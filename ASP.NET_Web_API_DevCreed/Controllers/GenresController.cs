namespace ASP.NET_Web_API_DevCreed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreServices _genreServices;

        public GenresController(IGenreServices genreServices)
        {
            _genreServices = genreServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenresAll()
        {
            var genres = await _genreServices.GetAll();
            return Ok(genres);
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre(GenreDto dto)
        {
            var genre = new Genre {Name = dto.Name };
            await _genreServices.Add(genre);
            return Ok(genre);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(byte id ,[FromBody] GenreDto dto)
        {
            var genreFromDb = await _genreServices.GetById(id);
            if (genreFromDb == null)
                return NotFound($"No genre was found with ID: {id}");

            genreFromDb.Name = dto.Name;
            _genreServices.Update(genreFromDb);
            return Ok(genreFromDb);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(byte id)
        {
            var genreFromDb = await _genreServices.GetById(id);
            if (genreFromDb == null)
                return NotFound($"No genre was found with ID: {id}");

            _genreServices.Delete(genreFromDb);

            return Ok();
        }
    }
}
