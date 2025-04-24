namespace Infrastructure.Data.DbInitializer.DataClasses;

public static class IPhone
{
   
        
        public static readonly (string CategoryName, string Name, string Brand, Dictionary<string, string[]> Attributes)[] ProductsConfigurations =
        {
            (
                CategoryName: "Мобильные телефоны",
                Name: "iPhone 16",
                Brand: "Apple",
                Attributes: new Dictionary<string, string[]>
                {
                    { "Цвет", new[] { "Розовый", "Голубой", "Белый", "Бирюзовый", "Чёрный" } },
                    { "Память", new[] { "128GB", "256GB", "512GB" } }
                }
            ),
            (
                CategoryName: "Мобильные телефоны",
                Name: "iPhone 16 Plus",
                Brand: "Apple",
                Attributes: new Dictionary<string, string[]>
                {
                    { "Цвет", new[] { "Розовый", "Голубой", "Белый", "Бирюзовый", "Чёрный" } },
                    { "Память", new[] { "128GB", "256GB", "512GB" } }
                }
            ),
            (
                CategoryName: "Мобильные телефоны",
                Name: "iPhone 16 Pro",
                Brand: "Apple",
                Attributes: new Dictionary<string, string[]>
                {
                    { "Цвет", new[] { "Титановый белый", "Титановый песочный", "Титановый натуральный", "Титановый чёрный" } },
                    { "Память", new[] { "128GB", "256GB", "512GB", "1TB" } }
                }
            ),
            (
                CategoryName: "Мобильные телефоны",
                Name: "iPhone 16 Pro Max",
                Brand: "Apple",
                Attributes: new Dictionary<string, string[]>
                {
                    { "Цвет", new[] { "Титановый белый", "Титановый песочный", "Титановый натуральный", "Титановый чёрный" } },
                    { "Память", new[] { "128GB", "256GB", "512GB", "1TB" } }
                }
            ),
            (
                CategoryName: "Мобильные телефоны",
                Name: "iPhone 16e",
                Brand: "Apple",
                Attributes: new Dictionary<string, string[]>
                {
                    { "Цвет", new[] { "Розовый", "Голубой", "Белый", "Бирюзовый", "Чёрный" } },
                    { "Память", new[] { "128GB", "256GB" } }
                }
            )
        };
}