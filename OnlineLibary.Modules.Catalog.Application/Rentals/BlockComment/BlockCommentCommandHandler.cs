using Newtonsoft.Json;
using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryRentalBooks;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription;
using OnlineLibrary.Modules.Catalog.Domain.OutboxMessages.OutboxMessageSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.BlockComment
{
    public class BlockCommentCommandHandler : ICommandHandler<BlockCommentCommand, Result>
    {
        private readonly IRentalBooksRepository _rentalBooksRepository;
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        public BlockCommentCommandHandler(IRentalBooksRepository rentalBooksRepository,IOutboxMessageRepository outboxMessageRepository)
        {
            _rentalBooksRepository = rentalBooksRepository;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task<Result> Handle(BlockCommentCommand request, CancellationToken cancellationToken)
        {
            RentalBook? rentalBook = await _rentalBooksRepository.GetByIdAsync(request.RentalBookId);
           
            if (rentalBook == null)
            {
                return Result.Failure("This rental does not exist.");
            }
            
            if(rentalBook.TextualComment==null)
            {
                return Result.Failure("This rental is not commented.");
            }
            
            if(rentalBook.IsCommentApproved ==false)
            {
                return Result.Failure("This comment already is blocked.");
            }
            rentalBook.BlockComment();
            _rentalBooksRepository.UpdateRentalBook(rentalBook);

            BlockedCommentDto blockedComment = new BlockedCommentDto
            {
                Comment = rentalBook.TextualComment,
                IsCommentApproved = rentalBook.IsCommentApproved
            };

            Reader reader = await _rentalBooksRepository.GetReader(rentalBook.RentalId);
            Owner owner = await _rentalBooksRepository.GetOwner(rentalBook.BookId);

            string dataReader = JsonConvert.SerializeObject(new
            {
                Recipient = reader.Email,
                Subject = "Blocked comment",
                Body = $"Your comment: '{rentalBook.TextualComment}' is blocked."
            });
            string dataOwner = JsonConvert.SerializeObject(new
            {
                Recipient = owner.Email,
                Subject = "Blocked comment",
                Body = $"Your comment: '{rentalBook.TextualComment}' is blocked."
            });
            var outboxMessageReader = new OutboxMessage("Email", dataReader);
            var outboxMessageOwner = new OutboxMessage("Email", dataOwner);
            List<OutboxMessage> outboxMessages = new List<OutboxMessage>();
            outboxMessages.Add(outboxMessageReader);
            outboxMessages.Add(outboxMessageOwner);
            await _outboxMessageRepository.AddAsync(outboxMessages);



            return Result.Success(blockedComment);  
        }
    }
}
