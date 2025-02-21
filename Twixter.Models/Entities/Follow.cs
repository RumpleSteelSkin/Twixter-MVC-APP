using CorePackage.Entities;

namespace Twixter.Models.Entities;

public class Follow : Entity<Guid>
{
    public required Guid FollowerId { get; set; }
    public ApplicationUser Follower { get; set; } = null!;

    public required Guid FollowingId { get; set; }
    public ApplicationUser Following { get; set; } = null!;
}