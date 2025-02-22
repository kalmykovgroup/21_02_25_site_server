using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations; 
using Domain.Entities.Common;

namespace Domain.Entities.AddressesSpace
{


    public enum AddressType
    { 
        General,
        User, 
        Supplier, 
    }


    /// <summary>
    ///  
    /// Адрес для клиентов, складов, поставщиков и сотрудников.
    /// Содержит данные о местоположении и принадлежности к различным объектам.
    /// </summary> 
    public abstract class Address : AuditableEntity<Address>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Тип сущности (кто владелец адреса)
        /// </summary> 
        public AddressType AddressType { get; set; }

     
        /// <summary>
        /// Название/метка адреса (например: "Дом", "Офис").
        /// </summary> 
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Улица и номер дома.
        /// </summary> 
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// Город.
        /// </summary> 
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Почтовый индекс.
        /// </summary> 
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// Область/штат.
        /// </summary> 
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Страна.
        /// </summary> 
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Дополнительная информация (этаж, квартира и т.д.).
        /// </summary> 
        public string? AdditionalInfo { get; set; }

        /// <summary>
        /// Признак основного адреса.
        /// </summary> 
        public bool IsPrimary { get; set; } = false;

 
          
    }

}
