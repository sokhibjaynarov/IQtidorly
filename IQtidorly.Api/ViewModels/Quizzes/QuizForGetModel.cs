using IQtidorly.Api.Enums;
using System;

namespace IQtidorly.Api.ViewModels.Quizzes
{
    public class QuizForGetModel
    {
        public Guid QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public QuizType QuizType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RegistrationStartDate { get; set; }
        public DateTime RegistrationEndDate { get; set; }
    }
}
