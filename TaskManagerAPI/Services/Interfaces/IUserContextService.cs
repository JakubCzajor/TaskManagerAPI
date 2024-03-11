using System.Security.Claims;

namespace TaskManagerAPI.Services.Interfaces;

public interface IUserContextService
{
    ClaimsPrincipal User { get; }
    int? GetUserId { get; }
}