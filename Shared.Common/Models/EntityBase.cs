namespace Shared.Common.Models;

public abstract class EntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    // We don't really need this for this demo, but in real-world applications,
    // it's common to track when an entity was created, for various reason, debugging or customer support.
    public DateTime CreatedDate { get; set; } 
    public DateTime ModifiedDate { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid ModifiedBy { get; set; }

    public void SetCreated(Guid userId)
    {
        CreatedBy = userId;
        CreatedDate = DateTime.UtcNow; 
    }

    public void SetModified(Guid userId) 
    { 
        ModifiedBy = userId; 
        ModifiedDate = DateTime.UtcNow;
    }
}

