using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.SubjectChapters;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.Subjects
{
    public class Subject : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SubjectId { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "jsonb")]
        public SubjectTranslation Translations { get; set; }

        public Collection<SubjectChapter> SubjectChapters { get; set; }
    }
}
