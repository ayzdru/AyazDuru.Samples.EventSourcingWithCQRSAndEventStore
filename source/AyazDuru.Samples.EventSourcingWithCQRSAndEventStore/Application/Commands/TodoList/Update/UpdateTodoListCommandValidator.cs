using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Commands
{
    public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
    {
        public UpdateTodoListCommandValidator()
        {
            RuleFor(v => v.Title).MaximumLength(Constants.TodoList.TitleMaximumLength).MinimumLength(Constants.TodoList.TitleMinimumLength).NotEmpty();
        }
    }
}
