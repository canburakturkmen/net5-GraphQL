using HotChocolate;
using HotChocolate.Types;
using net5_GraphQL.Data;
using net5_GraphQL.Models;
using System.Linq;

namespace net5_GraphQL.GraphQL.Commands
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("Represents any executable command.");

            descriptor
                .Field(c => c.Platform)
                .ResolveWith<Resolvers>(c => c.GetPlatform(default!, default))
                .UseDbContext<AppDbContext>()
                .Description("This is the platform to which the command belongs.");

            
        }

        private class Resolvers
        {
            public Platform GetPlatform([Parent]Command command, [ScopedService] AppDbContext dbContext)
            {
                return dbContext.Platforms.FirstOrDefault(p => p.Id == command.PlatformId);
            }
        }
    }
}
