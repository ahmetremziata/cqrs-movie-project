using Logic.Responses;

namespace Logic.AppServices.Queries
{
    public sealed class GetMoviePresentationListQuery : IQuery<MoviePresentationResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public int? CountryId { get; set; }
        public int? ConstructionYear { get; set; }
    }
}