using Micro.Catalog.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace Micro.Catalog.Domain.Views;

[BsonIgnoreExtraElements]
public class CategoryView : BaseView
{
    public string Name { get; set; } = null!;

    public ICollection<string> ProductIds { get; set; } = new List<string>();
}