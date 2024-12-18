using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.QuestionOptions;
using IQtidorly.Api.Models.Questions;
using IQtidorly.Api.Models.QuizParticipants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.QuizSubmissions
{
    public class QuizSubmission : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuizSubmissionId { get; set; }

        [ForeignKey(nameof(QuizParticipant))]
        public Guid QuizParticipantId { get; set; }

        [ForeignKey(nameof(Question))]
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }

        [ForeignKey(nameof(QuestionOption))]
        public Guid? SelectedOptionId { get; set; }

        public virtual QuizParticipant QuizParticipant { get; set; }
        public virtual Question Question { get; set; }
        public virtual QuestionOption SelectedOption { get; set; }
    }
}
