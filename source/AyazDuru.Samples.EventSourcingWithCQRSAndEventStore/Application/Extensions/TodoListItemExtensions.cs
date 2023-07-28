using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Extensions
{
    public static class TodoListItemExtensions
    {
        public static IQueryable<TodoListItem> GetByTodoListId(this DbSet<TodoListItem> dbSet, Guid todoListId)
        {
            return dbSet.Where(q => q.TodoListId == todoListId);
        }
    }
}
