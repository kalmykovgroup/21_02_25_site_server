using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common.Interfaces
{
    public interface IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedByUserId { get; set; }
        public byte[]? RowVersion { get; set; }

        public void OnUpdated();
        public void OnCreate();
        public void OnSoftDeleted(bool value = true);

        
    }
}
