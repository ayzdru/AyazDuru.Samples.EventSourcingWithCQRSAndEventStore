using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Interfaces;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Models;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Queries
{
    public class GetTodoListsQuery :  IRequest<List<TodoListModel>>
    {
        public class GetTodoListsQueryHandler : IRequestHandler<GetTodoListsQuery, List<TodoListModel>>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public GetTodoListsQueryHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public Task<List<TodoListModel>> Handle(GetTodoListsQuery request, CancellationToken cancellationToken)
            {
                return _applicationDbContext.TodoLists.Select(q => new TodoListModel(q.Id, q.Title, q.TodoListItems.Count(), q.Colour.Code)).ToListAsync(cancellationToken);
            }
        }
    }
}
