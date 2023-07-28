using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoListItem> TodoListItems { get; set; }
        DbSet<TodoList> TodoLists { get; set; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}