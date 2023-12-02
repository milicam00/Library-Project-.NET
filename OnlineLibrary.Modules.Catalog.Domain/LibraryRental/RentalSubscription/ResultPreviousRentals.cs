using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription
{
    public class ResultPreviousRentals
    {
        public string LibraryName { get; set; }
        public List<string> BookNames { get; set; }
        public Guid RentalId { get; set; }
        public DateTime RentalDate { get; set; }
        public bool Returned { get; set; }
        public string UserName { get; set; }
    }
}
