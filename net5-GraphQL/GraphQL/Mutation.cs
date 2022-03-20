using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using net5_GraphQL.Data;
using net5_GraphQL.GraphQL.Commands;
using net5_GraphQL.GraphQL.Platforms;
using net5_GraphQL.Models;
using System.Threading;
using System.Threading.Tasks;

namespace net5_GraphQL.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(
            AddPlatformInput input,
            [ScopedService] AppDbContext dbContext,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken
            )
        {
            var platform = new Platform
            {
                Name = input.Name,
            };

            dbContext.Platforms.Add(platform);
            await dbContext.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);

            return new AddPlatformPayload(platform);
        }
        
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input,
            [ScopedService] AppDbContext dbContext)
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId,
            };

            dbContext.Commands.Add(command);
            await dbContext.SaveChangesAsync();

            return new AddCommandPayload(command);

        }
    }
}
