namespace CleanCli.Todo.Application.Common.Mappings
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CleanCli.Todo.Application.Common.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
            this IQueryable<TDestination> queryable, int pageNumber, int pageSize) =>
                PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(
            this IQueryable queryable, IConfigurationProvider configuration) =>
                queryable.ProjectTo<TDestination>(configuration).ToListAsync();
    }
}
