using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.SubjectChapters;
using System;
using System.Collections.ObjectModel;

namespace IQtidorly.Api.Models.Subjects
{
    public class Subject : BaseModel
    {
        public Guid SubjectId { get; set; }
        public string Name { get; set; }

        public Collection<SubjectChapter> SubjectChapters { get; set; }
    }
}
