namespace Logic.Requests
{
    public class SearchAdminMovieRequest
    {
        public string MovieName { get; set; }
        public string ActorName { get; set; }
        public int? TypeId { get; set; }
        public int? CountryId { get; set; }
        public int? ConstructionYear { get; set; }
        public bool? IsDomestic { get; set; }
        public bool? IsInternational { get; set; }
    }
}