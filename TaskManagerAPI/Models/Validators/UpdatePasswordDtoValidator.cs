using FluentValidation;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Models.Accounts;

namespace TaskManagerAPI.Models.Validators;

public class UpdatePasswordDtoValidator : AbstractValidator<UpdatePasswordDto>
{
    public UpdatePasswordDtoValidator(TaskManagerDbContext context)
    {
        RuleFor(u => u.NewPassword).MinimumLength(8);

        RuleFor(u => u.ConfirmPassword).Equal(e => e.NewPassword);
    }
}