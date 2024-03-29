using AutoMapper;
using AutoMapper.QueryableExtensions;
using Micro.Catalog.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Micro.Catalog.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> AsPaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);
    
    public static PaginatedList<TDestination> AsPaginatedList<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.Create(queryable.AsNoTracking(), pageNumber, pageSize);
    
    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();   
}