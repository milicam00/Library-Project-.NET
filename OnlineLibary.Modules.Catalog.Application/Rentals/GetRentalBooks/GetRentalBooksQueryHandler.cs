﻿
using Dapper;
using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.BuildingBlocks.Application.Data;
using OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions;

namespace OnlineLibary.Modules.Catalog.Application.Rentals.GetRentalBooks
{
    public class GetRentalBooksQueryHandler : IQueryHandler<GetRentalBooksQuery,List<Guid>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetRentalBooksQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
           _sqlConnectionFactory = sqlConnectionFactory;
        }
       
        public async Task<List<Guid>> Handle(GetRentalBooksQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            string sql = "SELECT " +
               $"[BookId] AS [{nameof(BookId)}] " +
               "FROM [RentalBooks] AS [RentalBooks] " +
            "WHERE [RentalId] = @RentalId";

            var parameters = new { RentalId = request.RentalId };
          
            return (await connection.QueryAsync<Guid>(sql, parameters)).AsList();
        }
    }
}
