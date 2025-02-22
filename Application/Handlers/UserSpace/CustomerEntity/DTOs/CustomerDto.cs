using Domain.Entities.ProductSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserSpace.CustomerEntity.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; set; }


        /// <summary>
        /// Номер телефона пользователя
        /// </summary> 
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Имя пользователя
        /// </summary> 
        public string? FirstName { get; set; } = null;

        /// <summary>
        /// Фамилия пользователя
        /// </summary> 
        public string? LastName { get; set; } = null;

        /// <summary>
        /// Фамилия пользователя
        /// </summary> 
        public string? Patronymic { get; set; } = null;

        public DateTime? DateOfBirth { get; set; } = null;

        /// <summary>
        /// Электронная почта пользователя
        /// </summary> 
        public string? Email { get; set; }

    }
}
