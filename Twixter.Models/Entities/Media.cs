using CorePackage.Entities;
using Twixter.Models.Enums;

namespace Twixter.Models.Entities;

public class Media : Entity<Guid>
{
    public string Url { get; set; } = string.Empty;
    public MediaType Type { get; set; }

    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;
}