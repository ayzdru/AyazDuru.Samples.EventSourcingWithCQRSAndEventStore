using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Extensions;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Interfaces;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Notifications;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Data;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Commands
{    
    public class UpdateTodoListCommand : IRequest<int>
    {        

        public Guid Id { get; set; }
        public string Title { get; set; }
        public UpdateTodoListCommand(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand, int>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMediator _mediator;
            public UpdateTodoListCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMediator mediator)
            {
                _applicationDbContext = applicationDbContext;
                _currentUserService = currentUserService;
                _mediator = mediator;
            }

            public async Task<int> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
            {
                var affected = await _applicationDbContext.TodoLists.GetById(request.Id).ExecuteUpdateAsync(setters => setters.SetProperty(b => b.Title, request.Title).SetProperty(b=> b.LastModifiedByUserId, _currentUserService.UserId).SetProperty(b=> b.LastModified , DateTime.Now));

                if (affected == 0)
                {
                    throw new NotFoundException(nameof(TodoList), request.Id);
                }
                await _mediator.Publish(new TodoListUpdatedNotification { TodoListId = request.Id, Title = request.Title }, cancellationToken);
                return affected;
            }
        }
    }
}
