namespace Micro.Catalog.Domain.Common;

public abstract class BaseView
{
    public string Id { get; set; } = null!;
    
    public long? LastModified { get; set; }

}