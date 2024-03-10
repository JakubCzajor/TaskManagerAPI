using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services.Interfaces;

public interface IAccountService
{
    void RegisterUser(RegisterUserDto dto);
}