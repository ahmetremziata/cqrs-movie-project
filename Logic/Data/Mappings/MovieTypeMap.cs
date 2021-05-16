using Logic.Data.Entities;
using Logic.Data.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.Data.Mappings
{
    public class MovieTypeMap : EntityMappingConfiguration<MovieType>
    {
        public override void Map(EntityTypeBuilder<MovieType> builder)
        {
            builder.ToTable("movie_types");
            builder.Property(r => r.Id).HasColumnName("id");
            builder.Property(r => r.MovieId).HasColumnName("movie_id");
            builder.Property(r => r.TypeId).HasColumnName("type_id");
        }
    }
}