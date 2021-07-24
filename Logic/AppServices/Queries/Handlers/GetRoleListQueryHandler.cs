using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Responses;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetRoleListQueryHandler : IQueryHandler<GetRoleListQuery, List<RoleResponse>>
    {
        private readonly MovieDataContext _dataContext;

        public GetRoleListQueryHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<List<RoleResponse>> Handle(GetRoleListQuery query)
        {
            IReadOnlyList<Role> roles = await _dataContext.Roles.ToListAsync();
            List<RoleResponse> dtos = roles.Select(x => ConvertToDto(x)).OrderBy(item => item.Id).ToList();
            return dtos;
        }
        
        private RoleResponse ConvertToDto(Role role)
        {
            return new RoleResponse
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}