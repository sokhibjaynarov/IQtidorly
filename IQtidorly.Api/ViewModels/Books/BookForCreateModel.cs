using IQtidorly.Api.Models.Books;
using System;
using System.ComponentModel.DataAnnotations;

namespace IQtidorly.Api.ViewModels.Books
{
    public class BookForCreateModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid CoverFileId { get; set; }

        [Required]
        public Guid ShortDecriptionFileId { get; set; }

        [Required]
        public Guid FileId { get; set; }

        [Required]
        public int TotalPages { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Guid BookAuthorId { get; set; }

        public BookTranslation Translations { get; set; }
    }
}
