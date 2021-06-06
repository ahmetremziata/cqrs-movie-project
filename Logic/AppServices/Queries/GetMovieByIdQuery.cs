using Logic.Responses;

namespace Logic.AppServices.Queries
{
    public sealed class GetMovieByIdQuery : IQuery<MovieDetailResponse>
    {
        public int MovieId { get; set; }
    }
}