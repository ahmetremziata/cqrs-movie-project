using Logic.Responses;

namespace Logic.AppServices.Queries
{
    public sealed class GetTypeByIdQuery : IQuery<TypeResponse>
    {
        public int TypeId { get; set; }
    }
}