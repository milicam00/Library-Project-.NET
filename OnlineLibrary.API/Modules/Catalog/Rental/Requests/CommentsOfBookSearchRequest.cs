﻿namespace OnlineLibrary.API.Modules.Catalog.Rental.Requests
{
    public class CommentsOfBookSearchRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; } 
        public string? OrderBy { get; set; } 
    }
}
