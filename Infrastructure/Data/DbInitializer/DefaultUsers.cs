using Domain.Entities.AddressesSpace;
using Domain.Entities.AddressesSpace.Heirs;
using Domain.Entities.ProductSpace;
using Domain.Entities.UserSpace;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DbInitializer;

public static class DefaultUsers
{
    public static Guid CreateDefaultUsers(this ModelBuilder modelBuilder)
    {
        Guid createdByUserId = Guid.NewGuid();
        
        //Избранное пользователей    
        WishList[] wishLists =
        {
            new WishList { Id = Guid.NewGuid() },
            new WishList { Id = Guid.NewGuid() },
            new WishList { Id = Guid.NewGuid() },
            new WishList { Id = Guid.NewGuid() },
        };
        modelBuilder.Entity<WishList>().HasData(wishLists);
        
           // Пользователи
            User[] users = new[]
            {
                new User
                {
                    Id = createdByUserId,
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
                    CreatedByUserId = createdByUserId,
                    WishListId = wishLists[0].Id
                },
                new User
                {
                    Id = Guid.NewGuid(),
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
                    CreatedByUserId = createdByUserId,
                    WishListId = wishLists[1].Id
                },
                new User
                {
                    Id = Guid.NewGuid(),
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
                    CreatedByUserId = createdByUserId,
                    WishListId = wishLists[2].Id
                },
                new User
                {
                    Id = Guid.NewGuid(),  
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
                    CreatedByUserId = createdByUserId,
                    WishListId = wishLists[3].Id
                },
            };
            modelBuilder.Entity<User>().HasData(users);
            
            // Адреса пользователей
            modelBuilder.Entity<UserAddress>().HasData(
                new UserAddress { Id = Guid.NewGuid(), UserId = users[0].Id, Street = "ул. Ленина, д. 10", City = "Москва", PostalCode = "101000", Country = "Россия", IsPrimary = true, CreatedByUserId = createdByUserId },
                new UserAddress { Id = Guid.NewGuid(), UserId = users[1].Id, Street = "ул. Советская, д. 15", City = "Санкт-Петербург", PostalCode = "190000", Country = "Россия", IsPrimary = true, CreatedByUserId = createdByUserId },
                new UserAddress { Id = Guid.NewGuid(), UserId = users[2].Id, Street = "ул. Центральная, д. 20", City = "Новосибирск", PostalCode = "630000", Country = "Россия", IsPrimary = false, CreatedByUserId = createdByUserId },
                new UserAddress { Id = Guid.NewGuid(), UserId = users[3].Id, Street = "ул. Пушкина, д. 25", City = "Екатеринбург", PostalCode = "620000", Country = "Россия", IsPrimary = false, CreatedByUserId = createdByUserId }
            ); 

            return createdByUserId;
    }
}