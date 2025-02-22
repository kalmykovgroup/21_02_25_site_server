using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public class History
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Название таблицы, в которой произошло изменение
        /// </summary>
        public string TableName { get; set; } = null!;

        /// <summary>
        /// Уникальный идентификатор изменённой записи
        /// </summary>
        public string RecordId { get; set; } = string.Empty;

        /// <summary>
        /// Тип изменения (Create, Update, Delete)
        /// </summary>
        public string ChangeType { get; set; } = null!;

        /// <summary>
        /// Дата и время изменения
        /// </summary>
        public DateTime ChangeDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// JSON со старыми данными (до изменения)
        /// </summary>
        public string? OldData { get; set; }

        /// <summary>
        /// JSON с новыми данными (после изменения)
        /// </summary>
        public string? NewData { get; set; }

        /// <summary>
        /// Id пользователя, который внёс изменение (если применимо)
        /// </summary>
        public Guid? UserId { get; set; }
    }
}
