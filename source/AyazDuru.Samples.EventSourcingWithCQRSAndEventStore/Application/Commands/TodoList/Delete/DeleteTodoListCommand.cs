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
    public class DeleteTodoListCommand : IRequest<int>
    {
        public DeleteTodoListCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }


        public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand, int>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMediator _mediator;
            public DeleteTodoListCommandHandler(IApplicationDbContext applicationDbContext, IMediator mediator)
            {
                _applicationDbContext = applicationDbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
            {
                var affected = await _applicationDbContext.TodoLists.GetById(request.Id).ExecuteDeleteAsync(cancellationToken);

                if (affected == 0)
                {
                    throw new NotFoundException(nameof(TodoList), request.Id);
                }
                await _mediator.Publish(new TodoListDeletedNotification { TodoListId = request.Id}, cancellationToken);
                return affected;
            }
        }
    }
}
