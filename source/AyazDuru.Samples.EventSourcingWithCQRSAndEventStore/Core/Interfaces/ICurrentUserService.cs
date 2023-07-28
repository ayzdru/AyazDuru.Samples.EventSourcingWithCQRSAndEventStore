using System;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;

public interface ICurrentUserService
{
    Guid? UserId { get; }
}
