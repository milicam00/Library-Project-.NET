﻿namespace OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription
{
    public class ReturnDto
    {
        public Guid RentalId { get; set; }
        public Guid ReaderId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}
