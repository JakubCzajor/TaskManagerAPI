using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services.Interfaces;

public interface IAccountService
{
    Task RegisterUser(RegisterUserDto dto);
}