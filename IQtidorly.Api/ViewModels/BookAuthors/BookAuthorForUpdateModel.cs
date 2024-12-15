using System;
using System.ComponentModel.DataAnnotations;

namespace IQtidorly.Api.ViewModels.BookAuthors
{
    public class BookAuthorForUpdateModel
    {
        [Required]
        public Guid BookAuthorId { get; set; }

        [Required]
        public string FirsName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Guid PhotoFileId { get; set; }
    }
}
