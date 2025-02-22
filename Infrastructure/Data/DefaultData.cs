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
            var smartphonesId = Guid.NewGuid();
            var buttonPhonesId = Guid.NewGuid();
            var accessoriesId = Guid.NewGuid();

            modelBuilder.Entity<Category>().HasData(
              
            );

    
            // Подкатегории аксессуаров
            var casesId = Guid.NewGuid();
            var screenProtectorsId = Guid.NewGuid();
            var chargersId = Guid.NewGuid();
            var cablesId = Guid.NewGuid();
            var headphonesId = Guid.NewGuid();

            modelBuilder.Entity<Category>().HasData(
                  // Основная категория "Телефоны"
                new Category { Id = rootCategoryId, Name = "Телефоны", ParentCategoryId = null, Level = 0, CreatedByUserId = adminId }, 

                new Category { Id = smartphonesId, Name = "Смартфоны", ParentCategoryId = rootCategoryId, Level = 1, CreatedByUserId = adminId },
                new Category { Id = buttonPhonesId, Name = "Кнопочные телефоны", ParentCategoryId = rootCategoryId, Level = 1, CreatedByUserId = adminId },
                new Category { Id = accessoriesId, Name = "Аксессуары", ParentCategoryId = rootCategoryId, Level = 1, CreatedByUserId = adminId },

                // Подкатегории смартфонов
                new Category { Id = casesId, Name = "Флагманы", ParentCategoryId = smartphonesId, Level = 2, CreatedByUserId = adminId},
                new Category { Id = screenProtectorsId, Name = "Средний сегмент", ParentCategoryId = smartphonesId, Level = 2, CreatedByUserId = adminId },
                new Category { Id = chargersId, Name = "Бюджетные модели", ParentCategoryId = smartphonesId, Level = 2, CreatedByUserId = adminId },
                new Category { Id = cablesId, Name = "Игровые смартфоны", ParentCategoryId = smartphonesId, Level = 2, CreatedByUserId = adminId },
                new Category { Id = headphonesId, Name = "Компактные смартфоны", ParentCategoryId = smartphonesId, Level = 2, CreatedByUserId = adminId },

                 // Подкатегории кнопочных телефонов
                 new Category { Id = Guid.NewGuid(), Name = "Классические", ParentCategoryId = buttonPhonesId, Level = 2, CreatedByUserId = adminId },
                 new Category { Id = Guid.NewGuid(), Name = "Телефоны для пожилых", ParentCategoryId = buttonPhonesId, Level = 2, CreatedByUserId = adminId },
                 new Category { Id = Guid.NewGuid(), Name = "Защищенные кнопочные телефоны", ParentCategoryId = buttonPhonesId, Level = 2, CreatedByUserId = adminId },

                // Подкатегории чехлов
                new Category { Id = Guid.NewGuid(), Name = "Силиконовые", ParentCategoryId = casesId, Level = 3, CreatedByUserId = adminId },
                new Category { Id = Guid.NewGuid(), Name = "Кожаные", ParentCategoryId = casesId, Level = 3, CreatedByUserId = adminId },
                new Category { Id = Guid.NewGuid(), Name = "Противоударные", ParentCategoryId = casesId, Level = 3, CreatedByUserId = adminId },
                new Category { Id = Guid.NewGuid(), Name = "Книжки", ParentCategoryId = casesId, Level = 3, CreatedByUserId = adminId },
                new Category { Id = Guid.NewGuid(), Name = "С прозрачной крышкой", ParentCategoryId = casesId, Level = 3, CreatedByUserId = adminId },

                // Подкатегории зарядных устройств
                new Category { Id = Guid.NewGuid(), Name = "Беспроводные", ParentCategoryId = chargersId, Level = 3, CreatedByUserId = adminId },
                new Category { Id = Guid.NewGuid(), Name = "Сетевые", ParentCategoryId = chargersId, Level = 3, CreatedByUserId = adminId },
                new Category { Id = Guid.NewGuid(), Name = "Автомобильные", ParentCategoryId = chargersId, Level = 3, CreatedByUserId = adminId },
                new Category { Id = Guid.NewGuid(), Name = "Магнитные", ParentCategoryId = chargersId, Level = 3, CreatedByUserId = adminId }
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


            Random random = new Random();

            for (int i = 1; i <= 1000; i++)
            {
                Guid productId = Guid.NewGuid();
                Guid productWishListId = Guid.NewGuid();

                decimal number = 10 + random.Next(0, 200000); // Исходное число
                decimal minPercent = 10m; // Минимальный процент
                decimal maxPercent = 50m; // Максимальный процент

                decimal randomPercent = random.Next((int)minPercent, (int)maxPercent + 1); // Генерация числа от 10 до 50
                decimal newNumber = number + (number * randomPercent / 100); // Прибавление процента


                // Продукт: рандомная цена, случайное распределение по категориям и брендам
                var product = new Product
                {
                    Id = productId,
                    CategoryId = (i % 2 == 0) ? smartphonesId : buttonPhonesId,
                    BrandId = (i % 5 == 0) ? brand5Id : (i % 4 == 0) ? brand4Id : (i % 3 == 0) ? brand3Id : (i % 2 == 0) ? brand2Id : brand1Id,
                    Price = 1000 + i,
                    Name = $"Product {i}",
                    Url = "",
                    OriginalPrice = newNumber,
                    CreatedByUserId = adminId,
                    SupplierId = (i % 3 == 0) ? supplier3Id : (i % 2 == 0) ? supplier2Id : supplier1Id
                };
                productsList.Add(product);

               

                

                
            }

            modelBuilder.Entity<Product>().HasData(productsList);  

            // Скидки и связи скидок, если необходимо – можно добавить аналогично
        }

     

     
    }
}
