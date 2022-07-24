using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Web_API_DevCreed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private const long _maxAllowSizeForImages = 1048576;
        private List<string> _allowedExtenstions = new List<string> { ".png", ".jpg" };

        private readonly IMapper _mapper;
        private readonly IGenreServices _genreServices;
        private readonly IMovieServices _movieServices;
        public MoviesController(IGenreServices genreServices, IMovieServices movieServices, IMapper mapper)
        {
            _genreServices = genreServices;
            _movieServices = movieServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var movies = await _movieServices.GetAll();

            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieServices.GetById(id);
            if (movie == null)
                return NotFound();

            var data = _mapper.Map<MovieDetailsDto>(movie);

            return Ok(data);
        }

        [HttpGet("{GetByGenreId}")]
        public async Task<IActionResult> GetMovieByGenreId(byte genreId)
        {
            var movies = await _movieServices.GetAll(genreId);

            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]CreateMovieDto dto)
        {
            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and jpg extension are allowed");
            if (dto.Poster.Length > _maxAllowSizeForImages)
                return BadRequest("Max allowed size for poster is 1MB");

            bool isValidGenre = await _genreServices.IsVailedGenre(dto.GenreId);
            if (!isValidGenre)
                return BadRequest("Oops invalid genre id");

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<Movie>(dto);
            movie.Poster = dataStream.ToArray(); 
            _movieServices.Add(movie);
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var movieFromDb = await _movieServices.GetById(id);

            if (movieFromDb == null)
                return NotFound($"This Movie that has ID {id} not found");

            _movieServices.Delete(movieFromDb);
            return Ok(movieFromDb);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] EditMovieDto dto)
        {
            var movie = await _movieServices.GetById(id);
            if (movie == null)
                return NotFound($"No movie was found with ID: {id}");

            bool isValidGenre = await _genreServices.IsVailedGenre(dto.GenreId);
            if (!isValidGenre)
                return BadRequest("Oops invalid genre id");

            if (dto.Poster != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and jpg extension are allowed");
                if (dto.Poster.Length > _maxAllowSizeForImages)
                    return BadRequest("Max allowed size for poster is 1MB");

                using var dataStream = new MemoryStream();

                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }

            movie.Title = dto.Title;
            movie.StoreLine = dto.StoreLine;
            movie.GenreId = dto.GenreId;
            movie.Rate = dto.Rate;
            movie.Year = dto.Year;

            _movieServices.Update(movie);

            return Ok(movie);
        }
    }
}
