namespace ASP.NET_Web_API_DevCreed.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDetailsDto>();
            CreateMap<MovieDto, Movie>().ForMember(src=>src.Poster, options=>options.Ignore());
        }
    }
}
