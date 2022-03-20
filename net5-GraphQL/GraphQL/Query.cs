using HotChocolate;
using HotChocolate.Data;
using net5_GraphQL.Data;
using net5_GraphQL.Models;
using System.Linq;

namespace net5_GraphQL.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))] // To use multi threaded db context for execution parallel queries 
        [UseFiltering]
        [UseSorting]
        //[UseProjection] To get parent/childs, resolved explicitly in PlatformType.cs
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext dbContext)
        {
            return dbContext.Platforms;
        }

        [UseDbContext(typeof(AppDbContext))] // To use more db context for parallel query execution
        [UseFiltering]
        [UseSorting]
        //[UseProjection] To get parent/childs, resolved explicitly in CommandType.cs
        public IQueryable<Command> GetCommand([ScopedService] AppDbContext dbContext)
        {
            return dbContext.Commands;
        }
    }
}
