using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.Olympiads;
using IQtidorly.Api.Models.Questions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.OlympiadQuestions
{
    public class OlympiadQuestion : BaseModel
    {
        public Guid OlympiadQuestionId { get; set; }

        [ForeignKey(nameof(Olympiad))]
        public Guid OlympiadId { get; set; }

        [ForeignKey(nameof(Question))]
        public Guid QuestionId { get; set; }
        public int Order { get; set; }

        public virtual Olympiad Olympiad { get; set; }
        public virtual Question Question { get; set; }
    }
}
