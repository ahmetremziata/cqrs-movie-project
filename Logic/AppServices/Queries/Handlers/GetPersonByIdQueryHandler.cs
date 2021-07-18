using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Responses;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetPersonByIdQueryHandler : IQueryHandler<GetPersonByIdQuery, PersonDetailResponse>
    {
        private readonly MovieDataContext _dataContext;

        public GetPersonByIdQueryHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<PersonDetailResponse> Handle(GetPersonByIdQuery query)
        {
            Person person = await _dataContext.Persons.SingleOrDefaultAsync(item => item.Id == query.PersonId);
            if (person == null)
            {
                return null;
            }
            
            return await ConvertToDto(person);
        }
        
        private async Task<PersonDetailResponse> ConvertToDto(Person person)
        {
            var personDetail = new PersonDetailResponse
            {
                PersonId = person.Id,
                Name = person.Name,
                RealName = person.RealName,
                Biography = person.Biography,
                AvatarUrl = person.AvatarUrl,
                BirthDate = person.BirthDate,
                BirthPlace = person.BirthPlace,
                DeathDate = person.DeathDate,
                DeathPlace = person.DeathPlace
            };
            
            var moviePersons = await _dataContext.MoviePersons.Where(item => item.PersonId == person.Id).ToListAsync();

            List<PersonMovieResponse> personMovieResponse = new List<PersonMovieResponse>();

            foreach (var moviePerson in moviePersons)
            {
                var role = await _dataContext.Roles.SingleAsync(item => item.Id == moviePerson.RoleId);
                var movie = await _dataContext.Movies.SingleAsync(item => item.Id == moviePerson.MovieId);
                
                personMovieResponse.Add(new PersonMovieResponse()
                {
                    CharacterName = moviePerson.CharacterName,
                    ConstructionYear = movie.ConstructionYear,
                    MovieId = movie.Id,
                    Name = movie.Name,
                    OriginalName = movie.OriginalName,
                    PosterUrl = movie.PosterUrl,
                    Role = role.Name,
                    TotalMinute = movie.TotalMinute,
                    VisionEntryDate = movie.VisionEntryDate
                });
            }

            personDetail.Movies = personMovieResponse;

            return personDetail;
        }
    }
}