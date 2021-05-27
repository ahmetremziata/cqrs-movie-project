using System.Threading.Tasks;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Responses;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetTypeByIdQueryHandler : IQueryHandler<GetTypeByIdQuery, TypeResponse>
    {
        private readonly MovieDataContext _dataContext;

        public GetTypeByIdQueryHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<TypeResponse> Handle(GetTypeByIdQuery query)
        {
            Type type = await _dataContext.Types.SingleOrDefaultAsync(item => item.Id == query.TypeId);
            if (type == null)
            {
                return null;
            }
            
            return ConvertToDto(type);
        }
        
        private TypeResponse ConvertToDto(Type Type)
        {
            return new TypeResponse
            {
                Id = Type.Id,
                Name = Type.Name
            };
        }
    }
}