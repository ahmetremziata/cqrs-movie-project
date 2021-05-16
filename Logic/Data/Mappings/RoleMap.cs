using Logic.Data.Entities;
using Logic.Data.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.Data.Mappings
{
    public class RoleMap : EntityMappingConfiguration<Role>
    {
        public override void Map(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");
            builder.Property(r => r.Id).HasColumnName("id");
            builder.Property(r => r.Name).HasColumnName("name");
            builder.Property(r => r.CreatedOn).HasColumnName("created_on");
        }
    }
}