namespace CorePackage.Entities;

public abstract class Entity<TId>
{
    public required TId Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}