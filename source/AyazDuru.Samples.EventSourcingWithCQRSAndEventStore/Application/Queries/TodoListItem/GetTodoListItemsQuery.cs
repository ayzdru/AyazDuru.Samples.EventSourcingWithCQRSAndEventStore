using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Models;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Extensions;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Interfaces;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Queries
{
    public class GetTodoListItemsQuery :  IRequest<List<TodoListItemModel>>
    {       
        public Guid TodoListId { get; set; }
        public GetTodoListItemsQuery(Guid todoListId)
        {
            TodoListId = todoListId;
        }
        public class GetTodoListItemsQueryHandler : IRequestHandler<GetTodoListItemsQuery, List<TodoListItemModel>>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public GetTodoListItemsQueryHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public Task<List<TodoListItemModel>> Handle(GetTodoListItemsQuery request, CancellationToken cancellationToken)
            {
                return _applicationDbContext.TodoListItems.GetByTodoListId(request.TodoListId).Select(q => new TodoListItemModel(q.Id, q.Title, q.Description, q.IsDone)).ToListAsync(cancellationToken);
            }
        }
    }
}
