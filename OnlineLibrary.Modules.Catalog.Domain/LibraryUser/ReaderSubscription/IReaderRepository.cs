﻿namespace OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription
{
    public interface IReaderRepository
    {
        Task AddAsync(Reader reader);
        Task<Reader> GetById(Guid readerId);
        void Update(Reader reader);
        Task<Reader> GetByUsername(string username);
        void Delete(Reader reader);
    }
}
