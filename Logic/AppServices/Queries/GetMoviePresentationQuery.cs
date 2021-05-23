namespace Logic.AppServices.Queries
{
    public sealed class GetMoviePresentationQuery : IQuery<Logic.Indexes.Movie>
    {
        public int MovieId { get; set; }
    }
}