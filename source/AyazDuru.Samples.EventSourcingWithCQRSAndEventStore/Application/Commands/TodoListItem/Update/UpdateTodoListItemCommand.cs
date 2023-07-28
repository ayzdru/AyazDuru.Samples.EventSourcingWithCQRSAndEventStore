using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Extensions;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Interfaces;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Notifications;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Exceptions;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Commands
{    
    public class UpdateTodoListItemCommand : IRequest<int>
    {        

        public Guid Id { get; set; }
        public bool IsDone { get; set; }
        public UpdateTodoListItemCommand(Guid id, bool isDone)
        {
            Id = id;
            IsDone = isDone;
        }

        public class UpdateTodoListItemCommandHandler : IRequestHandler<UpdateTodoListItemCommand, int>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly ICurrentUserService _currentUserService; 
            private readonly IMediator _mediator;
            public UpdateTodoListItemCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMediator mediator)
            {
                _applicationDbContext = applicationDbContext;
                _currentUserService = currentUserService;
                _mediator = mediator;
            }

            public async Task<int> Handle(UpdateTodoListItemCommand request, CancellationToken cancellationToken)
            {                
                var affected = await _applicationDbContext.TodoListItems.GetById(request.Id).ExecuteUpdateAsync(setters => setters.SetProperty(b => b.IsDone, request.IsDone).SetProperty(b => b.LastModifiedByUserId, _currentUserService.UserId).SetProperty(b => b.LastModified, DateTime.Now));

                if (affected == 0)
                {
                    throw new NotFoundException(nameof(TodoList), request.Id);
                }
                await _mediator.Publish(new TodoListItemUpdatedNotification { TodoListItemId = request.Id, IsDone = request.IsDone }, cancellationToken);
                return affected;
            }
        }
    }
}
