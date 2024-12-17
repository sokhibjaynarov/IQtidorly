using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.OlympiadResultAnswers;
using IQtidorly.Api.Models.Olympiads;
using IQtidorly.Api.Models.Users;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.OlympiadResults
{
    public class OlympiadResult : BaseModel
    {
        public Guid OlympiadResultId { get; set; }

        [ForeignKey(nameof(Olympiad))]
        public Guid OlympiadId { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public int EmptyCount { get; set; }
        public int TotalScore { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }

        public virtual Olympiad Olympiad { get; set; }
        public virtual User User { get; set; }

        public virtual Collection<OlympiadResultAnswer> OlympiadResultAnswers { get; set; }
    }
}
