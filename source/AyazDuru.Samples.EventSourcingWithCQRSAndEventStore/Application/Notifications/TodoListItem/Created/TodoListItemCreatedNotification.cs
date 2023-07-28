using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Aggregates;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Notifications
{
    public class TodoListItemCreatedNotification : INotification
    {
        public TodoListItem Model { get; set; }
        public class TodoListItemCreatedNotificationHandler : INotificationHandler<TodoListItemCreatedNotification>
        {
            private readonly IAggregateRepository _aggregateRepository;
            private readonly TodoListItemAggregate _todoListItemAggregate;

            public TodoListItemCreatedNotificationHandler(IAggregateRepository aggregateRepository, TodoListItemAggregate todoListItemAggregate)
            {
                _aggregateRepository = aggregateRepository;
                _todoListItemAggregate = todoListItemAggregate;
            }

            public async Task Handle(TodoListItemCreatedNotification notification, CancellationToken cancellationToken)
            {
                _todoListItemAggregate.SetStreamName($"todolistitem-{notification.Model.Id}");
                _todoListItemAggregate.Created(notification.Model);

                await _aggregateRepository.SaveAsync(_todoListItemAggregate);
            }
        }
    }
}
