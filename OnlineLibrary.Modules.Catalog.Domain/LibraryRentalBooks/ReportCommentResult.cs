namespace OnlineLibrary.Modules.Catalog.Domain.LibraryRentalBooks
{
    public class ReportCommentResult
    {
        public Guid RentalBookId { get; set; }
        public string BookTitle { get; set; }   
        public int? Rate { get; set; }
        public string TextualComment { get; set; }
        public bool? IsCommentReported { get; set; }

    }
}
