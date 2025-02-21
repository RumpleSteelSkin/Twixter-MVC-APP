using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Twixter.Models.Entities;

namespace Twixter.Repository.Configurations;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.Property(x => x.Url).IsRequired().HasMaxLength(500);
    }
}