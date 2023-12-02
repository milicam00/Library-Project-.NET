using FluentValidation;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.RateBook
{
    public class RateBookCommandValidator : AbstractValidator<RateBookCommand>
    {
        public RateBookCommandValidator()
        {
            RuleFor(command => command.Rate)
                .InclusiveBetween(1, 5)
                .WithMessage("Rate must be between 1 and 5.");
        }
    }
}
