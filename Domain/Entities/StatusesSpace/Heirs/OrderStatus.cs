using Domain.Entities.OrderSpace;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.StatusesSpace.Heirs
{
    /// <summary>
    /// Сущность, представляющая статус заказа.
    /// Используется для определения текущего состояния заказа, например, "Ожидание", "Обработка" или "Завершён".
    /// </summary>
    public class OrderStatus : Status
    { 
    }
}
