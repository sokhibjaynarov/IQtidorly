using System;
using System.ComponentModel.DataAnnotations;

namespace IQtidorly.Api.ViewModels.SubjectChapters
{
    public class SubjectChapterForUpdateModel
    {
        [Required]
        public Guid SubjectChapterId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid SubjectId { get; set; }
    }
}
