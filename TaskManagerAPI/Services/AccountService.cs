using Microsoft.AspNetCore.Identity;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services;

public interface IAccountService
{
    void RegisterUser(RegisterUserDto dto);
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

    public void RegisterUser(RegisterUserDto dto)
    {
        var newUser = new User()
        {
            Email = dto.Email
        };

        var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
        newUser.PasswordHash = hashedPassword;

        _context.Users.Add(newUser);
        _context.SaveChanges();
    }
}