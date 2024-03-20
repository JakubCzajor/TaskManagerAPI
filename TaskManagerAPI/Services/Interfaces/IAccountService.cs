using TaskManagerAPI.Models.Accounts;

namespace TaskManagerAPI.Services.Interfaces;

public interface IAccountService
{
    Task RegisterUser(RegisterUserDto dto);
    Task<string> GenerateJwtToken(LoginUserDto dto);
    Task UpdateUserRole(int id, UpdateRoleDto dto);
    Task UpdateUserPassword(UpdatePasswordDto dto);
    Task<IEnumerable<UserDto>> GetAll();
    Task<UserProfileDto> GetUserProfile();
}