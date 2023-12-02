using FluentValidation;
using OnlineLibrary.API.Modules.Catalog.Rental.Requests;

namespace OnlineLibrary.API.Validators
{
    public class CreateRentalValidator : AbstractValidator<RentalRequest>
    {
        public CreateRentalValidator() 
        {
            RuleFor(request => request.BookIds)
                .Must(bookIds => bookIds.Count <= 10)
                .WithMessage("You can rent a maximum of 10 books.");
        }

    }
}
