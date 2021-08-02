using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Logic.Responses;
using Logic.Utils;
using Npgsql;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetMovieByIdWithSeperateDomainModelQueryHandler : IQueryHandler<GetMovieByIdQuery, MovieDetailResponse>
    {
        private readonly ConnectionString _connection;
        
        public GetMovieByIdWithSeperateDomainModelQueryHandler(ConnectionString connection)
        {
            _connection = connection;
        }
        
        public async Task<MovieDetailResponse> Handle(GetMovieByIdQuery query)
        {
            List<MovieInDb> movies = new List<MovieInDb>();
            string sql = @"
            SELECT
                   m.id movie_id,
                   m.name movie_name,
                   m.original_name,
                   m.description,
                   m.construction_year,
                   m.total_minute,
                   m.poster_url,
                   m.is_active,
                   m.vision_entry_date,
                   (select count(1) from movie_persons mp where mp.movie_id=1 and mp.role_id=1) total_actor_count,
                   c.id country_id,
                   c.name country_name,
                   t.id type_id,
                   t.name type_name,
                   p.id person_id,
                   p.name actor_name,
                   mp.character_name,
                   r.name role_name,
                   r.id role_id
            FROM movies m
            left join movie_countries mc on m.id = mc.movie_id
            left join movie_types mt on m.id = mt.movie_id
            left join movie_persons mp on m.id = mp.movie_id
            left join roles r on r.id=mp.role_id
            left join types t on mt.type_id = t.id
            left join countries c on mc.country_id = c.id
            left join persons p on mp.person_id = p.id
            where
                m.id=@movieId
            order by r.id
            ";
            
            using (NpgsqlConnection connection = new NpgsqlConnection(_connection.Value))
            {
                movies = connection
                    .Query<MovieInDb>(sql,
                        new
                        {
                            MovieId = query.MovieId
                        }
                    ).ToList();
            }

            if (!movies.Any())
            {
                return null;
            }

            MovieDetailResponse result = new MovieDetailResponse();

            var firstMovie = movies.First();
            result.Id = firstMovie.movie_id;
            result.Description = firstMovie.description;
            result.OriginalName = firstMovie.original_name;
            result.ConstructionYear = firstMovie.construction_year;
            result.PosterUrl = firstMovie.poster_url;
            result.TotalMinute = firstMovie.total_minute;
            result.TotalActorCount = firstMovie.total_actor_count;
            result.Name = firstMovie.movie_name;
            result.VisionEntryDate = firstMovie.vision_entry_date;
            result.IsActive = firstMovie.is_active;
            result.Types = await GetTypes(movies);
            result.Countries = await GetCountries(movies);
            result.Team = await GetTeam(movies);
            return result;
        }
        
        private async Task<List<MovieTypeResponse>> GetTypes(List<MovieInDb> movies)
        {
            List<MovieTypeResponse> result = new List<MovieTypeResponse>();

            foreach (var movie in movies)
            {
                if (!result.Exists(item => item.TypeId == movie.type_id))
                {
                    result.Add(new MovieTypeResponse()
                    {
                        TypeId = movie.type_id,
                        Name = movie.type_name
                    });
                }
            }

            return result;
        }
        
        private async Task<List<MovieCountryResponse>> GetCountries(List<MovieInDb> movies)
        {
            List<MovieCountryResponse> result = new List<MovieCountryResponse>();

            foreach (var movie in movies)
            {
                if (!result.Exists(item => item.CountryId == movie.country_id))
                {
                    result.Add(new MovieCountryResponse()
                    {
                        CountryId = movie.country_id,
                        Name = movie.country_name
                    });
                }
            }

            return result;
        }
        
        private async Task<List<MovieTeamResponse>> GetTeam(List<MovieInDb> movies)
        {
            List<MovieTeamResponse> result = new List<MovieTeamResponse>();

            foreach (var movie in movies)
            {
                if (!result.Exists(item => item.PersonId == movie.person_id && item.RoleName == movie.role_name))
                {
                    result.Add(new MovieTeamResponse()
                    {
                        PersonId = movie.person_id,
                        Name = movie.actor_name,
                        CharacterName = movie.character_name,
                        RoleName = movie.role_name,
                        RoleId = movie.role_id
                    });
                }
            }

            return result;
        }

        private class MovieInDb
        {
            public readonly int movie_id;
            public readonly string movie_name;
            public readonly string original_name;
            public readonly string description;
            public readonly int construction_year;
            public readonly int total_minute;
            public readonly string poster_url;
            public readonly bool is_active;
            public readonly DateTime? vision_entry_date;
            public readonly int total_actor_count;
            public readonly int country_id;
            public readonly string country_name;
            public readonly int type_id;
            public readonly string type_name;
            public readonly int person_id;
            public readonly string actor_name;
            public readonly string character_name;
            public readonly string role_name;
            public readonly int role_id;

            public MovieInDb()
            {
            }

            public MovieInDb(int movieId, string movieName, string originalName, string description, int constructionYear, int totalMinute, string posterUrl, bool isActive, DateTime? visionEntryDate, int totalActorCount, int countryId, string countryName, int typeId, string typeName, int actorId, string actorName, string characterName, string roleName, int roleId)
            {
                movie_id = movieId;
                movie_name = movieName;
                original_name = originalName;
                this.description = description;
                construction_year = constructionYear;
                total_minute = totalMinute;
                poster_url = posterUrl;
                is_active = isActive;
                vision_entry_date = visionEntryDate;
                total_actor_count = totalActorCount;
                country_id = countryId;
                country_name = countryName;
                type_id = typeId;
                type_name = typeName;
                person_id = actorId;
                actor_name = actorName;
                character_name = characterName;
                role_name = roleName;
                role_id = roleId;
            }
        }
    }
}