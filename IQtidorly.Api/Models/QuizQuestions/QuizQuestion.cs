using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.Questions;
using IQtidorly.Api.Models.Quizzes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.QuizQuestions
{
    public class QuizQuestion : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuizQuestionId { get; set; }

        [ForeignKey(nameof(Quiz))]
        public Guid QuizId { get; set; }

        [ForeignKey(nameof(Question))]
        public Guid QuestionId { get; set; }
        public int Order { get; set; }

        public virtual Quiz Quiz { get; set; }
        public virtual Question Question { get; set; }
    }
}
