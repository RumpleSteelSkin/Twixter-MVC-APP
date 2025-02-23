namespace Twixter.Models.Dtos.Comments;

public class CommentAddRequestDto
{
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    
    public string? CreatedBy { get; set; }
    public string Content { get; set; } 
    public Guid PostId { get; set; }
    public required Guid UserId { get; set; }
}