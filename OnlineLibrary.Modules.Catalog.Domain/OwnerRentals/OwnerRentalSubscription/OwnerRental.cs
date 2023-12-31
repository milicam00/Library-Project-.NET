﻿using OnlineLibrary.BuildingBlocks.Domain;
using OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription;
using OnlineLibrary.Modules.Catalog.Domain.OwnerRentalBooks.OwnerRentalBookSubscription;

namespace OnlineLibrary.Modules.Catalog.Domain.OwnerRentals.OwnerRentalSubscription
{
    public class OwnerRental : Entity, IAggregateRoot
    {
        public Guid OwnerRentalId { get; set; }
        public Owner Owner { get; set; }
        public Guid OwnerId { get; set; }
        public List<OwnerRentalBook> OwnerRentalBooks { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public OwnerRental()
        {
            OwnerRentalId = Guid.NewGuid();
            OwnerRentalBooks = new List<OwnerRentalBook>();
        }

        public OwnerRental(Guid ownerId, List<Guid> bookIds)
        {
            OwnerRentalId = Guid.NewGuid();
            OwnerId = ownerId;
            OwnerRentalBooks = new List<OwnerRentalBook>();

            foreach (var id in bookIds)
            {
                OwnerRentalBooks.Add(new OwnerRentalBook(id));
            }
            RentalDate = DateTime.Now;

        }

        public static OwnerRental Create(Guid ownerId, List<Guid> bookIds)
        {
            return new OwnerRental(ownerId, bookIds);
        }

        public void ReturnBooks()
        {
            ReturnDate = DateTime.Now;
        }
    }
}
