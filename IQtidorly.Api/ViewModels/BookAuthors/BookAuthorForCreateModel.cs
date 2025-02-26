using IQtidorly.Api.Models.BookAuthors;
using System;
using System.ComponentModel.DataAnnotations;

namespace IQtidorly.Api.ViewModels.BookAuthors
{
    public class BookAuthorForCreateModel
    {
        [Required]
        public string FirsName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Guid PhotoFileId { get; set; }

        public BookAuthorTranslation Translations { get; set; }
    }
}
