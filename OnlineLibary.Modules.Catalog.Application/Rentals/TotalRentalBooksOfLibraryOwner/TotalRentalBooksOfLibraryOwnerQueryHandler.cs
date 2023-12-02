using Dapper;
using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Application.Data;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.TotalRentalBooksOfLibraryOwner
{
    public class TotalRentalBooksOfLibraryOwnerQueryHandler : IQueryHandler<TotalRentalBooksOfLibraryOwnerQuery, Result>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ILibraryRepository _libraryRepository;
        public TotalRentalBooksOfLibraryOwnerQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IOwnerRepository ownerRepository, ILibraryRepository libraryRepository)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _ownerRepository = ownerRepository;
            _libraryRepository = libraryRepository;
        }

        public async Task<Result> Handle(TotalRentalBooksOfLibraryOwnerQuery request, CancellationToken cancellationToken)
        {

            var owner = await _ownerRepository.GetByUsername(request.OwnerUsername);
            if (owner == null)
            {
                return Result.Failure("This owner does not exist.");
            }
            var library = await _libraryRepository.GetByIdAsync(request.LibraryId);
            if(library == null)
            {
                return Result.Failure("This library does not exist.");
            }

            var connection = _sqlConnectionFactory.GetOpenConnection();
            var parameters = new { OwnerId = owner.OwnerId,LibraryId=request.LibraryId, StartDate = request.StartDate, EndDate = request.EndDate };

            string sql = "SELECT COUNT(R.[BookId]) AS TotalBooks " +
             "FROM [Libraries] AS [L] " +
             "LEFT JOIN [Books] AS [B] ON L.[LibraryId] = B.[LibraryId] " +
             "LEFT JOIN [RentalBooks] AS [R] ON B.[BookId] = R.[BookId] " +
             "LEFT JOIN [Rentals] AS [A] ON R.[RentalId] = A.[RentalId] " +
             "WHERE [L].[OwnerId] = @OwnerId AND [L].[LibraryId] = @LibraryId " +
             "AND [A].[RentalDate] BETWEEN @StartDate AND @EndDate";



            var result = (await connection.QueryAsync<int>(sql, parameters));
            return Result.Success(result);
        }
    }
}
