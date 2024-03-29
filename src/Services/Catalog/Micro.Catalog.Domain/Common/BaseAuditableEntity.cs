namespace Micro.Catalog.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public long Created { get; set; }

    public string? CreatedBy { get; set; }

    public long? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}