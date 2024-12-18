using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.QuizSubmissions;
using IQtidorly.Api.Models.Quizzes;
using IQtidorly.Api.Models.Users;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.QuizParticipants
{
    public class QuizParticipant : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuizResultId { get; set; }

        [ForeignKey(nameof(Quiz))]
        public Guid QuizId { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public int EmptyCount { get; set; }
        public int TotalScore { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public DateTime RegisteredDate { get; set; }

        public virtual Quiz Quiz { get; set; }
        public virtual User User { get; set; }

        public virtual Collection<QuizSubmission> QuizSubmission { get; set; }
    }
}
