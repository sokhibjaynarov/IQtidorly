using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.Books;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.BookAuthors
{
    public class BookAuthor : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BookAuthorId { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public Guid PhotoFileId { get; set; }

        public Collection<Book> Books { get; set; }
    }
}
