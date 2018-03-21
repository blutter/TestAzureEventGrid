using Microsoft.Azure.EventGrid.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.EventGrid;

namespace Test.Azure
{
    /// <summary>Extension methods for EventGridClient.</summary>
    public static class EventGridClientExtensions
    {
        /// <summary>
        /// Publishes a batch of events to an Azure Event Grid topic.
        /// </summary>
        /// <param name="operations">
        /// The operations group for this extension method.
        /// </param>
        /// <param name="topicHostname">
        /// The host name of the topic, e.g. topic1.westus2-1.eventgrid.azure.net
        /// </param>
        /// <param name="events">
        /// An array of events to be published to Event Grid.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static async Task PublishEventsAsync<T>(this IEventGridClient operations, string topicHostname, T data, CancellationToken cancellationToken = default(CancellationToken))
        {
            (await operations.PublishEventsWithHttpMessagesAsync(topicHostname, events, (Dictionary<string, List<string>>)null, cancellationToken).ConfigureAwait(false)).Dispose();
        }
    }
}
