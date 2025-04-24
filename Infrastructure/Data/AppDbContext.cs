using Domain.Entities.ProductSpace; 
using Domain.Entities.UserSpace;
using Domain.Entities.CategorySpace; 
using Domain.Entities.IntermediateSpace; 
using Microsoft.EntityFrameworkCore;   
using Infrastructure.Data.ConfigurationsEntity.AddressesSpace.Heirs; 
using Application.Common.Interfaces;
using AutoMapper;
using Infrastructure.Data.DbInitializer;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        private readonly ChangeLogInterceptor _changeLogInterceptor = new();

        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AppDbContext(DbContextOptions<AppDbContext> options, IMapper mapper, ILogger<AppDbContext> logger) : base(options)
        {
            _mapper = mapper;
            _logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_changeLogInterceptor);
              //  .EnableSensitiveDataLogging(); // Включает логирование чувствительных данных;
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
        
        public DbSet<SellerOffer> SellerOffers { get; set; }
         
 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SupplierAddressConf).Assembly);
            
            Initializer.Set(modelBuilder, _mapper, _logger);
            
            base.OnModelCreating(modelBuilder);
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
