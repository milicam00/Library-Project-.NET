﻿namespace OnlineLibrary.BuildingBlocks.Domain.Results
{
    public class ImportedBookDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public int Genres { get; set; }
        public int PubblicationYear { get; set; }
        public double UserRating { get; set; }
        public int NumberOfCopies { get; set; }
        public int NumberOfRatings { get; set; }
        public bool IsDeleted { get; set; }
        public string LibraryId { get; set; }
    }
}
