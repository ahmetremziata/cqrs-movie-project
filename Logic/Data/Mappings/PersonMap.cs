using Logic.Data.Entities;
using Logic.Data.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.Data.Mappings
{
    public class PersonMap : EntityMappingConfiguration<Person>
    {
        public override void Map(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("persons");
            builder.Property(r => r.Id).HasColumnName("id");
            builder.Property(r => r.Name).HasColumnName("name");
            builder.Property(r => r.RealName).HasColumnName("real_name");
            builder.Property(r => r.Biography).HasColumnName("biography");
            builder.Property(r => r.AvatarUrl).HasColumnName("avatar_url");
            builder.Property(r => r.BirthDate).HasColumnName("birth_date");
            builder.Property(r => r.DeathDate).HasColumnName("death_date");
            builder.Property(r => r.BirthPlace).HasColumnName("birth_place");
            builder.Property(r => r.DeathPlace).HasColumnName("death_place");
            builder.Property(r => r.CreatedOn).HasColumnName("created_on");
        }
    }
}