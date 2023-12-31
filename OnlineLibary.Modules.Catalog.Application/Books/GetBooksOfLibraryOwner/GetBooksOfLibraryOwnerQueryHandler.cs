﻿using Dapper;
using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Application.Data;
using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.BuildingBlocks.Domain.Results;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;

namespace OnlineLibary.Modules.Catalog.Application.Books.GetBooksOfLibraryOwner
{
    public class GetBooksOfLibraryOwnerQueryHandler : IQueryHandler<GetBooksOfLibraryOwnerQuery, Result>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IOwnerRepository _ownerRepository;
        public GetBooksOfLibraryOwnerQueryHandler(ISqlConnectionFactory connectionFactory, IOwnerRepository ownerRepository)
        {
            _connectionFactory = connectionFactory;
            _ownerRepository = ownerRepository;
        }
        public async Task<Result> Handle(GetBooksOfLibraryOwnerQuery request, CancellationToken cancellationToken)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var parameters = new { OwnerUsername = request.OwnerUsername, LibraryName=request.LibraryName };

            Owner owner = await _ownerRepository.GetByUsername(request.OwnerUsername);
            if (owner == null)
            {
                return Result.Failure("This owner does  not exist.");
            }

            string sql = "SELECT " +
            $"B.[BookId] AS [{nameof(BookOfOwnerDto.BookId)}], " +
            $"B.[Title] AS [{nameof(BookOfOwnerDto.Title)}], " +
            $"B.[Description] AS [{nameof(BookOfOwnerDto.Description)}], " +
            $"B.[Author] AS [{nameof(BookOfOwnerDto.Author)}], " +
            $"B.[Pages] AS [{nameof(BookOfOwnerDto.Pages)}], " +
            $"B.[PubblicationYear] AS [{nameof(BookOfOwnerDto.PubblicationYear)}], " +
            $"B.[UserRating] AS [{nameof(BookOfOwnerDto.UserRating)}], " +
            $"B.[Genres] AS [{nameof(BookOfOwnerDto.Genres)}], " +
            $"B.[NumberOfCopies] AS [{nameof(BookOfOwnerDto.NumberOfCopies)}], " +
            $"B.[NumberOfRatings] AS [{nameof(BookOfOwnerDto.NumberOfRatings)}], " +
            $"B.[IsDeleted] AS [{nameof(BookOfOwnerDto.IsDeleted)}], " +
            $"B.[LibraryId] AS [{nameof(BookOfOwnerDto.LibraryId)}] " +
            "FROM [Books] AS [B] " +
            "LEFT JOIN [Libraries] AS [L] ON B.[LibraryId] = L.[LibraryId] " +
            "LEFT JOIN [Owners] AS [O] ON L.[OwnerId] = O.[OwnerId] " +
            "WHERE O.[UserName] = @OwnerUsername AND L.[LibraryName] = @LibraryName";

            var result = (await connection.QueryAsync<BookOfOwnerDto>(sql, parameters)).AsList();

            return Result.Success(result);  
        }
    }
}
