using Domain.Entities.ProductSpace;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.ProductSpace;

public class SellerOfferImageConf : AuditableEntityConf<SellerOfferImage>
{
    public override void Configure(EntityTypeBuilder<SellerOfferImage> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("seller_offer_images");
        
        
        builder.HasKey(pi => pi.Id); // Первичный ключ
        
        builder.Property(pi => pi.Id)
            .HasColumnName("id") 
            .ValueGeneratedOnAdd();

        builder.Property(pi => pi.SellerOfferId)
            .HasColumnName("seller_offer_id") 
            .IsRequired();

        builder.Property(pi => pi.Index)
            .HasColumnName("index") 
            .IsRequired();

        builder.Property(pi => pi.OriginalExtension)
            .HasColumnName("original_extension") 
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(pi => pi.StoragePath)
            .HasColumnName("storage_path") 
            .HasMaxLength(255)
            .IsRequired();

        // Связь с Product (многие к одному)
        builder.HasOne(pi => pi.SellerOffer)
            .WithMany(p => p.Images)
            .HasForeignKey(pi => pi.SellerOfferId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}