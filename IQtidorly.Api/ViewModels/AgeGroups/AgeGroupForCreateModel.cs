using IQtidorly.Api.Models.AgeGroups;
using System.ComponentModel.DataAnnotations;

namespace IQtidorly.Api.ViewModels.AgeGroups
{
    public class AgeGroupForCreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int MinAge { get; set; }

        [Required]
        public int MaxAge { get; set; }

        public AgeGroupTranslation Translation { get; set; }
    }
}
