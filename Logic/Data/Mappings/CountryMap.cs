using Logic.Data.Entities;
using Logic.Data.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.Data.Mappings
{
    public class CountryMap : EntityMappingConfiguration<Country>
    {
        public override void Map(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("countries");
            builder.Property(r => r.Id).HasColumnName("id");
            builder.Property(r => r.Name).HasColumnName("name");
            builder.Property(r => r.CreatedOn).HasColumnName("created_on");
        }
    }
}