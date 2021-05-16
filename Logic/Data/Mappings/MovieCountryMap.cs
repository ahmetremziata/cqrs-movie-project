using Logic.Data.Entities;
using Logic.Data.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.Data.Mappings
{
    public class MovieCountryMap : EntityMappingConfiguration<MovieCountry>
    {
        public override void Map(EntityTypeBuilder<MovieCountry> builder)
        {
            builder.ToTable("movie_countries");
            builder.Property(r => r.Id).HasColumnName("id");
            builder.Property(r => r.MovieId).HasColumnName("movie_id");
            builder.Property(r => r.CountryId).HasColumnName("country_id");
        }
    }
}