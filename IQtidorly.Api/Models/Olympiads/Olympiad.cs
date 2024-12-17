using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.OlympiadQuestions;
using System;
using System.Collections.ObjectModel;

namespace IQtidorly.Api.Models.Olympiads
{
    public class Olympiad : BaseModel
    {
        public Guid OlympiadId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Collection<OlympiadQuestion> OlympiadQuestions { get; set; }
    }
}
