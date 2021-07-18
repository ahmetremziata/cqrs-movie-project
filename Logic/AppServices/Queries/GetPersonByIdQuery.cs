using Logic.Responses;

namespace Logic.AppServices.Queries
{
    public class GetPersonByIdQuery : IQuery<PersonDetailResponse>
    {
        public int PersonId { get; set; }
    }
}