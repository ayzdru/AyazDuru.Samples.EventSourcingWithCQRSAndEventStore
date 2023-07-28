using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Application.Aggregates;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Data;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.IoC;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.IoC;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Repositories;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using ProtoBuf.Meta;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);
var eventStoreConnection = EventStoreConnection.Create(
               connectionString: builder.Configuration.GetValue<string>("EventStore:ConnectionString"),
builder: ConnectionSettings.Create().EnableVerboseLogging()
                 .UseConsoleLogger().DisableTls().DisableServerCertificateValidation().KeepReconnecting(),
               connectionName: builder.Configuration.GetValue<string>("EventStore:ConnectionName"));
eventStoreConnection.Connected += static (sender, clientConnectionEventArgs) =>
{
    Console.WriteLine($"'{clientConnectionEventArgs.Connection.ConnectionName}' is connected.");  
};
eventStoreConnection.Disconnected += (sender, clientConnectionEventArgs) =>
{
    Console.WriteLine($"'{clientConnectionEventArgs.Connection.ConnectionName}' is disconnected.");
};
eventStoreConnection.Reconnecting += (sender, clientReconnectingEventArgs) =>
{
    Console.WriteLine($"'{clientReconnectingEventArgs.Connection.ConnectionName}' reconnecting.");
};
eventStoreConnection.ErrorOccurred += (sender, clientErrorEventArgs) =>
{
    Console.WriteLine($"'{clientErrorEventArgs.Connection.ConnectionName}' error occurered.\n{clientErrorEventArgs.Exception.Message}");
};

eventStoreConnection.ConnectAsync().GetAwaiter().GetResult();
builder.Services.AddSingleton(eventStoreConnection);
builder.Services.AddTransient<IAggregateRepository, AggregateRepository>();
builder.Services.AddTransient<TodoListAggregate>();
builder.Services.AddTransient<TodoListItemAggregate>();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddWeb();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

public partial class Program { }