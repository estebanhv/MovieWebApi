using MovieWebApi.presentation.dto;

namespace MovieWebApi.Persistence.entities
{
    public class OmdbSearchResult
    {
        public List<MovieDTO> Search { get; set; }
        public string TotalResults { get; set; }
        public string Response { get; set; }
    }
}
