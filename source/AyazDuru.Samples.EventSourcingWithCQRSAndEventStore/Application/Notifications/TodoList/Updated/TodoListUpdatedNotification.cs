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
    public class TodoListUpdatedNotification : INotification
    {       
        public Guid TodoListId { get; set; }
        public string Title { get; set; }
        public class TodoListUpdatedNotificationHandler : INotificationHandler<TodoListUpdatedNotification>
        {
            private readonly IAggregateRepository _aggregateRepository;
            private readonly TodoListAggregate _todoListAggregate;

            public TodoListUpdatedNotificationHandler(IAggregateRepository aggregateRepository, TodoListAggregate todoListAggregate)
            {
                _aggregateRepository = aggregateRepository;
                _todoListAggregate = todoListAggregate;
            }

            public async Task Handle(TodoListUpdatedNotification notification, CancellationToken cancellationToken)
            {
                _todoListAggregate.SetStreamName($"todolist-{notification.TodoListId}");
                _todoListAggregate.TitleChanged(notification.TodoListId, notification.Title);

                await _aggregateRepository.SaveAsync(_todoListAggregate);               
            }
        }
    }
}
