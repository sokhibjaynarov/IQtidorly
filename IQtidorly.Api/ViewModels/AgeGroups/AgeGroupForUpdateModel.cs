using IQtidorly.Api.Models.AgeGroups;
using System;
using System.ComponentModel.DataAnnotations;

namespace IQtidorly.Api.ViewModels.AgeGroups
{
    public class AgeGroupForUpdateModel
    {
        [Required]
        public Guid AgeGroupId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int MinAge { get; set; }

        [Required]
        public int MaxAge { get; set; }

        [Required]
        public AgeGroupTranslation Translation { get; set; }
    }
}