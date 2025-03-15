using Domain.Entities.Common;

namespace Domain.Entities.CategorySpace;

/// <summary>
/// Шаблон характеристик, применимых к товарам определённой категории.
/// Например, "Объём памяти", "Диагональ экрана".
/// </summary>
public class CategoryAttribute : AuditableEntity<CategoryAttribute>
{
   
    public Guid Id { get; set; }
    
    /// <summary>
    /// Ссылка на категорию (Category). 
    /// Здесь только Id; сам класс Category не приводим, т.к. он исключен из задачи.
    /// </summary> 
    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    /// <summary>
    /// Название атрибута (например, "Цвет").
    /// </summary> 
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Показывать ли данный атрибут в фильтрах (true = да).
    /// </summary>
    public bool IsFilterable { get; set; } = true;

    /// <summary>
    /// Участвует ли атрибут в формировании вариаций (SKU).
    /// Например, "Цвет" или "Размер" часто являются вариациями.
    /// </summary>
    public bool IsVariation { get; set; } = false;

    /// <summary>
    /// Порядок (позиция) отображения. Например, "Цвет" первым в списке характеристик.
    /// </summary>
    public int Position { get; set; } = 1;
 

    // Навигационное свойство - связь "один ко многим": 
    // Один CategoryAttribute имеет много значений (CategoryAttributeValue).
    public virtual ICollection<CategoryAttributeValue> CategoryAttributeValues { get; set; } = new List<CategoryAttributeValue>();

}