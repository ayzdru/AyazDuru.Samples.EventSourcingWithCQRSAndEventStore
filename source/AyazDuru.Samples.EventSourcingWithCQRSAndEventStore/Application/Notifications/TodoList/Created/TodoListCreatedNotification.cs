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
    public class TodoListCreatedNotification : INotification
    {       
        public TodoList Model { get; set; }       
        public class TodoListCreatedNotificationHandler : INotificationHandler<TodoListCreatedNotification>
        {
            private readonly IAggregateRepository _aggregateRepository;
            private readonly TodoListAggregate _todoListAggregate;

            public TodoListCreatedNotificationHandler(IAggregateRepository aggregateRepository, TodoListAggregate todoListAggregate)
            {
                _aggregateRepository = aggregateRepository;
                _todoListAggregate = todoListAggregate;
            }

            public async Task Handle(TodoListCreatedNotification notification, CancellationToken cancellationToken)
            {
                _todoListAggregate.SetStreamName($"todolist-{notification.Model.Id}");
                _todoListAggregate.Created(notification.Model);

                await _aggregateRepository.SaveAsync(_todoListAggregate);               
            }
        }
    }
}
