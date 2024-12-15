using System;

namespace IQtidorly.Api.ViewModels.AgeGroups
{
    public class AgeGroupForGetModel
    {
        public Guid AgeGroupId { get; set; }
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}
