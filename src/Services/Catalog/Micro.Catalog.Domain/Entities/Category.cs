using Micro.Catalog.Domain.Common;

namespace Micro.Catalog.Domain.Entities;

public class Category : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}