
using Domain.Entities.Common.Interfaces;
using Domain.Entities.UserSpace;
using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Common
{

    /// <summary>
    /// Базовый класс для сущностей с аудитом изменений
    /// </summary>
    public abstract class AuditableEntity<TEntity> : IAuditableEntity where TEntity : class
    {

        /// <summary>
        /// Идентификатор пользователя, создавшего запись
        /// </summary> 
        public Guid CreatedByUserId { get; set; }

        /// <summary>
        /// Навигационное свойство пользователя-создателя
        /// </summary>
        public virtual User CreatedByUser { get; set; } = null!;


        /// <summary>
        /// Идентификатор пользователя, обновившего запись
        /// </summary> 
        public Guid? UpdatedByUserId { get; set; }


        /// <summary>
        /// Навигационное свойство пользователя-редактора
        /// </summary>
        public virtual User? UpdatedByUser { get; set; }


        /// <summary>
        /// Идентификатор пользователя, удалившего запись
        /// </summary> 
        public Guid? DeletedByUserId { get; set; }

        /// <summary>
        /// Навигационное свойство пользователя-удалителя
        /// </summary>
        public virtual User? DeletedByUser { get; set; }

        /// <summary>
        /// Признак мягкого удаления записи
        /// </summary> 
        public bool IsDeleted { get; set; } = false;


        /// <summary>
        /// Дата и время создания записи (UTC)
        /// </summary> 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        /// <summary>
        /// Дата и время последнего обновления записи (UTC)
        /// </summary> 
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Дата и время мягкого удаления (UTC)
        /// </summary> 
        public DateTime? DeletedAt { get; set; }


        /// <summary>
        /// Версия записи для оптимистичной блокировки
        /// </summary> 
        public byte[]? RowVersion { get; set; } = Array.Empty<byte>();

        public virtual void OnCreate()
        {
            CreatedAt = DateTime.UtcNow;
        }


        public virtual void OnSoftDeleted(bool value = true)
        {
            IsDeleted = value;
        }

        public virtual void OnUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }

}
