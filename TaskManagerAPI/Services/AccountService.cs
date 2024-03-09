using Microsoft.AspNetCore.Identity;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Services;

public interface IAccountService
{

}

public class AccountService : IAccountService
{
    private readonly TaskManagerDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;

    public AccountService(TaskManagerDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
    }
}