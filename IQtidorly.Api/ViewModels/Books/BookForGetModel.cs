using System;

namespace IQtidorly.Api.ViewModels.Books
{
    public class BookForGetModel
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CoverFileId { get; set; }
        public Guid ShortDecriptionFileId { get; set; }
        public Guid FileId { get; set; }
        public int TotalPages { get; set; }
        public decimal Price { get; set; }
        public Guid BookAuthorId { get; set; }
    }
}
