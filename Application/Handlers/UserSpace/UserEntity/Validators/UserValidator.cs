using Application.Common;
using Domain.Entities.UserSpace;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserSpace.UserEntity.Validators
{
    public class UserValidator<TCommand> : DynamicValidator<TCommand>
    {
       

        protected override void ConfigureRules()
        {
            AddRule<string>(nameof(User.PhoneNumber),
                rule => rule
                .NotEmpty()
                .WithMessage("Номер телефона обязателен.")
                .Matches(@"^\+?[0-9]{7,15}$")
                .WithMessage("Неверный формат номера телефона. Ожидается от 7 до 15 цифр, опционально с плюсом.")
                );


            AddRule<string>(nameof(User.LastName), rule => rule
                  .MaximumLength(50)
                  .When(entity => !string.IsNullOrEmpty(GetPropertyValue<string>(entity, nameof(User.LastName))))
                  .WithMessage("Фамилия не должна превышать 50 символов.")
              );

             

            AddRule<string>(nameof(User.FirstName),
                  rule => rule
                 .MaximumLength(50)
                 .When(entity => !string.IsNullOrEmpty(GetPropertyValue<string>(entity, nameof(User.FirstName))))
                 .WithMessage("Имя не должно превышать 50 символов.")
                ); 

            AddRule<string>(nameof(User.Patronymic),
                rule => rule
                .MaximumLength(50)
                .When(entity => !string.IsNullOrEmpty(GetPropertyValue<string>(entity, nameof(User.Patronymic))))
                .WithMessage("Отчество не должно превышать 50 символов.")
                );
             

            AddRule<string>(nameof(User.Email),
                  rule => rule
                   .EmailAddress()
                   .When(entity => !string.IsNullOrEmpty(GetPropertyValue<string>(entity, nameof(User.Email))))
                   .WithMessage("Неверный формат электронной почты.")
                );

            AddRule<DateTime>(nameof(User.DateOfBirth), rule => rule
            .LessThan(DateTime.Now).WithMessage("Не корректное значение даты рождения") 
        );


        }
 
    }
}
