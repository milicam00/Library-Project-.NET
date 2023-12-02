using OnlineLibrary.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.Events
{
    public class LibraryCreatedDomainEvent : DomainEventBase
    {
        public LibraryCreatedDomainEvent(Guid libraryId)
        {
            LibraryId = libraryId;
        }

        public Guid LibraryId { get; set; }
    }
}
