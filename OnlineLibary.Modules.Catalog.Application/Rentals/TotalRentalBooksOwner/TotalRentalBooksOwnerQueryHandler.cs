using Dapper;
using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Application.Data;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.TotalRentals
{
    public class TotalRentalBooksOwnerQueryHandler : IQueryHandler<TotalRentalBooksOwnerQuery, Result>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IOwnerRepository _ownerRepository;
        public TotalRentalBooksOwnerQueryHandler(ISqlConnectionFactory connectionFactory, IOwnerRepository ownerRepository)
        {
            _connectionFactory = connectionFactory;
            _ownerRepository = ownerRepository;
        }

        public async Task<Result> Handle(TotalRentalBooksOwnerQuery request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetByUsername(request.OwnerUsername);
            if (owner == null)
            {
                return Result.Failure("This owner does not exist.");
            }

            var connection = _connectionFactory.GetOpenConnection();
            var parameters = new {OwnerId = owner.OwnerId, StartDate = request.StartDate, EndDate=request.EndDate};

            string sql = "SELECT COUNT(R.[BookId]) AS TotalBooks " +
             "FROM [Libraries] AS [L] " +
             "LEFT JOIN [Books] AS [B] ON L.[LibraryId] = B.[LibraryId] " +
             "LEFT JOIN [RentalBooks] AS [R] ON B.[BookId] = R.[BookId] " +
             "LEFT JOIN [Rentals] AS [A] ON R.[RentalId] = A.[RentalId] " +
             "WHERE [L].[OwnerId] = @OwnerId " +
             "AND [A].[RentalDate] BETWEEN @StartDate AND @EndDate";



            var result = (await connection.QueryAsync<int>(sql, parameters));
            return Result.Success(result);
            
        }
    }
}
