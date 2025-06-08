namespace Shared.Common.Models;

public abstract class EntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }

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

