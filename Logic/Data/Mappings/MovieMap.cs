using Logic.Data.Entities;
using Logic.Data.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.Data.Mappings
{
    public class MovieMap : EntityMappingConfiguration<Movie>
    {
        public override void Map(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("movies");
            builder.Property(r => r.Id).HasColumnName("id");
            builder.Property(r => r.Name).HasColumnName("name");
            builder.Property(r => r.OriginalName).HasColumnName("original_name");
            builder.Property(r => r.Description).HasColumnName("description");
            builder.Property(r => r.ConstructionYear).HasColumnName("construction_year");
            builder.Property(r => r.TotalMinute).HasColumnName("total_minute");
            builder.Property(r => r.PosterUrl).HasColumnName("poster_url");
            builder.Property(r => r.VisionEntryDate).HasColumnName("vision_entry_date");
            builder.Property(r => r.CreatedOn).HasColumnName("created_on");
            builder.Property(r => r.IsActive).HasColumnName("is_active");
            builder.Property(r => r.IsSynchronized).HasColumnName("is_synchronized");
        }
    }
}