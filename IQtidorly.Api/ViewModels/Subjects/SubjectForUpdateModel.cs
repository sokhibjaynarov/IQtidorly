using System;
using System.ComponentModel.DataAnnotations;

namespace IQtidorly.Api.ViewModels.Subjects
{
    public class SubjectForUpdateModel
    {
        [Required]
        public Guid SubjectId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
