using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Aggregates;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Notifications
{
    public class TodoListItemDeletedNotification : INotification
    {       
        public Guid TodoListItemId { get; set; }
        public class TodoListItemDeletedNotificationHandler : INotificationHandler<TodoListItemDeletedNotification>
        {
            private readonly IAggregateRepository _aggregateRepository;
            private readonly TodoListItemAggregate _todoListItemAggregate;

            public TodoListItemDeletedNotificationHandler(IAggregateRepository aggregateRepository, TodoListItemAggregate todoListItemAggregate)
            {
                _aggregateRepository = aggregateRepository;
                _todoListItemAggregate = todoListItemAggregate;
            }

            public async Task Handle(TodoListItemDeletedNotification notification, CancellationToken cancellationToken)
            {
                _todoListItemAggregate.SetStreamName($"todolistitem-{notification.TodoListItemId}");
                _todoListItemAggregate.Deleted(notification.TodoListItemId);

                await _aggregateRepository.SaveAsync(_todoListItemAggregate);               
            }
        }
    }
}
