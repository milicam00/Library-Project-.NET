using FluentValidation;
using OnlineLibrary.API.Modules.UserAccess.Requests;

namespace OnlineLibrary.API.Validators
{
    public class CheckPasswordValidator : AbstractValidator<ChangePasswordRequest>
    {
        public CheckPasswordValidator()
        {
            RuleFor(request => request.NewPassword)
                .MinimumLength(8).WithMessage("Password must contain at least  8 characters.")
                .Must(ContainUppercase).WithMessage("Password must contain at least one capital letter.")
                .Must(ContainLowercase).WithMessage("Password must contain at least one lowercase letter.")
                .Must(ContainDigit).WithMessage("Password must contain at least one digit.")
                .Matches(@"[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]").WithMessage("Password must contain at least one special character.");
        }
        private bool ContainUppercase(string password)
        {
            return password.Any(char.IsUpper);
        }

        private bool ContainLowercase(string password)
        {
            return password.Any(char.IsLower);
        }

        private bool ContainDigit(string password)
        {
            return password.Any(char.IsDigit);
        }
    }
}
