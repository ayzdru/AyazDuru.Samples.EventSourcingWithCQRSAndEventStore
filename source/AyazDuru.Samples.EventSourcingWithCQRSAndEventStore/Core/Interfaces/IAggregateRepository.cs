using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Common;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces
{
    public interface IAggregateRepository
    {
        Task<dynamic> GetEvents(string streamName);
        Task SaveAsync<T>(T aggregate) where T : BaseAggregate, new();
    }
}