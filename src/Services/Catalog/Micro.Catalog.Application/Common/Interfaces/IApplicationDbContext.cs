using Micro.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Micro.Catalog.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}