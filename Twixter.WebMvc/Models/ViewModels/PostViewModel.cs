using Twixter.Models.Dtos.Posts;
namespace Twixter.WebMvc.Models.ViewModels;
public class PostViewModel
{
    public List<PostResponseDto> Posts { get; set; } = new();
    public PostAddRequestDto NewPost { get; set; } = new();
}