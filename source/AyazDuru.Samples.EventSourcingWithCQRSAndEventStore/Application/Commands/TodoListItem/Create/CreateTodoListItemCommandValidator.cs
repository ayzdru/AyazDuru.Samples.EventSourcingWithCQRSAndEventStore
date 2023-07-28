using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Commands
{
    public class CreateTodoListItemCommandValidator : AbstractValidator<CreateTodoListItemCommand>
    {
        public CreateTodoListItemCommandValidator()
        {
            RuleFor(v => v.TodoListItem.Title).MaximumLength(Constants.TodoListItem.TitleMaximumLength).MinimumLength(Constants.TodoListItem.TitleMinimumLength).NotEmpty();
            RuleFor(v => v.TodoListItem.Description).MaximumLength(Constants.TodoListItem.DescriptionMaximumLength).MinimumLength(Constants.TodoListItem.DescriptionMinimumLength).NotEmpty();
        }
    }
}
