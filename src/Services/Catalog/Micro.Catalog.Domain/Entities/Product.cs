using System.Text.Json.Serialization;
using Micro.Catalog.Domain.Common;

namespace Micro.Catalog.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public double Price { get; set; }
    
    [JsonIgnore]
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}