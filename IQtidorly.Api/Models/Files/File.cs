using IQtidorly.Api.Enums;
using IQtidorly.Api.Models.Base;
using IQtidorly.Api.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQtidorly.Api.Models.Files
{
    public class File : BaseModel, IEntityAuthorship
    {
        public Guid FileId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Extension { get; set; }

        public byte[]? Content { get; set; }

        public string? MimeType { get; set; }

        [Required]
        public bool IsTemporary { get; set; } = false;

        [Required]
        [ForeignKey(nameof(Author))]
        public Guid CreatedById { get; set; }

        public DateTime? InactivatedDate { get; set; }

        public Guid? InactivatedById { get; set; }

        [Column(TypeName = "varchar(24)")]
        public EntityState EntityState { get; set; } = EntityState.Active;

        #region ForeignKeys

        public virtual User Author { get; set; }

        #endregion

        public void Create(Guid createdById)
        {
            CreatedById = createdById;
        }

        public void Inactivate(Guid inactivatedById)
        {
            InactivatedById = inactivatedById;
        }
    }
}
