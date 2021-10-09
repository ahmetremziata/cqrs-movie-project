using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Logic.Responses;
using Logic.Utils;
using Npgsql;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetMovieListByFilterQueryHandler : IQueryHandler<GetMovieListByFilterQuery, List<MovieResponse>>
    {
        private readonly ConnectionString _connection;
        private const int TURKEY_COUNTRY_ID = 1;

        public GetMovieListByFilterQueryHandler(ConnectionString connection)
        {
            _connection = connection;
        }

        public async Task<List<MovieResponse>> Handle(GetMovieListByFilterQuery query)
        {
            List<FilteredMovie> moviesInDb = new List<FilteredMovie>();
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT
                   DISTINCT
                   m.id movie_id,
                   m.name movie_name,
                   m.original_name,
                   m.construction_year,
                   m.total_minute,
                   m.is_active,
                   m.is_synchronized
            FROM movies m
            left join movie_persons mp on m.id = mp.movie_id
            left join movie_countries c on c.movie_id = m.id
            left join movie_types t on t.movie_id = m.id
            left join persons p on mp.person_id = p.id
            where
                1 = 1
            ");

            if (!String.IsNullOrWhiteSpace(query.MovieName))
            {
                sql.Append($" and (m.name like '%{query.MovieName}%'or m.original_name like '%{query.MovieName}%')");
            }
            
            if (!String.IsNullOrWhiteSpace(query.ActorName))
            {
                sql.Append($" and and p.name like '%{query.ActorName}%'");
            }
            
            if (query.ConstructionYear != null)
            {
                sql.Append($" and m.construction_year={query.ConstructionYear}");
            }
            
            if (query.TypeId != null)
            {
                sql.Append($" and t.type_id ={query.TypeId}");
            }
            
            if (query.CountryId != null)
            {
                sql.Append($" and c.country_id={query.CountryId}");
            }
            
            if (query.IsDomestic == true)
            {
                sql.Append($" and c.country_id IN ({TURKEY_COUNTRY_ID})");
            }
            
            if (query.IsInternational == true)
            {
                sql.Append($" and c.country_id NOT IN ({TURKEY_COUNTRY_ID})");
            }

            sql.Append(" order by m.id");
            
            using (NpgsqlConnection connection = new NpgsqlConnection(_connection.Value))
            {
                moviesInDb = connection
                    .Query<FilteredMovie>(sql.ToString()
                    ).ToList();
            }


            if (!moviesInDb.Any())
            {
                return new List<MovieResponse>();
            }

            List<MovieResponse> result = new List<MovieResponse>();

            foreach (var movieInDb in moviesInDb)
            {
                result.Add(ConvertToDto(movieInDb));
            }

            return result;
        }

        private MovieResponse ConvertToDto(FilteredMovie movie)
        {
            return new MovieResponse
            {
                Id = movie.movie_id,
                Name = movie.movie_name,
                OriginalName = movie.original_name,
                ConstructionYear = movie.construction_year,
                TotalMinute = movie.total_minute,
                IsActive = movie.is_active,
                IsSynchronized = movie.is_synchronized
            };
        }
        
        private class FilteredMovie
        {
            public readonly int movie_id;
            public readonly string movie_name;
            public readonly string original_name;
            public readonly int construction_year;
            public readonly int total_minute;
            public readonly bool is_active;
            public readonly bool is_synchronized;

            public FilteredMovie()
            {
            }

            public FilteredMovie(int movieId, string movieName, string originalName, int constructionYear, int totalMinute, bool isActive, bool isSynchronized)
            {
                movie_id = movieId;
                movie_name = movieName;
                original_name = originalName;
                construction_year = constructionYear;
                total_minute = totalMinute;
                is_active = isActive;
                is_synchronized = isSynchronized;
            }
        }
    }
}