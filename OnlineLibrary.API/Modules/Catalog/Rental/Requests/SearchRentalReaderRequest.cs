﻿namespace OnlineLibrary.API.Modules.Catalog.Rental.Requests
{
    public class SearchRentalReaderRequest
    {
        public string? Title { get; set; }  
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? OrderBy { get; set; }    
    }
}
