using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.BindingModels
{
    public class UpdateTodoListBindingModel
    {
        [Required]
        public Guid? TodoListId { get; set; }
        [Required]
        [StringLength(Constants.TodoList.TitleMaximumLength,MinimumLength = Constants.TodoList.TitleMinimumLength)]
        public string Title { get; set; }
    }
}
