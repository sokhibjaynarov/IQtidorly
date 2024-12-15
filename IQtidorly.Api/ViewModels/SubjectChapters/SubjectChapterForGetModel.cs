using System;

namespace IQtidorly.Api.ViewModels.SubjectChapters
{
    public class SubjectChapterForGetModel
    {
        public Guid SubjectChapterId { get; set; }
        public string Name { get; set; }
        public Guid SubjectId { get; set; }
    }
}
