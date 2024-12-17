using IQtidorly.Api.Enums;
using IQtidorly.Api.ViewModels.QuestionOptions;
using System;
using System.Collections.Generic;

namespace IQtidorly.Api.ViewModels.Questions
{
    public class QuestionForCreateModel
    {
        public string Content { get; set; }
        public QuestionType Type { get; set; }
        public Guid FileId { get; set; }
        public int Difficulty { get; set; }
        public Guid SubjectChapterId { get; set; }
        public Guid AgeGroupId { get; set; }
        public List<QuestionOptionForSaveModel> Options { get; set; }
    }
}
