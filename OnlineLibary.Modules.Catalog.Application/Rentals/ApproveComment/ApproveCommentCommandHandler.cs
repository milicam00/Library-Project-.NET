using Newtonsoft.Json;
using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRentalBooks;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using OnlineLibrary.Modules.Catalog.Domain.OutboxMessages.OutboxMessageSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.ApproveComment
{
    public class ApproveCommentCommandHandler : ICommandHandler<ApproveCommentCommand, Result>
    {
        private readonly IRentalBooksRepository _rentalBooksRepository;
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        public ApproveCommentCommandHandler(IRentalBooksRepository rentalBooksRepository, IOutboxMessageRepository outboxMessageRepository)
        {
            _rentalBooksRepository = rentalBooksRepository;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task<Result> Handle(ApproveCommentCommand request, CancellationToken cancellationToken)
        {
            RentalBook? rentalBook = await _rentalBooksRepository.GetByIdAsync(request.RentalBookId);
            if (rentalBook == null)
            {
                return Result.Failure("This rental does not exist.");
            }
            if (rentalBook.TextualComment == null)
            {
                return Result.Failure("This rental is not commented.");
            }

            if (rentalBook.IsCommentApproved == true)
            {
                return Result.Failure("This comment already is approved.");
            }
            rentalBook.ApproveComment();
            _rentalBooksRepository.UpdateRentalBook(rentalBook);

            ApprovedCommentDto approvedComment = new ApprovedCommentDto
            {
                Comment = rentalBook.TextualComment,
                IsCommentApproved = rentalBook.IsCommentApproved
            };


            Owner owner = await _rentalBooksRepository.GetOwner(rentalBook.BookId);

            string data = JsonConvert.SerializeObject(new
            {
                Recipient = owner.Email,
                Subject = "Approved comment",
                Body = $"Comment '{rentalBook.TextualComment}' is approved."
            });

            var outboxMessage = new OutboxMessage("Email", data);
            await _outboxMessageRepository.AddAsync(outboxMessage);
         


            return Result.Success(approvedComment);
        }
    }
}
