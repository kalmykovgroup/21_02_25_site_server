using Domain.Entities.ProductSpace;
using Domain.Entities.OrderSpace;
using Infrastructure.Data.ConfigurationsEntity.ProductSpace;
using Domain.Entities.AddressesSpace;
using Domain.Entities.UserSpace;
using Domain.Entities.CategorySpace;
using Domain.Entities.Common;
using Domain.Entities.IntermediateSpace; 
using Microsoft.EntityFrameworkCore;  
using Infrastructure.Data.ConfigurationsEntity.Common;
using Infrastructure.Data.ConfigurationsEntity.UserSpace;
using Infrastructure.Data.ConfigurationsEntity;
using Infrastructure.Data.ConfigurationsEntity.AddressesSpace;
using Infrastructure.Data.ConfigurationsEntity.CategorySpace;
using Infrastructure.Data.ConfigurationsEntity.IntermediateSpaceConf;
using Infrastructure.Data.ConfigurationsEntity.OrderSpace;
using Infrastructure.Data.ConfigurationsEntity.StatusesSpace; 
using Domain.Entities.UserSpace.UserTypes;
using Infrastructure.Data.ConfigurationsEntity.UserSpace.UserTypes;
using Domain.Entities.AnalyticsSpace;
using Infrastructure.Data.ConfigurationsEntity.BrandSpaceConf;
using Infrastructure.Data.ConfigurationsEntity.InventorySpaceConf;
using Infrastructure.Data.ConfigurationsEntity.LoyaltyProgramSpace.Bundle;
using Infrastructure.Data.ConfigurationsEntity.LoyaltyProgramSpace.CouponSpace;
using Infrastructure.Data.ConfigurationsEntity.LoyaltyProgramSpace.Discount;
using Infrastructure.Data.ConfigurationsEntity.LoyaltyProgramSpace.Loyalty;
using Infrastructure.Data.ConfigurationsEntity.NotificationsSpaceConf;
using Infrastructure.Data.ConfigurationsEntity.StorageSpace.Heirs;
using Infrastructure.Data.ConfigurationsEntity.StorageSpace;
using Infrastructure.Data.ConfigurationsEntity.SupplierSpaceConf; 
using Infrastructure.Data.ConfigurationsEntity.AddressesSpace.Heirs;
using Infrastructure.Data.ConfigurationsEntity.PaymentSpace;
using Infrastructure.Data.ConfigurationsEntity.StatusesSpace.Heirs;
using Application.Common.Interfaces;
namespace Infrastructure.Data
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        private readonly ChangeLogInterceptor _changeLogInterceptor = new();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_changeLogInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
         

        public DbSet<Product> Products { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<WishListProduct> WishListProducts { get; set; }
        public DbSet<PhoneVerificationCode> PhoneVerificationCodes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RecommendedGroup> RecommendedGroups { get; set; }
        public DbSet<RecommendedGroupProduct> RecommendedGroupProducts { get; set; }
         


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SupplierAddressConf).Assembly);
             

            new DefaultData(modelBuilder);

        }
         
  

        public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            var transaction = await Database.BeginTransactionAsync(cancellationToken);
            return new EfTransaction(transaction);
        }

        public override int SaveChanges()
        {
            ConvertDateTimeToUtc();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ConvertDateTimeToUtc();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ConvertDateTimeToUtc()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                foreach (var property in entry.Properties)
                {
                    if (property.Metadata.ClrType == typeof(DateTime) || property.Metadata.ClrType == typeof(DateTime?))
                    {
                        if (property.CurrentValue is DateTime dt && dt.Kind != DateTimeKind.Utc)
                        {
                            // 🔹 Переводим в UTC правильно
                            property.CurrentValue = dt.ToUniversalTime();

                            // ✅ Выводим после изменения
                            Console.WriteLine($"\n\nProperty: {property.Metadata.Name}, Value (converted): {property.CurrentValue}, Kind: {((DateTime)property.CurrentValue).Kind}\n\n");
                        }
                    }
                }
            }
        }

    }
}
