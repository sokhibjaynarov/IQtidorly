using System;

namespace IQtidorly.Api.ViewModels.QuizSubmissions
{
    public class SubmitAnswerModel
    {
        public Guid QuizId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid SelectedOptionId { get; set; }
        public string Answer { get; set; }
    }
}
