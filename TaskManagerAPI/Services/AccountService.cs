using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Exceptions;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services.Interfaces;

namespace TaskManagerAPI.Services;

public class AccountService : IAccountService
{
    private readonly TaskManagerDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public AccountService(TaskManagerDbContext context, IPasswordHasher<User> passwordHasher,
        AuthenticationSettings authenticationSettings, IUserContextService userContextService, IMapper mapper)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
        _userContextService = userContextService;
        _mapper = mapper;
    }

    public async Task RegisterUser(RegisterUserDto dto)
    {
        var newUser = new User()
        {
            Email = dto.Email,
            RoleId = dto.RoleId
        };

        var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
        newUser.PasswordHash = hashedPassword;

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
    }

    public async Task<string> GenerateJwtToken(LoginUserDto dto)
    {
        var user = await _context
            .Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user is null)
            throw new BadRequestException("Invalid username or password.");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new BadRequestException("Invalid username or password.");

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, $"{user.Email}"),
            new Claim(ClaimTypes.Role, $"{user.Role.Name}")
        };

        if (!(string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName)))
        {
            claims.Add(
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
                );
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(double.Parse(_authenticationSettings.JwtExpireDays));

        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    public async Task UpdateUserRole(int userId, UpdateRoleDto dto)
    {
        var user = await _context
            .Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            throw new NotFoundException("User not found.");

        if (user.Id == userId)
            throw new UnauthorizedException("Users are not authorized to change their own roles.");

        var role = await _context
            .Roles
            .FirstOrDefaultAsync(r => r.Id == dto.RoleId);

        if (role is null)
            throw new NotFoundException("Role not found.");

        if (user.Role.Name == "Manager" && role.Name == "Admin")
            throw new UnauthorizedException("You are not authorized to change user roles to roles higher than Manager.");

        if (user.Role.Name == "Manager" && role.Name == "User")
            throw new UnauthorizedException("Managers are not authorized to change other Managers roles to User.");

        user.RoleId = dto.RoleId;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserPassword(UpdatePasswordDto dto)
    {
        var userId = int.Parse(_userContextService.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var user = await _context
            .Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            throw new NotFoundException("User not found.");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.OldPassword);
        if (result == PasswordVerificationResult.Failed)
            throw new BadRequestException("Invalid password.");

        var hashedPassword = _passwordHasher.HashPassword(user, dto.NewPassword);
        user.PasswordHash = hashedPassword;

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserDto>> GetAll()
    {
        var tasks = await _context
            .Users
            .Include(u => u.Role)
            .ToListAsync();

        return _mapper.Map<List<UserDto>>(tasks);
    }

    public async Task<UserProfileDto> GetUserProfile()
    {
        var userId = int.Parse(_userContextService.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var user = await _context
            .Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            throw new NotFoundException("User not found.");

        return _mapper.Map<UserProfileDto>(user);
    }
}