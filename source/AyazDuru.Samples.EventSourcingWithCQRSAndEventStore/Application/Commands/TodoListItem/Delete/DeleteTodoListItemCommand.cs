using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Extensions;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Interfaces;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Notifications;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Commands
{    
    public class DeleteTodoListItemCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public DeleteTodoListItemCommand(Guid id)
        {
            Id = id;
        }    

        public class DeleteTodoListItemCommandHandler : IRequestHandler<DeleteTodoListItemCommand, int>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMediator _mediator;
            public DeleteTodoListItemCommandHandler(IApplicationDbContext applicationDbContext, IMediator mediator)
            {
                _applicationDbContext = applicationDbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(DeleteTodoListItemCommand request, CancellationToken cancellationToken)
            {
                var affected = await _applicationDbContext.TodoListItems.GetById(request.Id).ExecuteDeleteAsync(cancellationToken);

                if (affected == 0)
                {
                    throw new NotFoundException(nameof(TodoList), request.Id);
                }
                await _mediator.Publish(new TodoListItemDeletedNotification { TodoListItemId = request.Id }, cancellationToken);
                return affected;
            }
        }
    }
}
