using IQtidorly.Api.Attributes;
using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.BookAuthors;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.Books
{
    public class Book : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BookId { get; set; }

        [Translatable]
        public string Title { get; set; }

        [Translatable]
        public string Description { get; set; }
        public Guid CoverFileId { get; set; }
        public Guid ShortDecriptionFileId { get; set; }
        public Guid FileId { get; set; }
        public int TotalPages { get; set; }
        public decimal Price { get; set; }

        [TranslationProperty]
        [Column(TypeName = "jsonb")]
        public BookTranslation Translations { get; set; }

        [ForeignKey(nameof(BookAuthor))]
        public Guid BookAuthorId { get; set; }

        public BookAuthor BookAuthor { get; set; }
    }
}
