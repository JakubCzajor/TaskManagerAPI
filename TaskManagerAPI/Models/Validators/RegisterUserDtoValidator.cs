using FluentValidation;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Models.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator(TaskManagerDbContext context)
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(u => u.Password).MinimumLength(8);

        RuleFor(u => u.ConfirmPassword).Equal(e => e.Password);

        RuleFor(u => u.Email)
            .Custom((value, con) =>
            {
                var emailInUse = context.Users.Any(u => u.Email == value);
                if (emailInUse)
                {
                    con.AddFailure("Email", "This email is already in use.");
                }
            });
    }
}