namespace Logic.Requests
{
    public class SearchMovieRequest
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public int? CountryId { get; set; }
        public int? ConstructionYear { get; set; }
    }
}