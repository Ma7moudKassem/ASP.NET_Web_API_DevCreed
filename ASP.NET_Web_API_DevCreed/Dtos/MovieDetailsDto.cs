namespace ASP.NET_Web_API_DevCreed.Dtos
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string StoreLine { get; set; }
        public byte[] Poster { get; set; }
        public string GenreName { get; set; }
        public byte GenreId { get; set; }
    }
}
