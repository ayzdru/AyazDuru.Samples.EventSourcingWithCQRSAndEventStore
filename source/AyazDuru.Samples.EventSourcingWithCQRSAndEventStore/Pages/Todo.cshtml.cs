using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Extensions;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.BindingModels;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Commands;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Models;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.ApiModels.Response;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.ValueObjects;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Pages
{
    public class TodoPageModel : BasePageModel
    {      
        public List<TodoListModel> TodoList { get; set; }
        public async Task OnGet()
        {
            TodoList =await Mediator.Send(new GetTodoListsQuery());
        }
        public CreateTodoListBindingModel  CreateTodoListBindingModel { get; set; }
        public async Task<IActionResult> OnPostCreateTodoList(CreateTodoListBindingModel createTodoListBindingModel)
        {
            if(ModelState.IsValid)
            { 
                var randomColour = Colour.SupportedColours.OrderBy(q => Guid.NewGuid()).First();
                var creatTodoListId = await Mediator.Send(new CreateTodoListCommand(new TodoList(createTodoListBindingModel.Title, randomColour)));
                if(creatTodoListId.HasValue)
                {
                    return new JsonResult(new AlertApiModel("Create Todo List", $"{createTodoListBindingModel.Title} created.", new { Id = creatTodoListId.Value, ColourCode = randomColour.Code }));
                }
                else
                {
                    return new JsonResult(new AlertApiModel("Create Todo List", $"The List could not be created."))
                    {

                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }
            return new JsonResult(new AlertApiModel("Create Todo List", ModelState.GetModelStateErrors()))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
        public DeleteTodoListBindingModel  DeleteTodoListBindingModel { get; set; }
        public async Task<IActionResult> OnPostDeleteTodoList(DeleteTodoListBindingModel deleteTodoListBindingModel)
        {
            if (ModelState.IsValid)
            {
                var deleteTodoListCommandAffected = await Mediator.Send(new DeleteTodoListCommand(deleteTodoListBindingModel.TodoListId.Value));
                if (deleteTodoListCommandAffected != 0)
                {
                    return new JsonResult(new AlertApiModel("Delete Todo List", $"The List deleted."));
                }
                else
                {
                    return new JsonResult(new AlertApiModel("Delete Todo List", $"The List could not be deleted.")) {

                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }
            return new JsonResult(new AlertApiModel("Create Todo List", ModelState.GetModelStateErrors()))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
        public UpdateTodoListBindingModel UpdateTodoListBindingModel { get; set; }
        public async Task<IActionResult> OnPostUpdateTodoList(UpdateTodoListBindingModel updateTodoListBindingModel)
        {
            if (ModelState.IsValid)
            {
                var updateTodoListAffected = await Mediator.Send(new UpdateTodoListCommand(updateTodoListBindingModel.TodoListId.Value,updateTodoListBindingModel.Title));
                if (updateTodoListAffected!=0)
                {
                    return new JsonResult(new AlertApiModel("Update Todo List", $"The List updated."));
                }
                else
                {
                    return new JsonResult(new AlertApiModel("Update Todo List", $"The List could not be updated."))
                    {

                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }
            return new JsonResult(new AlertApiModel("Update Todo List", ModelState.GetModelStateErrors()))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
        public GetTodoListItemsBindingModel GetTodoListItemsBindingModel { get; set; }
        public async Task<IActionResult> OnPostGetTodoListItems(GetTodoListItemsBindingModel getTodoListItemsBindingModel)
        {
            if (ModelState.IsValid)
            {
                var todoListItems = await Mediator.Send(new GetTodoListItemsQuery(getTodoListItemsBindingModel.TodoListId.Value));
                if (todoListItems!=null)
                {
                    return new JsonResult(todoListItems);
                }
                else
                {
                    return new JsonResult(new AlertApiModel("Get Todo List Items", $"The List Items could not be fetched."))
                    {

                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }
            return new JsonResult(new AlertApiModel("Get Todo List Items", ModelState.GetModelStateErrors()))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
        public CreateTodoListItemBindingModel CreateTodoListItemBindingModel { get; set; }
        public async Task<IActionResult> OnPostCreateTodoListItem(CreateTodoListItemBindingModel createTodoListItemBindingModel)
        {
            if (ModelState.IsValid)
            {
                var creatTodoListItemId = await Mediator.Send(new CreateTodoListItemCommand(new TodoListItem(createTodoListItemBindingModel.TodoListId.Value, createTodoListItemBindingModel.Title, createTodoListItemBindingModel.Description, false)));
                if (creatTodoListItemId.HasValue)
                {
                    return new JsonResult(new AlertApiModel("Create Todo List Item", $"{createTodoListItemBindingModel.Title} created.", new { Id = creatTodoListItemId.Value }));
                }
                else
                {
                    return new JsonResult(new AlertApiModel("Create Todo List Item", $"The List Item could not be created."))
                    {

                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }
            return new JsonResult(new AlertApiModel("Create Todo List Item", ModelState.GetModelStateErrors()))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
        public async Task<IActionResult> OnPostUpdateTodoListItem(UpdateTodoListItemBindingModel  updateTodoListItemBindingModel)
        {
            if (ModelState.IsValid)
            {
                var updateTodoListItemAffected = await Mediator.Send(new UpdateTodoListItemCommand(updateTodoListItemBindingModel.TodoListItemId.Value, updateTodoListItemBindingModel.IsDone.Value));
                if (updateTodoListItemAffected != 0)
                {
                    return new JsonResult(new AlertApiModel("Update Todo List Item", $"The List Item updated."));
                }
                else
                {
                    return new JsonResult(new AlertApiModel("Update Todo List Item", $"The List Item could not be updated."))
                    {

                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }
            return new JsonResult(new AlertApiModel("Update Todo List Item", ModelState.GetModelStateErrors()))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
        public DeleteTodoListItemBindingModel DeleteTodoListItemBindingModel { get; set; }
        public async Task<IActionResult> OnPostDeleteTodoListItem(DeleteTodoListItemBindingModel deleteTodoListItemBindingModel)
        {
            if (ModelState.IsValid)
            {
                var creatTodoListItemId = await Mediator.Send(new DeleteTodoListItemCommand(deleteTodoListItemBindingModel.TodoListItemId.Value));
                if (creatTodoListItemId !=0)
                {
                    return new JsonResult(new AlertApiModel("Delete Todo List Item", $"The List Item deleted."));
                }
                else
                {
                    return new JsonResult(new AlertApiModel("Delete Todo List Item", $"The List Item could not be deleted."))
                    {

                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }
            return new JsonResult(new AlertApiModel("Delete Todo List Item", ModelState.GetModelStateErrors()))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }
}
