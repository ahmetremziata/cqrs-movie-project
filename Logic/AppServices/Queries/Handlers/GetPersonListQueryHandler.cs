using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Responses;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetPersonListQueryHandler : IQueryHandler<GetPersonListQuery, List<PersonResponse>>
    {
        private readonly MovieDataContext _dataContext;

        public GetPersonListQueryHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<PersonResponse>> Handle(GetPersonListQuery query)
        {
            IReadOnlyList<Person> persons = await _dataContext.Persons.ToListAsync();
            List<PersonResponse> dtos = persons.Select(x => ConvertToDto(x)).OrderBy(item => item.PersonId).ToList();
            return dtos;        
        }
        
        private PersonResponse ConvertToDto(Person person)
        {
            return new PersonResponse
            {
                PersonId = person.Id,
                Name = person.Name,
                RealName = person.RealName,
                AvatarUrl = person.AvatarUrl
            };
        }
    }
}