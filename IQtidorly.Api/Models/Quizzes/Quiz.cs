using IQtidorly.Api.Enums;
using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.QuizQuestions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.Quizzes
{
    public class Quiz : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public QuizType QuizType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RegistrationStartDate { get; set; }
        public DateTime RegistrationEndDate { get; set; }

        [Column(TypeName = "jsonb")]
        public QuizTranslation Translations { get; set; }

        public virtual Collection<QuizQuestion> QuizQuestions { get; set; }
    }
}
