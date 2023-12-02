using OnlineLibrary.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.Events
{
    public class BookCreatedDomainEvent : DomainEventBase
    {
        public BookCreatedDomainEvent(Guid bookId)
        {
            BookId = bookId;
        }
        public Guid BookId { get; set; }

    }
}
