using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.OlympiadResults;
using IQtidorly.Api.Models.QuestionOptions;
using IQtidorly.Api.Models.Questions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.OlympiadResultAnswers
{
    public class OlympiadResultAnswer : BaseModel
    {
        public Guid OlympiadResultAnswerId { get; set; }

        [ForeignKey(nameof(OlympiadResult))]
        public Guid OlympiadResultId { get; set; }

        [ForeignKey(nameof(Question))]
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }

        [ForeignKey(nameof(QuestionOption))]
        public Guid? SelectedOptionId { get; set; }

        public virtual OlympiadResult OlympiadResult { get; set; }
        public virtual Question Question { get; set; }
        public virtual QuestionOption SelectedOption { get; set; }
    }
}
