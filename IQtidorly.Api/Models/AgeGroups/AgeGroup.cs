using IQtidorly.Api.Models.Base;
using System;

namespace IQtidorly.Api.Models.AgeGroups
{
    public class AgeGroup : BaseModel
    {
        public Guid AgeGroupId { get; set; }
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}
