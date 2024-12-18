using IQtidorly.Api.Enums;
using IQtidorly.Api.ViewModels.QuizQuestions;
using System;
using System.Collections.Generic;

namespace IQtidorly.Api.ViewModels.Quizzes
{
    public class QuizForCreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public QuizType QuizType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RegistrationStartDate { get; set; }
        public DateTime RegistrationEndDate { get; set; }

        public List<QuizQuestionForSaveModel> QuizQuestions { get; set; }
    }
}
