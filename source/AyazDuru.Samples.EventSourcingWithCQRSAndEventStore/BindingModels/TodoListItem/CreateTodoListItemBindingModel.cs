using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.BindingModels
{
    public class CreateTodoListItemBindingModel
    {
        [Required]
        public Guid? TodoListId { get; set; }
        [Required]
        [StringLength(Constants.TodoListItem.TitleMaximumLength,MinimumLength = Constants.TodoListItem.TitleMinimumLength)]
        public string Title { get; set; }
        [Required]
        [StringLength(Constants.TodoListItem.DescriptionMaximumLength, MinimumLength = Constants.TodoListItem.DescriptionMinimumLength)]
        public string Description { get; set; }
    }
}
