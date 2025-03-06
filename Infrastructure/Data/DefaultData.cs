using Domain.Entities.AddressesSpace;
using Domain.Entities.AddressesSpace.Heirs;
using Domain.Entities.BrandSpace;
using Domain.Entities.CategorySpace;
using Domain.Entities.IntermediateSpace;
using Domain.Entities.ProductSpace;
using Domain.Entities.SupplierSpace; 
using Domain.Entities.UserSpace;
using Domain.Entities.UserSpace.UserTypes;
using Domain.Models.LoyaltyProgramSpace.Discount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DefaultData 
    {


        public DefaultData(ModelBuilder modelBuilder)
        {
           

            // 2. Генерация базовых пользователей, адресов, поставщиков, категорий, брендов и пр.

            // Генерируем фиксированные идентификаторы для админа, клиентов, сотрудников и поставщиков
            Guid adminId = Guid.NewGuid();
            Guid customer1Id = Guid.NewGuid();
            Guid customer2Id = Guid.NewGuid();
            Guid employee1Id = Guid.NewGuid();

            Guid supplier1Id = Guid.NewGuid();
            Guid supplier2Id = Guid.NewGuid();
            Guid supplier3Id = Guid.NewGuid();

            // Адреса пользователей
            modelBuilder.Entity<UserAddress>().HasData(
                new UserAddress { Id = Guid.NewGuid(), UserId = customer1Id, Street = "ул. Ленина, д. 10", City = "Москва", PostalCode = "101000", Country = "Россия", IsPrimary = true, CreatedByUserId = adminId },
                new UserAddress { Id = Guid.NewGuid(), UserId = customer2Id, Street = "ул. Советская, д. 15", City = "Санкт-Петербург", PostalCode = "190000", Country = "Россия", IsPrimary = true, CreatedByUserId = adminId },
                new UserAddress { Id = Guid.NewGuid(), UserId = customer2Id, Street = "ул. Центральная, д. 20", City = "Новосибирск", PostalCode = "630000", Country = "Россия", IsPrimary = false, CreatedByUserId = adminId },
                new UserAddress { Id = Guid.NewGuid(), UserId = customer2Id, Street = "ул. Пушкина, д. 25", City = "Екатеринбург", PostalCode = "620000", Country = "Россия", IsPrimary = false, CreatedByUserId = adminId }
            );

            // Адреса поставщиков
            modelBuilder.Entity<SupplierAddress>().HasData(
                new SupplierAddress { Id = Guid.NewGuid(), SupplierId = supplier2Id, Street = "МКАД, 19-й километр, вл20с1", City = "Москва", PostalCode = "101000", Country = "Россия", IsPrimary = true, CreatedByUserId = adminId },
                new SupplierAddress { Id = Guid.NewGuid(), SupplierId = supplier3Id, Street = "ул. Калинина, д. 5", City = "Санкт-Петербург", PostalCode = "190000", Country = "Россия", IsPrimary = true, CreatedByUserId = adminId },
                new SupplierAddress { Id = Guid.NewGuid(), SupplierId = supplier3Id, Street = "ул. Жукова, д. 8", City = "Новосибирск", PostalCode = "630000", Country = "Россия", IsPrimary = false, CreatedByUserId = adminId },
                new SupplierAddress { Id = Guid.NewGuid(), SupplierId = supplier3Id, Street = "ул. Тверская, д. 40", City = "Екатеринбург", PostalCode = "620000", Country = "Россия", IsPrimary = false, CreatedByUserId = adminId }
            );

            // Поставщики
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = supplier1Id, Name = "Default Supplier", CreatedByUserId = adminId },
                new Supplier { Id = supplier2Id, Name = "Южные ворота", CreatedByUserId = adminId },
                new Supplier { Id = supplier3Id, Name = "РМС", CreatedByUserId = adminId }
            );

            // WishList (обычно для пользователя, но для примера создадим несколько)
            Guid wishList1Id = Guid.NewGuid();
            Guid wishList2Id = Guid.NewGuid();
            Guid wishList3Id = Guid.NewGuid();
            Guid wishList4Id = Guid.NewGuid();

            WishList[] wishLists =
            {
            new WishList { Id = wishList1Id },
            new WishList { Id = wishList2Id },
            new WishList { Id = wishList3Id },
            new WishList { Id = wishList4Id },
        };
            modelBuilder.Entity<WishList>().HasData(wishLists);

            // Пользователи
            User[] users = new[]
            {
                new User
                {
                    Id = adminId,
                    UserType = UserType.Admin,
                    PhoneNumber = "+79260128187",
                    FirstName = "Иван",
                    LastName = "Калмыков",
                    Patronymic = "Алексеевич",
                    DateOfBirth = new DateTime(1996, 10, 17),
                    Email = "admin@kalmykov-group.ru",
                    PasswordHash = "hashed_password_1",
                    IsActive = true,
                    AddressType = AddressType.User,
                    CreatedByUserId = adminId,
                    WishListId = wishList1Id
                },
                new User
                {
                    Id = employee1Id,
                    UserType = UserType.Employee,
                    PhoneNumber = "+1122334455",
                    FirstName = "Дмитрий",
                    LastName = "Сидоров",
                    Patronymic = "Алексеевич",
                    DateOfBirth = new DateTime(1988, 7, 30),
                    Email = "dmitry.sidorov@example.com",
                    PasswordHash = "hashed_password_3",
                    IsActive = true,
                    AddressType = AddressType.Supplier,
                    CreatedByUserId = adminId,
                    WishListId = wishList2Id
                },
                new User
                {
                    Id = customer1Id,
                    UserType = UserType.Customer,
                    PhoneNumber = "+1987654321",
                    FirstName = "Анна",
                    LastName = "Смирнова",
                    Patronymic = "Игоревна",
                    DateOfBirth = new DateTime(1995, 10, 22),
                    Email = "anna.smirnova@example.com",
                    PasswordHash = "hashed_password_2",
                    IsActive = true,
                    AddressType = AddressType.User,
                    CreatedByUserId = adminId,
                    WishListId = wishList3Id
                },
                new User
                {
                    Id = customer2Id,  
                    UserType = UserType.Customer,
                    PhoneNumber = "+79260128187",
                    FirstName = "Иван",
                    LastName = "Калмыков",
                    Patronymic = "Алексеевич",
                    DateOfBirth = new DateTime(1996, 10, 17),
                    Email = "customer2@kalmykov-group.ru",
                    PasswordHash = "hashed_password_1123",
                    IsActive = true,
                    AddressType = AddressType.User,
                    CreatedByUserId = adminId,
                    WishListId = wishList4Id
                },
            };
            modelBuilder.Entity<User>().HasData(users);

            var rootCategoryId = Guid.NewGuid();

            modelBuilder.Entity<Category>().HasData(
             
            );
        
            // Основные категории
            var smartphonesId = Guid.Parse("12345678-abcd-1234-ef00-1234567890ab");
            var buttonPhonesId = Guid.NewGuid();
            var accessoriesId = Guid.NewGuid();
            var raciiId = Guid.NewGuid();
 
            // Подкатегории аксессуаров
            var casesId = Guid.NewGuid();
            var screenProtectorsId = Guid.NewGuid();
            var chargersId = Guid.NewGuid();
            var cablesId = Guid.NewGuid();
            var headphonesId = Guid.NewGuid();

            List<Guid> listCategoryGuid = new List<Guid>();

            for (int i = 0; i < 19; i++)
            {
                listCategoryGuid.Add(Guid.NewGuid());
            }

            listCategoryGuid.Add(smartphonesId);
            listCategoryGuid.Add(buttonPhonesId);
            listCategoryGuid.Add(accessoriesId);
            listCategoryGuid.Add(raciiId);

            listCategoryGuid.Add(casesId);
            listCategoryGuid.Add(screenProtectorsId);
            listCategoryGuid.Add(chargersId);
            listCategoryGuid.Add(cablesId);
            listCategoryGuid.Add(headphonesId);

            modelBuilder.Entity<Category>().HasData(
                // Основная категория "Телефоны"
                new Category { Id = rootCategoryId, Index = 0, Level = 0, Name = "Телефоны", Path = "phones", ParentCategoryId = null, CreatedByUserId = adminId },

                new Category { Id = smartphonesId, Index = 0, Level = 1, Name = "Смартфоны", Path = "smartphones", ParentCategoryId = rootCategoryId, CreatedByUserId = adminId },
                new Category { Id = buttonPhonesId, Index = 1, Level = 1, Name = "Кнопочные телефоны", Path = "button-phones", ParentCategoryId = rootCategoryId, CreatedByUserId = adminId },
                new Category { Id = accessoriesId, Index = 2, Level = 1, Name = "Аксессуары", Path = "accessories", ParentCategoryId = rootCategoryId, CreatedByUserId = adminId },
                new Category { Id = raciiId, Index = 3, Level = 1, Name = "Рации", Path = "walkie-talkies", ParentCategoryId = rootCategoryId, CreatedByUserId = adminId },

                // Подкатегории смартфонов
                new Category { Id = listCategoryGuid[0], Index = 0, Level = 2, Name = "Флагманы", Path = "flagships", ParentCategoryId = smartphonesId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[1], Index = 1, Level = 2, Name = "Средний сегмент", Path = "mid-range", ParentCategoryId = smartphonesId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[2], Index = 2, Level = 2, Name = "Бюджетные модели", Path = "budget-models", ParentCategoryId = smartphonesId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[3], Index = 3, Level = 2, Name = "Игровые смартфоны", Path = "gaming-smartphones", ParentCategoryId = smartphonesId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[4], Index = 4, Level = 2, Name = "Компактные смартфоны", Path = "compact-smartphones", ParentCategoryId = smartphonesId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[5], Index = 5, Level = 2, Name = "Топовые", Path = "top-models", ParentCategoryId = smartphonesId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[6], Index = 6, Level = 2, Name = "Бизнес", Path = "business", ParentCategoryId = smartphonesId, CreatedByUserId = adminId },

                // Подкатегории кнопочных телефонов
                new Category { Id = listCategoryGuid[7], Index = 0, Level = 2, Name = "Классические", Path = "classic", ParentCategoryId = buttonPhonesId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[8], Index = 1, Level = 2, Name = "Телефоны для пожилых", Path = "phones-for-elderly", ParentCategoryId = buttonPhonesId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[9], Index = 2, Level = 2, Name = "Защищенные кнопочные телефоны", Path = "rugged-button-phones", ParentCategoryId = buttonPhonesId, CreatedByUserId = adminId },

                // Подкатегории аксессуаров
                new Category { Id = casesId, Index = 0, Level = 2, Name = "Чехлы", Path = "cases", ParentCategoryId = accessoriesId, CreatedByUserId = adminId },
                new Category { Id = screenProtectorsId, Index = 1, Level = 2, Name = "Защитные стекла и пленки", Path = "screen-protectors", ParentCategoryId = accessoriesId, CreatedByUserId = adminId },
                new Category { Id = chargersId, Index = 2, Level = 2, Name = "Зарядные устройства", Path = "chargers", ParentCategoryId = accessoriesId, CreatedByUserId = adminId },
                new Category { Id = cablesId, Index = 3, Level = 2, Name = "Кабели и адаптеры", Path = "cables-and-adapters", ParentCategoryId = accessoriesId, CreatedByUserId = adminId },
                new Category { Id = headphonesId, Index = 4, Level = 2, Name = "Наушники и гарнитуры", Path = "headphones-and-headsets", ParentCategoryId = accessoriesId, CreatedByUserId = adminId },

                // Подкатегории чехлов
                new Category { Id = listCategoryGuid[10], Index = 0, Level = 3, Name = "Силиконовые", Path = "silicone", ParentCategoryId = casesId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[11], Index = 1, Level = 3, Name = "Кожаные", Path = "leather", ParentCategoryId = casesId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[12], Index = 2, Level = 3, Name = "Противоударные", Path = "shockproof", ParentCategoryId = casesId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[13], Index = 3, Level = 3, Name = "Книжки", Path = "book-style", ParentCategoryId = casesId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[14], Index = 4, Level = 3, Name = "С прозрачной крышкой", Path = "with-transparent-cover", ParentCategoryId = casesId, CreatedByUserId = adminId },

                // Подкатегории зарядных устройств
                new Category { Id = listCategoryGuid[15], Index = 0, Level = 3, Name = "Беспроводные", Path = "wireless", ParentCategoryId = chargersId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[16], Index = 1, Level = 3, Name = "Сетевые", Path = "network", ParentCategoryId = chargersId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[17], Index = 2, Level = 3, Name = "Автомобильные", Path = "car", ParentCategoryId = chargersId, CreatedByUserId = adminId },
                new Category { Id = listCategoryGuid[18], Index = 3, Level = 3, Name = "Магнитные", Path = "magnetic", ParentCategoryId = chargersId, CreatedByUserId = adminId }
            );




            var brand1Id = Guid.NewGuid();
            var brand2Id = Guid.NewGuid();
            var brand3Id = Guid.NewGuid();
            var brand4Id = Guid.NewGuid();
            var brand5Id = Guid.NewGuid();

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = brand1Id, Name = "Sony", CreatedByUserId = adminId },
                new Brand { Id = brand2Id, Name = "Samsung", CreatedByUserId = adminId },
                new Brand { Id = brand3Id, Name = "Nike", CreatedByUserId = adminId },
                new Brand { Id = brand4Id, Name = "Adidas", CreatedByUserId = adminId },
                new Brand { Id = brand5Id, Name = "Apple", CreatedByUserId = adminId }
            );

            // Продукты
            // Создадим 10 000 продуктов, для каждого:
            // - Генерируем уникальный ProductId
            // - Создаем две записи в ProductTranslation (ru и en)
            // - Создаем WishList для продукта и связь через WishListProduct

            var productsList = new List<Product>(); 
            var wishListsList = new List<WishList>();
            var wishListProductsList = new List<WishListProduct>();

             

            List<Guid> RecommendedGroupGuids = new List<Guid>();

            RecommendedGroupGuids.Add(Guid.NewGuid());
            RecommendedGroupGuids.Add(Guid.NewGuid());
            RecommendedGroupGuids.Add(Guid.NewGuid());
            RecommendedGroupGuids.Add(Guid.NewGuid());
            RecommendedGroupGuids.Add(Guid.NewGuid());

            // 🔹 Seed Data (5 групп)
            var recommendedGroups = new List<RecommendedGroup>
            {
                new RecommendedGroup { Id = RecommendedGroupGuids[0], Title = "Популярные товары", Background = "#dfcfd2", Color = "#3a3b36", CreatedByUserId = adminId},
                new RecommendedGroup { Id = RecommendedGroupGuids[1], Title = "Горячие скидки", Background = "#ddd2c8", Color = "#3a3b36",  CreatedByUserId = adminId },
                new RecommendedGroup { Id = RecommendedGroupGuids[2], Title = "Лучшая электроника" , Background = "#d7defc", Color = "#3a3b36",  CreatedByUserId = adminId},
                new RecommendedGroup { Id = RecommendedGroupGuids[3], Title = "Игровая зона" , Background = "#f8d1de", Color = "#3a3b36",  CreatedByUserId = adminId},
                new RecommendedGroup { Id = RecommendedGroupGuids[4], Title = "Спорт и активный отдых", Background = "#e6daba", Color = "#3a3b36",  CreatedByUserId = adminId }
            };

            modelBuilder.Entity<RecommendedGroup>().HasData(recommendedGroups);


            string[] productNames = new string[]
                    {
                        "Ультрасовременный Смартфон с Тройной Камерой и OLED Дисплеем",
                        "Многофункциональная Кофемашина с Автоматическим Приготовлением",
                        "Игровая Механическая Клавиатура с RGB Подсветкой и Макросами",
                        "Профессиональный Фотокамера с 4K Записью и Оптической Стабилизацией",
                        "Сверхмощный Игровой Ноутбук с Видеокартой Последнего Поколения",
                        "Инновационные Беспроводные Наушники с Активным Шумоподавлением",
                        "Робот-Пылесос с Лазерной Навигацией и Управлением через Приложение",
                        "Электрический Самокат с Длинным Запасом Хода и Амортизацией",
                        "Автоматическая Сушильная Машина с Технологией Энергосбережения",
                        "Смарт-Телевизор с 4K UHD Экраном и Встроенным Голосовым Ассистентом",
                        "Профессиональный 3D Принтер с Высокой Точностью Печати",
                        "Эргономичное Компьютерное Кресло с Регулируемой Поддержкой Спины",
                        "Премиальный Электрический Гриль с Антипригарным Покрытием",
                        "Многофункциональный Умный Чайник с Регулируемым Нагревом",
                        "Высокотехнологичная Видеокарта с Охлаждением и Подсветкой",
                        "Геймерский Монитор с 240Hz Частотой Обновления и HDR Поддержкой",
                        "Компактный Дрон с Камерой 4K и Автономным Полетом",
                        "Электронная Книга с E-Ink Экраном и Регулируемой Подсветкой",
                        "Портативная Колонка с Мощным Басом и Влагозащитой",
                        "Смарт-Часы с GPS Трекером и Мониторингом Сердечного Ритма"
                    };

            List<Guid> guidsProduct = new List<Guid>();

            Random random = new Random();

            for (int i = 1; i <= 1000; i++)
            {
                Guid productId = Guid.NewGuid();
                Guid productWishListId = Guid.NewGuid();

                if(guidsProduct.Count() < recommendedGroups.Count * 4)
                {
                    guidsProduct.Add(productId);
                }

                decimal number = 10 + random.Next(0, 200000); // Исходное число
                decimal minPercent = 10m; // Минимальный процент
                decimal maxPercent = 50m; // Максимальный процент

                decimal randomPercent = random.Next((int)minPercent, (int)maxPercent + 1); // Генерация числа от 10 до 50
                decimal newNumber = number + (number * randomPercent / 100); // Прибавление процента


                // Продукт: рандомная цена, случайное распределение по категориям и брендам
                var product = new Product
                {
                    Id = productId,
                    CategoryId = listCategoryGuid[random.Next(0, listCategoryGuid.Count - 1)],
                    BrandId = (i % 5 == 0) ? brand5Id : (i % 4 == 0) ? brand4Id : (i % 3 == 0) ? brand3Id : (i % 2 == 0) ? brand2Id : brand1Id,
                    Price = 1000 + i,
                    Name = i < productNames.Length ? productNames[i] : $"Product {i}",
                    Url = "",
                    OriginalPrice = newNumber,
                    CreatedByUserId = adminId,
                    SupplierId = (i % 3 == 0) ? supplier3Id : (i % 2 == 0) ? supplier2Id : supplier1Id
                };
                productsList.Add(product);
                     
            }

            modelBuilder.Entity<Product>().HasData(productsList);  

            List<RecommendedGroupProduct> RecommendedGroupProducts = new List<RecommendedGroupProduct >();


            for (int i = 0; i < recommendedGroups.Count; i++)//1,2,3,4,5
            {
                for(int j = 0; j < 4; j++)//1,2,3,4
                {
                    RecommendedGroupProducts.Add(new RecommendedGroupProduct() {
                        ProductId = guidsProduct[(i + 1 * j + 1) - 1],
                        RecommendedGroupId = recommendedGroups[i].Id,
                    });
                }
            }

            modelBuilder.Entity<RecommendedGroupProduct>().HasData(RecommendedGroupProducts);

            // Скидки и связи скидок, если необходимо – можно добавить аналогично
        }

     

     
    }
}
