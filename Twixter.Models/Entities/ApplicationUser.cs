using Microsoft.AspNetCore.Identity;
namespace Twixter.Models.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Follow> Followers { get; set; } = new List<Follow>();
    public ICollection<Follow> Following { get; set; } = new List<Follow>();
}