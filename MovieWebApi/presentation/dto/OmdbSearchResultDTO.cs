namespace MovieWebApi.presentation.dto
{
    public class OmdbSearchResultDTO
    {
        public List<MovieDTO> Search { get; set; }
        public string TotalResults { get; set; }
        public string Response { get; set; }
    }
}
