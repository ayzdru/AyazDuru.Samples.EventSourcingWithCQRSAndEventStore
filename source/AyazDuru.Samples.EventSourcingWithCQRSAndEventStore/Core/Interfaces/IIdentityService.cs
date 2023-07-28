using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Models;
using System;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(Guid userId);

    Task<bool> IsInRoleAsync(Guid userId, string role);

    Task<bool> AuthorizeAsync(Guid userId, string policyName);

    Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(Guid userId);
}
