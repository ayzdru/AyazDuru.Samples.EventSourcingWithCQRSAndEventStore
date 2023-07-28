using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Models
{
    public record TodoListModel
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public int ItemsCount { get; init; }
        public string ColourCode { get; init; }
        public TodoListModel(Guid id, string title, int itemsCount, string colourCode)
        {
            Id = id;
            Title = title;
            ItemsCount = itemsCount;
            ColourCode = colourCode;
        }
    }
}
