using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.BindingModels
{
    public class DeleteTodoListItemBindingModel
    {
        [Required]
        public Guid? TodoListItemId { get; set; }
    }
}
