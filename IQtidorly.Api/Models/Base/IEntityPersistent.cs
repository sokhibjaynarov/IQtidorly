using IQtidorly.Api.Enums;
using System;

namespace IQtidorly.Api.Models.Base
{
    public interface IEntityPersistent
    {
        public EntityState EntityState { get; set; }

        public DateTime? InactivatedDate { get; set; }
    }
}
