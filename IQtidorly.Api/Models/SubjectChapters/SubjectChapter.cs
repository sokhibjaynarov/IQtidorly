using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.Subjects;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.SubjectChapters
{
    public class SubjectChapter : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SubjectChapterId { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Subject))]
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
