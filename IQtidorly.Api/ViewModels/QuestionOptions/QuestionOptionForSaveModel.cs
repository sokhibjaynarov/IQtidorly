using IQtidorly.Api.Enums;
using System;

namespace IQtidorly.Api.ViewModels.QuestionOptions
{
    public class QuestionOptionForSaveModel
    {
        public Guid? QuestionOptionId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public State State { get; set; }
    }
}
