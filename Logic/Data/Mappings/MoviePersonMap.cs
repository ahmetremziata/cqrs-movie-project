using Logic.Data.Entities;
using Logic.Data.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.Data.Mappings
{
    public class MoviePersonMap : EntityMappingConfiguration<MoviePerson>
    {
        public override void Map(EntityTypeBuilder<MoviePerson> builder)
        {
            builder.ToTable("movie_persons");
            builder.Property(r => r.Id).HasColumnName("id");
            builder.Property(r => r.MovieId).HasColumnName("movie_id");
            builder.Property(r => r.PersonId).HasColumnName("person_id");
            builder.Property(r => r.RoleId).HasColumnName("role_id");
            builder.Property(r => r.CharacterName).HasColumnName("character_name");
        }
    }
}