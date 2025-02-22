using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public abstract class BaseDto<T> where T : BaseDto<T>
    {
        public class Validator : AbstractValidator<T>
        {
            public Validator()
            {
                if (typeof(T).GetMethod(nameof(ConfigureValidationRules)) is { } method)
                {
                    method.Invoke(this, null);
                }
            }
        }

        /// <summary>
        /// Метод для настройки правил валидации, который должен переопределяться в наследниках.
        /// </summary>
        protected abstract void ConfigureValidationRules();
    }
}
