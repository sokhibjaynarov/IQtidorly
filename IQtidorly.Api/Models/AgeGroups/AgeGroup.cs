using IQtidorly.Api.Attributes;
using IQtidorly.Api.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.AgeGroups
{
    public class AgeGroup : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AgeGroupId { get; set; }

        [Translatable]
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        [Column(TypeName = "jsonb")]
        public AgeGroupTranslation Translation { get; set; }
    }
}
