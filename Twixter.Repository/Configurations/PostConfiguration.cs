using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Twixter.Models.Entities;

namespace Twixter.Repository.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(x => x.Content).IsRequired();
        builder.HasMany(x => x.MediaFiles)
            .WithOne(x => x.Post)
            .HasForeignKey(x => x.PostId);
    }
}