using Twixter.Models.Entities;

namespace Twixter.Models.Dtos.Posts;

public class PostAddRequestDto
{
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
    public string Content { get; set; }
    public string? MediaUrl { get; set; }
    public ICollection<Media> MediaFiles { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
}