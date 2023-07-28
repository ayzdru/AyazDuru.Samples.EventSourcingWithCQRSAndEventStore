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
    public class TodoListDeletedNotification : INotification
    {       
        public Guid TodoListId { get; set; }
        public class TodoListDeletedNotificationHandler : INotificationHandler<TodoListDeletedNotification>
        {
            private readonly IAggregateRepository _aggregateRepository;
            private readonly TodoListAggregate _todoListAggregate;

            public TodoListDeletedNotificationHandler(IAggregateRepository aggregateRepository, TodoListAggregate todoListAggregate)
            {
                _aggregateRepository = aggregateRepository;
                _todoListAggregate = todoListAggregate;
            }

            public async Task Handle(TodoListDeletedNotification notification, CancellationToken cancellationToken)
            {
                _todoListAggregate.SetStreamName($"todolist-{notification.TodoListId}");
                _todoListAggregate.Deleted(notification.TodoListId);

                await _aggregateRepository.SaveAsync(_todoListAggregate);               
            }
        }
    }
}
