using System;
using System.Collections.Generic;
using System.Text;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities
{
    public class TodoListItem : BaseEntity
    {     
        public TodoList TodoList { get; private set; }
        public Guid TodoListId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsDone { get; private set; }
       
        public TodoListItem(Guid todoListId, string title, string description, bool isDone)
        {
            TodoListId = todoListId;
            Title = title;
            Description = description;
            IsDone = isDone;
        }
        public TodoListItem(string title, string description)
        {          
            Title = title;
            Description = description;
        }
       
    }
}
