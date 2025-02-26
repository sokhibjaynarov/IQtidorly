using IQtidorly.Api.Models.Subjects;
using System.ComponentModel.DataAnnotations;

namespace IQtidorly.Api.ViewModels.Subjects
{
    public class SubjectForCreateModel
    {
        [Required]
        public string Name { get; set; }

        public SubjectTranslation Translations { get; set; }
    }
}
