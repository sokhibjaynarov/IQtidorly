using System;

namespace IQtidorly.Api.ViewModels.BookAuthors
{
    public class BookAuthorForGet
    {
        public Guid BookAuthorId { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public Guid PhotoFileId { get; set; }
    }
}
