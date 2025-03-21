﻿using IQtidorly.Api.Enums;
using IQtidorly.Api.ViewModels.QuestionOptions;
using System;
using System.Collections.Generic;

namespace IQtidorly.Api.ViewModels.Questions
{
    public class QuestionForGetModel
    {
        public Guid QuestionId { get; set; }
        public string Content { get; set; }
        public QuestionType Type { get; set; }
        public Guid FileId { get; set; }
        public int Difficulty { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SubjectChapterId { get; set; }
        public Guid AgeGroupId { get; set; }
        public string SubjectName { get; set; }
        public string ChapterName { get; set; }
        public string AgeGroupName { get; set; }
        public List<QuestionOptionForGetModel> Options { get; set; }
    }
}
