using IQtidorly.Api.Enums;
using System;

namespace IQtidorly.Api.ViewModels.QuizQuestions
{
    public class QuizQuestionForSaveModel
    {
        public Guid? QuizQuestionId { get; set; }
        public Guid QuestionId { get; set; }
        public State State { get; set; }
    }
}
