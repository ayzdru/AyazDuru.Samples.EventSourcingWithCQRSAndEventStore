using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Models
{
    public record TodoListItemModel
    {       
        public Guid TodoListItemId { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public bool IsDone { get; init; }
        public TodoListItemModel(Guid todoListItemId, string title, string description, bool isDone)
        {
            TodoListItemId = todoListItemId;
            Title = title;
            Description = description;
            IsDone = isDone;
        }
    }
}
