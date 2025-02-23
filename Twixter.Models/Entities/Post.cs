using CorePackage.Entities;

namespace Twixter.Models.Entities;

public class Post : Entity<Guid>
{
    public string Content { get; set; } = string.Empty;
    public string? MediaUrl { get; set; }
    public ICollection<Media> MediaFiles { get; set; } = new List<Media>();
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
}