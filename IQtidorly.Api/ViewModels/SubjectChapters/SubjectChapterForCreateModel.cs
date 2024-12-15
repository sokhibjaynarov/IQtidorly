using System;
using System.ComponentModel.DataAnnotations;

namespace IQtidorly.Api.ViewModels.SubjectChapters
{
    public class SubjectChapterForCreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid SubjectId { get; set; }
    }
}
