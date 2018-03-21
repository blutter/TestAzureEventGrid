using System;
using System.Collections;
using EventGridPublisher.Sample;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Rest;

namespace EventGridPublisher
{
    class Program
    {
        // The custom topic host name
        // Example: {topicname}.westus2-1.eventgrid.azure.net
        // https://bltexamplecustomtopic.westus2-1.eventgrid.azure.net/api/events
        private const string TopicHostName = "bltexamplecustomtopic.westus2-1.eventgrid.azure.net";

        // Custom topic access key
        private const string TopicKey = "";

        private static void Main(string[] args)
        {
            PublishEvents().Wait();
        }

        private static async Task PublishEvents()
        {
            // Create service credential with the topic credential
            // class and custom topic access key
            ServiceClientCredentials credentials =
                           new TopicCredentials(TopicKey);

            IEventGridClient client = new EventGridClient(credentials);

            var events = GetEvents();

            await client.PublishEventsAsync(
                TopicHostName,
                events).ConfigureAwait(false);
        }

        private static IList<EventGridEvent> GetEvents()
        {
            var events = new List<EventGridEvent>
            {
                new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    Data = new MusicianAdded
                    {
                        Name = "Eddie Van Halen",
                        Instruments = "Guitar, Keyboards"
                    },
                    EventTime = DateTime.Now,
                    EventType = "EventGrid.Sample.MusicianAdded",
                    Subject = "Musicians",
                    DataVersion = "1.1"
                },
                new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    Data = new MusicianAdded
                    {
                        Name = "Slash",
                        Instruments = "Guitar"
                    },
                    EventTime = DateTime.Now,
                    EventType = "EventGrid.Sample.MusicianAdded",
                    Subject = "Musicians",
                    DataVersion = "1.2"
                }
            };

            var @event = new AzureEventGridEvent<MusicianAdded>(new MusicianAdded { Name = "U2", Instruments = "GeeTar"}, "New muso");

            events.Add(@event);

            return events;
        }
    }
}
