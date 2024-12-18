using IQtidorly.Api.Enums;
using System;

namespace IQtidorly.Api.ViewModels.Questions
{
    public class QuestionForGetListModel
    {
        public Guid QuestionId { get; set; }
        public string Content { get; set; }
        public QuestionType Type { get; set; }
        public Guid FileId { get; set; }
        public int Difficulty { get; set; }
        public Guid SubjectChapterId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid AgeGroupId { get; set; }
        public string SubjectName { get; set; }
        public string ChapterName { get; set; }
        public string AgeGroupName { get; set; }
    }
}
