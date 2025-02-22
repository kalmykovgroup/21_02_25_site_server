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
         


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SupplierAddressConf).Assembly);

            /* #region AddressesSpace 
             //heirs
             modelBuilder.ApplyConfiguration(new SupplierAddressConf()); 
             modelBuilder.ApplyConfiguration(new UserAddressConf());  
             modelBuilder.ApplyConfiguration(new AddressConfig());

             #endregion


             #region AnalyticsSpace

             modelBuilder.ApplyConfiguration(new CustomerPreferenceConf());
             modelBuilder.ApplyConfiguration(new SalesReportConf());
             modelBuilder.ApplyConfiguration(new SalesReportItemConf());

             modelBuilder.ApplyConfiguration(new ViewHistoryConf());
             modelBuilder.ApplyConfiguration(new VisitorActionConf());
             modelBuilder.ApplyConfiguration(new VisitorSessionConf());



             #endregion

             #region BrandSpaceConf

             modelBuilder.ApplyConfiguration(new BrandConf());


             #endregion


             #region CategorySpace

             modelBuilder.ApplyConfiguration(new CategoryConfig());
             #endregion


             #region Common

             modelBuilder.ApplyConfiguration(new HistoryConf());
             #endregion


             #region IntermediateSpaceConf

             modelBuilder.ApplyConfiguration(new CustomerPreferenceBrandConf());
             modelBuilder.ApplyConfiguration(new CustomerPreferenceCategoryConf());
             modelBuilder.ApplyConfiguration(new OrderCouponConf());

             modelBuilder.ApplyConfiguration(new ProductDiscountConf());

             modelBuilder.ApplyConfiguration(new ProductTagConf());
             modelBuilder.ApplyConfiguration(new RolePermissionConf());
             modelBuilder.ApplyConfiguration(new SupplierBrandConf());

             modelBuilder.ApplyConfiguration(new UserPermissionConf());
             modelBuilder.ApplyConfiguration(new UserRoleConf());


             modelBuilder.ApplyConfiguration(new WishListProductConf());


             #endregion

             #region InventorySpaceConf

             modelBuilder.ApplyConfiguration(new ProductStockConf());
             modelBuilder.ApplyConfiguration(new SupplyProductConf());
             modelBuilder.ApplyConfiguration(new SupplyConf());
             modelBuilder.ApplyConfiguration(new WarehouseConf());

             #endregion

             #region LoyaltyProgramSpace

             //Bundle

             modelBuilder.ApplyConfiguration(new BundleItemConf());
             modelBuilder.ApplyConfiguration(new DiscountBundleConf());

             //Coupon

             modelBuilder.ApplyConfiguration(new CouponConf());
             modelBuilder.ApplyConfiguration(new CouponUsageConf());

             //Discount

             modelBuilder.ApplyConfiguration(new DiscountConditionConf());
             modelBuilder.ApplyConfiguration(new DiscountRuleConf());
             modelBuilder.ApplyConfiguration(new DiscountUsageConf());

             //Loyalty

             modelBuilder.ApplyConfiguration(new CustomerLoyaltyConf());
             modelBuilder.ApplyConfiguration(new LoyaltyBonusConf());
             modelBuilder.ApplyConfiguration(new LoyaltyProgramConf());
             modelBuilder.ApplyConfiguration(new LoyaltyTierConf()); 

             #endregion


             #region NotificationsSpaceConf

             modelBuilder.ApplyConfiguration(new NotificationConf());

             #endregion

             #region OrderSpace

             modelBuilder.ApplyConfiguration(new OrderConf());
             modelBuilder.ApplyConfiguration(new OrderHistoryConf());
             modelBuilder.ApplyConfiguration(new OrderItemConf());

             modelBuilder.ApplyConfiguration(new ShippingDetailsConf());
             modelBuilder.ApplyConfiguration(new ShippingMethodConf());

             #endregion

             #region PaymentSpace

             modelBuilder.ApplyConfiguration(new CurrencyConf());
             modelBuilder.ApplyConfiguration(new PaymentCardConf());
             modelBuilder.ApplyConfiguration(new PaymentDetailsConf());

             modelBuilder.ApplyConfiguration(new PaymentMethodConf());
             modelBuilder.ApplyConfiguration(new PaymentTransactionConf());
             modelBuilder.ApplyConfiguration(new PaymentTypeConf());

             modelBuilder.ApplyConfiguration(new ReceiptConf());

             #endregion

             #region ProductSpace

             modelBuilder.ApplyConfiguration(new ProductAttributeConf());
             modelBuilder.ApplyConfiguration(new ProductConf());
             modelBuilder.ApplyConfiguration(new ProductVariantConf());

             modelBuilder.ApplyConfiguration(new ProductWaitConf());
             modelBuilder.ApplyConfiguration(new ReviewConf());
             modelBuilder.ApplyConfiguration(new TagConf());

             modelBuilder.ApplyConfiguration(new WishListConf());

             #endregion

             #region StatusesSpace

             modelBuilder.ApplyConfiguration(new OrderStatusConfig());
             modelBuilder.ApplyConfiguration(new ShippingStatusConfig());
             modelBuilder.ApplyConfiguration(new StatusConfig());

             #endregion



             #region StorageSpace

             //Heirs
             modelBuilder.ApplyConfiguration(new OrderFileConf());
             modelBuilder.ApplyConfiguration(new ProductFileConf());
             modelBuilder.ApplyConfiguration(new ReviewFileConf());

             modelBuilder.ApplyConfiguration(new FileStorageConf());

             #endregion


             #region StorageSpace

             modelBuilder.ApplyConfiguration(new SupplierConf());

             #endregion



             #region UserSpace

             //UserTypes         
             modelBuilder.ApplyConfiguration(new EmployeeConf());
             modelBuilder.ApplyConfiguration(new CustomerConf());
             modelBuilder.ApplyConfiguration(new AdminConf()); 

             modelBuilder.ApplyConfiguration(new CustomerGroupConf()); 
             modelBuilder.ApplyConfiguration(new PermissionConf());  

             modelBuilder.ApplyConfiguration(new PhoneVerificationCodeConf());  
             modelBuilder.ApplyConfiguration(new RoleConf());  
             modelBuilder.ApplyConfiguration(new UserConf());   

             #endregion*/

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
