using Dapper;
using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Application.Data;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;
using OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.TopTenMostPopularBooksOfLIbraryOwner
{
    public class TopFiveMostPopularBooksOfLibraryOwnerQueryHandler : IQueryHandler<TopFiveMostPopularBooksOfLibraryOwnerQuery, Result>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ILibraryRepository _libraryRepository;
        
        public TopFiveMostPopularBooksOfLibraryOwnerQueryHandler(ISqlConnectionFactory connectionFactory,IOwnerRepository ownerRepository,ILibraryRepository libraryRepository)
        {
            _connectionFactory = connectionFactory;
            _ownerRepository = ownerRepository;
            _libraryRepository = libraryRepository;
        }
        public async Task<Result> Handle(TopFiveMostPopularBooksOfLibraryOwnerQuery request, CancellationToken cancellationToken)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var owner = await _ownerRepository.GetByUsername(request.OwnerUsername);
            if (owner == null)
            {
                return Result.Failure("This owner does not exist.");
            }
            var parameters = new { OwnerId = owner.OwnerId,LibraryId = request.LibraryId, StartDate = request.StartDate, EndDate = request.EndDate };

           

            var library = await _libraryRepository.GetByIdAsync(request.LibraryId);
            if (library == null)
            {
                return Result.Failure("This library does not exist.");
            }

            string sql = @"
            SELECT TOP 5
            A.[BookId] AS [BookId], 
            A.[Title] AS [Title], 
            A.[Description] AS [Description], 
            A.[Author] AS [Author], 
            A.[Pages] AS [Pages], 
            A.[Genres] AS [Genres], 
            A.[PubblicationYear] AS [PubblicationYear], 
            A.[UserRating] AS [UserRating], 
            A.[NumberOfCopies] AS [NumberOfCopies]
            FROM [Books] AS [A]
            WHERE A.[BookId] IN (
                SELECT TOP 5 B.[BookId]
                FROM [Libraries] AS [L]
                JOIN [Books] AS [B] ON L.[LibraryId] = B.[LibraryId]
                JOIN [RentalBooks] AS [RB] ON B.[BookId] = RB.[BookId]
                JOIN [Rentals] AS [R] ON RB.[RentalId] = R.[RentalId]
                WHERE R.[RentalDate] BETWEEN @StartDate AND @EndDate AND L.[LibraryId] = @LibraryId AND L.[OwnerId]=@OwnerId
                GROUP BY B.[BookId]
                ORDER BY COUNT(*) DESC
            );";

            var result = (await connection.QueryAsync<BookDto>(sql, parameters)).AsList();
            return Result.Success(result);
        }
    }
}
