using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities
{
    public class TodoList : BaseEntity
    {
        public string Title { get; private set; }
        public Colour Colour { get; set; } = Colour.White;
        private readonly List<TodoListItem>  _todoListItems = new List<TodoListItem>();
        public IReadOnlyCollection<TodoListItem> TodoListItems => _todoListItems.AsReadOnly();
        public TodoList(string title)
        {
            Title = title;
        }
        public TodoList(string title, Colour colour )
        {
            Title = title; 
            Colour = colour;
        }        
        public void Add(TodoListItem item)
        {
            _todoListItems.Add(item);
        }
        public void UpdateColour(Colour colour)
        {
            Colour = colour;
        }
    }
}
