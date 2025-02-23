using Twixter.Models.Entities;

namespace Twixter.Models.Dtos.Posts;

public class PostResponseDto
{
    public DateTime CreatedDate { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? MediaUrl { get; set; }
    public ICollection<Media> MediaFiles { get; set; } = new List<Media>();
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
}