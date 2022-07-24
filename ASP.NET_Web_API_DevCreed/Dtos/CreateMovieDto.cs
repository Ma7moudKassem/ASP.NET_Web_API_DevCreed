namespace ASP.NET_Web_API_DevCreed.Dtos
{
    public class CreateMovieDto : BaseMovieDto
    {
        public IFormFile Poster { get; set; }

    }
}
