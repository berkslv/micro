using Micro.Catalog.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace Micro.Catalog.Domain.Views;

[BsonIgnoreExtraElements]
public class ProductView : BaseView
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public double Price { get; set; }
    
    public ICollection<string> CategoryIds { get; set; } = new List<string>();
}