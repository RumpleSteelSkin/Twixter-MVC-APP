namespace Twixter.Models.Dtos.Comments;

public class CommentResponseDto
{
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
    public string Content { get; set; }
    public string UserName { get; set; }
}