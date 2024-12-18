using IQtidorly.Api.Enums;
using IQtidorly.Api.Models.AgeGroups;
using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.QuestionOptions;
using IQtidorly.Api.Models.SubjectChapters;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.Questions
{
    public class Question : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuestionId { get; set; }
        public string Content { get; set; }
        public QuestionType Type { get; set; }
        public Guid FileId { get; set; }
        public int Difficulty { get; set; }

        [ForeignKey(nameof(SubjectChapter))]
        public Guid SubjectChapterId { get; set; }

        [ForeignKey(nameof(AgeGroup))]
        public Guid AgeGroupId { get; set; }


        public virtual SubjectChapter SubjectChapter { get; set; }
        public virtual AgeGroup AgeGroup { get; set; }

        public virtual Collection<QuestionOption> Options { get; set; }
    }
}
