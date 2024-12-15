using System;

namespace IQtidorly.Api.Models.Base
{
    public interface IEntityAuthorship
    {
        public Guid CreatedById { get; set; }

        public Guid? InactivatedById { get; set; }

        public void Create(Guid createdById);

        public void Inactivate(Guid InactivatedById);
    }
}
