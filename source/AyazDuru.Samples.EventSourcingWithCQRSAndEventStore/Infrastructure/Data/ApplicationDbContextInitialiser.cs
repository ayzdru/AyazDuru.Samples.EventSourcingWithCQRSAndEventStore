using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Data;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new ApplicationRole();

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!_context.TodoLists.Any())
        {
            var todoList = new TodoList("Todo List", Colour.White);
            todoList.Add(new TodoListItem("Make a todo list 📃", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "));
            todoList.Add(new TodoListItem("Check off the first item ✅", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Bibendum ut tristique et egestas quis ipsum suspendisse ultrices. Euismod nisi porta lorem mollis. Curabitur vitae nunc sed velit. Diam maecenas sed enim ut sem viverra aliquet. Odio pellentesque diam volutpat commodo sed. Nec nam aliquam sem et tortor consequat id."));
            todoList.Add(new TodoListItem("Realise you've already done two things on the list! 🤯", "Id cursus metus aliquam eleifend mi in. Mauris rhoncus aenean vel elit. Et sollicitudin ac orci phasellus egestas tellus rutrum. Porttitor lacus luctus accumsan tortor. Eget egestas purus viverra accumsan in nisl nisi scelerisque. Quam lacus suspendisse faucibus interdum posuere lorem. Consequat nisl vel pretium lectus quam id."));
            todoList.Add(new TodoListItem("Reward yourself with a nice, long nap 🏆", "Tortor aliquam nulla facilisi cras fermentum odio eu feugiat. Faucibus et molestie ac feugiat sed lectus. Neque volutpat ac tincidunt vitae semper quis. Nunc mattis enim ut tellus elementum. Sit amet est placerat in egestas erat. Turpis cursus in hac habitasse platea dictumst quisque sagittis. Mi proin sed libero enim sed. Orci sagittis eu volutpat odio facilisis mauris sit."));
            _context.TodoLists.Add(todoList);
            var todoList2 = new TodoList("Todo List", Colour.Red);
            todoList2.Add(new TodoListItem("Make a todo list 📃", "Turpis in eu mi bibendum neque egestas congue. Facilisis mauris sit amet massa vitae tortor condimentum lacinia quis. Diam in arcu cursus euismod quis viverra nibh cras pulvinar. Non arcu risus quis varius quam. Odio pellentesque diam volutpat commodo sed. Vivamus at augue eget arcu. Malesuada fames ac turpis egestas. Volutpat lacus laoreet non curabitur. "));
            todoList2.Add(new TodoListItem("Check off the first item ✅", "Semper feugiat nibh sed pulvinar proin gravida hendrerit. Ac tincidunt vitae semper quis lectus nulla. Nibh tortor id aliquet lectus proin nibh nisl. Massa sed elementum tempus egestas sed. Mattis rhoncus urna neque viverra justo nec ultrices dui. Consequat id porta nibh venenatis cras sed felis. Libero nunc consequat interdum varius. Semper viverra nam libero justo. "));
            todoList2.Add(new TodoListItem("Realise you've already done two things on the list! 🤯", "Gravida in fermentum et sollicitudin. Id aliquet risus feugiat in ante metus dictum at. Id diam maecenas ultricies mi eget mauris. Non consectetur a erat nam at lectus. Posuere morbi leo urna molestie at elementum. Elit at imperdiet dui accumsan sit amet. Massa tempor nec feugiat nisl."));
            todoList2.Add(new TodoListItem("Reward yourself with a nice, long nap 🏆", "Gravida in fermentum et sollicitudin. Id aliquet risus feugiat in ante metus dictum at. Id diam maecenas ultricies mi eget mauris. Non consectetur a erat nam at lectus. Posuere morbi leo urna molestie at elementum. Elit at imperdiet dui accumsan sit amet. Massa tempor nec feugiat nisl."));
            _context.TodoLists.Add(todoList2);
            var todoList3 = new TodoList("Todo List", Colour.Blue);
            todoList3.Add(new TodoListItem("Make a todo list 📃", "Gravida in fermentum et sollicitudin. Id aliquet risus feugiat in ante metus dictum at. Id diam maecenas ultricies mi eget mauris. Non consectetur a erat nam at lectus. Posuere morbi leo urna molestie at elementum. Elit at imperdiet dui accumsan sit amet. Massa tempor nec feugiat nisl."));
            todoList3.Add(new TodoListItem("Check off the first item ✅", "Gravida in fermentum et sollicitudin. Id aliquet risus feugiat in ante metus dictum at. Id diam maecenas ultricies mi eget mauris. Non consectetur a erat nam at lectus. Posuere morbi leo urna molestie at elementum. Elit at imperdiet dui accumsan sit amet. Massa tempor nec feugiat nisl."));
            todoList3.Add(new TodoListItem("Realise you've already done two things on the list! 🤯", "Gravida in fermentum et sollicitudin. Id aliquet risus feugiat in ante metus dictum at. Id diam maecenas ultricies mi eget mauris. Non consectetur a erat nam at lectus. Posuere morbi leo urna molestie at elementum. Elit at imperdiet dui accumsan sit amet. Massa tempor nec feugiat nisl."));
            todoList3.Add(new TodoListItem("Reward yourself with a nice, long nap 🏆", "Gravida in fermentum et sollicitudin. Id aliquet risus feugiat in ante metus dictum at. Id diam maecenas ultricies mi eget mauris. Non consectetur a erat nam at lectus. Posuere morbi leo urna molestie at elementum. Elit at imperdiet dui accumsan sit amet. Massa tempor nec feugiat nisl."));
            _context.TodoLists.Add(todoList3);
            await _context.SaveChangesAsync();
        }
    }
}
