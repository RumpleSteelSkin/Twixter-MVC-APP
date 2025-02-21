using CorePackage.Entities;

namespace Twixter.Models.Entities;

public class Comment : Entity<Guid>
{
    public string Content { get; set; } = string.Empty;

    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;

    public required Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
}