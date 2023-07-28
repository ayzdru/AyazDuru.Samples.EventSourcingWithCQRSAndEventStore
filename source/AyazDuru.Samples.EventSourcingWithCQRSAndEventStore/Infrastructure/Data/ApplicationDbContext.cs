using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Interfaces;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Extensions;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Extensions;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationDbContext
    {
        private readonly IMediator _mediator;
        private readonly EntitySaveChangesInterceptor _entitySaveChangesInterceptor;
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoListItem> TodoListItems { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator, EntitySaveChangesInterceptor entitySaveChangesInterceptor)
             : base(options)
        {
            _mediator = mediator;
            _entitySaveChangesInterceptor = entitySaveChangesInterceptor;
        }      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_entitySaveChangesInterceptor);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchNotifications(this);

            return await base.SaveChangesAsync(cancellationToken);
        }       
    }
}
