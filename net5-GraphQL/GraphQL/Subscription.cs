using HotChocolate;
using HotChocolate.Types;
using net5_GraphQL.Models;

namespace net5_GraphQL.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public Platform OnPlatformAdded([EventMessage] Platform platform)
        {
            return platform;
        }
    }
}
