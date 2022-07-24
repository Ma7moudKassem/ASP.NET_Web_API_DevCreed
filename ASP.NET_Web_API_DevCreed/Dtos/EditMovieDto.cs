namespace ASP.NET_Web_API_DevCreed.Dtos
{
    public class EditMovieDto : BaseMovieDto
    {
        public IFormFile? Poster { get; set; }
    }
}
