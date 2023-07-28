using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Common;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;
using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Repositories
{
    public class AggregateRepository : IAggregateRepository
    {
        readonly IEventStoreConnection _connection;
        public AggregateRepository(IEventStoreConnection connection)
            => _connection = connection;
        public async Task SaveAsync<T>(T aggregate) where T : BaseAggregate, new()
        {
            List<EventData> events = aggregate.GetEvents
                .Select(@event => new EventData(
                    eventId: Guid.NewGuid(),
                    type: @event.GetType().Name,
                    isJson: true,
                    data: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(
                        value: @event,
                        inputType: @event.GetType(),
                        new JsonSerializerOptions() { WriteIndented = true }
                        )),
                    metadata: Encoding.UTF8.GetBytes(@event.GetType().FullName))
                )
                .ToList();

            if (!events.Any())
                return;

            await _connection.AppendToStreamAsync(aggregate.StreamName, ExpectedVersion.Any, events);
            aggregate.GetEvents.Clear();
        }
        public async Task<dynamic> GetEvents(string streamName)
        {
            long nextSliceStart = 0L;
            List<ResolvedEvent> events = new();
            StreamEventsSlice readEvents = null;
            do
            {
                readEvents = await _connection.ReadStreamEventsForwardAsync(
                    stream: streamName,
                    start: nextSliceStart,
                    count: 4096,
                    resolveLinkTos: true
                    );

                if (readEvents.Events.Length > 0)
                    events.AddRange(readEvents.Events);

                nextSliceStart = readEvents.NextEventNumber;
            } while (!readEvents.IsEndOfStream);
            return events.Select(@event => new
            {
                @event.Event.EventNumber,
                @event.Event.EventType,
                @event.Event.Created,
                @event.Event.EventId,
                @event.Event.EventStreamId,
                Data = JsonSerializer.Deserialize(
                    json: Encoding.UTF8.GetString(@event.Event.Data),
                    returnType: Type.GetType(Encoding.UTF8.GetString(@event.Event.Metadata)) //returnType : Yukarıda 'SaveAsync' metodunda metadata olarak tutulan event class'ının tam adı, burada ilgili event'in özgün sınıfına dönüştürülürken kullanılmaktadır.
                    ),
                Metadata = Encoding.UTF8.GetString(@event.Event.Metadata)
            });
        }
    }
}
