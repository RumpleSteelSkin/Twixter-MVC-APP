using CorePackage.Entities;

namespace Twixter.Models.Entities;

public class Like : Entity<Guid>
{
    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;

    public required Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
}