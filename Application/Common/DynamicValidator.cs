using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public abstract class DynamicValidator<TEntity> : AbstractValidator<TEntity>
    {
        // Кешируем свойства для оптимизации 
        private static readonly Dictionary<string, PropertyInfo> _propertyCache = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                           .ToDictionary(p => p.Name, p => p);

        protected TResult? GetPropertyValue<TResult>(TEntity entity, string propertyName)
        {
            if (_propertyCache.TryGetValue(propertyName, out var propertyInfo))
            {
                object? value = propertyInfo.GetValue(entity);

                return value != null ? (TResult)value : default;

            }
            return default;
        }


        // 🔹 Универсальный метод для добавления правил в `_validationRules`
        protected void AddRule<TProperty>(string propertyName, Action<IRuleBuilderOptions<TEntity, TProperty>> rule)
        {
            _validationRules[propertyName] = rule; // Добавляем правило для конкретного свойства
        }

        // 🔹 Хранит правила валидации для свойств сущности (ключ — имя свойства, значение — делегат с логикой правила)
        private readonly Dictionary<string, Delegate> _validationRules = new();

        public DynamicValidator()
        {
            // 🔹 Вызываем метод, который заполняет `_validationRules` правилами (должен быть переопределён в наследниках)
            ConfigureRules();

            // 🔹 Перебираем все свойства сущности `TEntity`
            foreach (var property in typeof(TEntity).GetProperties())
            {
                // 🔹 Если для свойства есть правило в `_validationRules`, получаем его
                if (_validationRules.TryGetValue(property.Name, out var ruleDelegate))
                {
                    // 🔹 Создаём параметр `x` для лямбда-выражения `x => x.Property`
                    var param = Expression.Parameter(typeof(TEntity), "x");

                    // 🔹 Создаём доступ к свойству `x.Property`
                    var propertyAccess = Expression.Property(param, property);

                    // 🔹 Создаём строго типизированное лямбда-выражение `x => (TProperty)x.Property`
                    var lambda = Expression.Lambda(
                        Expression.Convert(propertyAccess, property.PropertyType), // Преобразуем к `TProperty`
                        param
                    );

                    // 🔹 Получаем метод `ApplyTypedRule<TProperty>` и указываем его тип `TProperty`
                    var method = typeof(DynamicValidator<TEntity>)
                        .GetMethod(nameof(ApplyTypedRule), BindingFlags.NonPublic | BindingFlags.Instance)
                        ?.MakeGenericMethod(property.PropertyType);

                    // 🔹 Вызываем `ApplyTypedRule<TProperty>()`, передавая в него выражение и правило
                    method?.Invoke(this, new object[] { lambda, ruleDelegate });
                }
            }
        }


        // 🔹 Метод, который должен переопределяться в наследниках для задания правил
        protected virtual void ConfigureRules() { }

        // 🔹 Применяет правило `ruleDelegate` к выражению `expression`
        private void ApplyTypedRule<TProperty>(
            LambdaExpression expression,
            Delegate ruleDelegate)
        {
            // 🔹 Преобразуем `LambdaExpression` в строго типизированное `Expression<Func<TEntity, TProperty>>`
            var typedExpression = (Expression<Func<TEntity, TProperty>>)expression;

            // 🔹 Создаём `RuleFor()` на основе выражения (пример: `RuleFor(x => x.Name)`)
            var ruleBuilder = RuleFor(typedExpression);

            // 🔹 Применяем правило `ruleDelegate`, преобразовав его в нужный тип
            if (ruleDelegate is Action<IRuleBuilderOptions<TEntity, TProperty>> typedRule)
            {
                typedRule.Invoke((IRuleBuilderOptions<TEntity, TProperty>)ruleBuilder); // Применяем правила валидации
            }
        }
    }

}
