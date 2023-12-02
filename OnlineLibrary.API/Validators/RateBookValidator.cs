using FluentValidation;
using OnlineLibrary.API.Modules.Catalog.Rental.Requests;

namespace OnlineLibrary.API.Validators
{
    public class RateBookValidator : AbstractValidator<RateRequest>
    {
        public RateBookValidator()
        {
            RuleFor(request => request.Rate)
                .InclusiveBetween(1, 5)
                .WithMessage("Rate must be between 1 and 5.");
        }

    }
}
