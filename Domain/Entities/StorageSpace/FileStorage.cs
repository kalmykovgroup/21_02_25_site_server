using Domain.Entities.Common; 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities._Storage
{
    /// <summary>
    /// Категория файла для определения типа сущности в TPH.
    /// </summary>
    public enum FileCategory
    {
        GeneralFile,   // Общий тип файлов (например, если нет явного типа)
        ProductFile,   // Файлы, связанные с продуктами
        OrderFile,     // Файлы, связанные с заказами
        ReviewFile     // Файлы, связанные с отзывами
    }

    /// <summary>
    /// Типа сущности TPH.
    /// Это подход, при котором несколько классов иерархии наследования сопоставляются с одной таблицей в базе данных
    /// Базовая сущность для хранения данных о файлах.
    /// Используется для управления файлами, связанными с продуктами, заказами, отзывами и другими сущностями.
    /// </summary> 
    public abstract class FileStorage : AuditableEntity<FileStorage>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Дискриминатор для определения категории файла.
        /// Например, общий файл, файл продукта или заказа.
        /// </summary> 
        public FileCategory FileCategory { get; protected set; }

        /// <summary>
        /// Имя файла (например, "image.png").
        /// Используется для отображения или управления файлом.
        /// </summary> 
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Путь к файлу на сервере или в облачном хранилище.
        /// Указывает местоположение файла.
        /// </summary> 
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// MIME-тип файла (например, "image/png", "application/pdf").
        /// Используется для определения типа содержимого файла.
        /// </summary> 
        public string FileType { get; set; } = string.Empty;

        /// <summary>
        /// Размер файла в байтах.
        /// Используется для отображения или анализа объёма данных.
        /// </summary> 
        public long FileSize { get; set; }

        /// <summary>
        /// Признак основного файла для сущности.
        /// Например, основное изображение для продукта.
        /// </summary> 
        public bool IsPrimary { get; set; } = false;
         
    }

}
