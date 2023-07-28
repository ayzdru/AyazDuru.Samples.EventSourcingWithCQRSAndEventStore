using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.BindingModels
{
    public class GetTodoListItemsBindingModel
    {
        [Required]
        public Guid? TodoListId { get; set; }
    }
}
