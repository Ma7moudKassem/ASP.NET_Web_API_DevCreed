namespace ASP.NET_Web_API_DevCreed.Dtos
{
    public class BaseMovieDto
    {
        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }

        [MaxLength(2500)]
        public string StoreLine { get; set; }
        public byte GenreId { get; set; }
    }
}
