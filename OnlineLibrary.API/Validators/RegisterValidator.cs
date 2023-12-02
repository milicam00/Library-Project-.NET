using FluentValidation;
using OnlineLibrary.API.Modules.UserAccess.Requests;

namespace OnlineLibrary.API.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(request => request.Password)
                .MinimumLength(8).WithMessage("Password must contain at least  8 characters.")
                .Must(ContainUppercase).WithMessage("Password must contain at least one capital letter.")
                .Must(ContainLowercase).WithMessage("Password must contain at least one lowercase letter.")
                .Must(ContainDigit).WithMessage("Password must contain at least one digit.")
                .Matches(@"[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]").WithMessage("Password must contain at least one special character.");

            RuleFor(request => request.Username)
                .Must(ContainNoUppercase).WithMessage("Username must not contain uppercase letters.");

            RuleFor(request => request.Email)
                .MaximumLength(100).WithMessage("Email address must not exceed 100 characters.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$") .WithMessage("Invalid email address format.");
        }
        private bool ContainUppercase(string password)
        {
            return password.Any(char.IsUpper);
        }
        private bool ContainNoUppercase(string username)
        {
            return !username.Any(char.IsUpper);
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
