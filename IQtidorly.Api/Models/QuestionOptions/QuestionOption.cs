using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.Questions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.QuestionOptions
{
    public class QuestionOption : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuestionOptionId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }

        [Column(TypeName = "jsonb")]
        public QuestionOptionTranslation Translations { get; set; }

        [ForeignKey(nameof(Question))]
        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
