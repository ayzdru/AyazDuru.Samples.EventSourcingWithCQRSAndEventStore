using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Data.Configurations
{
    public class TodoListItemConfiguration : BaseConfiguration<TodoListItem>
    {
        public override void Configure(EntityTypeBuilder<TodoListItem> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.Title).HasMaxLength(Constants.TodoListItem.TitleMaximumLength).IsRequired(Constants.TodoListItem.TitleRequired);
            builder.Property(b => b.Description).HasMaxLength(Constants.TodoListItem.DescriptionMaximumLength).IsRequired(Constants.TodoListItem.DescriptionRequired);
            builder.Property(b => b.IsDone).IsRequired(Constants.TodoListItem.IsDoneRequired);
            builder.HasOne(b => b.TodoList).WithMany(b => b.TodoListItems).HasForeignKey(b => b.TodoListId);           
        }
    }
}
