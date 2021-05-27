using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Responses;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetTypeListQueryHandler : IQueryHandler<GetTypeListQuery, List<TypeResponse>>
    {
        private readonly MovieDataContext _dataContext;

        public GetTypeListQueryHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<List<TypeResponse>> Handle(GetTypeListQuery query)
        {
            IReadOnlyList<Type> types = await _dataContext.Types.ToListAsync();
            List<TypeResponse> dtos = types.Select(x => ConvertToDto(x)).OrderBy(item => item.Name).ToList();
            return dtos;
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