using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using System.Threading;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Notifications;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Interfaces;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Commands
{
    public class CreateTodoListCommand : IRequest<Guid?>
    {
        public TodoList TodoList { get; set; }
        public CreateTodoListCommand(TodoList todoList)
        {

            TodoList = todoList;
        }
        public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, Guid?>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMediator _mediator;
            public CreateTodoListCommandHandler(IApplicationDbContext applicationDbContext, IMediator mediator)
            {
                _applicationDbContext = applicationDbContext;
                _mediator = mediator;
            }
            public async Task<Guid?> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
            {
                await using var transaction = await _applicationDbContext.Database.BeginTransactionAsync();
                _applicationDbContext.TodoLists.Add(request.TodoList);                    
                var affected = await _applicationDbContext.SaveChangesAsync(cancellationToken);
                if (affected != 0)
                {
                    await _mediator.Publish(new TodoListCreatedNotification { Model = request.TodoList }, cancellationToken);
                    await transaction.CommitAsync();
                    return request.TodoList.Id;
                }               
                return null;
            }
        }
    }
}
