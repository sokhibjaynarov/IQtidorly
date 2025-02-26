using IQtidorly.Api.Enums;
using IQtidorly.Api.Models.QuestionOptions;
using System;

namespace IQtidorly.Api.ViewModels.QuestionOptions
{
    public class QuestionOptionForSaveModel
    {
        public Guid? QuestionOptionId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public State State { get; set; }
        public QuestionOptionTranslation Translations { get; set; }
    }
}
