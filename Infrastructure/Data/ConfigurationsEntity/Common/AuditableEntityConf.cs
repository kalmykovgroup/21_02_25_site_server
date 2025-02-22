using Domain.Entities.Common;
using Domain.Entities.Common.Interfaces;
using Domain.Entities.UserSpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.Common
{
    public class AuditableEntityConf<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : AuditableEntity<TEntity>
    {

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            string provider = "PostgreSql";
             

            var isSqlServer = provider?.Contains("SqlServer") ?? false;
            var isMySql = provider?.Contains("MySql") ?? false;
            var isPostgreSql = provider?.Contains("PostgreSql") ?? false;

            builder.Property(e => e.CreatedByUserId)
                 .IsRequired()
              .HasColumnName("created_by_user_id");

            builder.Property(e => e.UpdatedByUserId)
               .HasColumnName("updated_by_user_id");

            builder.Property(e => e.DeletedByUserId)
               .HasColumnName("deleted_by_user_id");

            // Настройка CreatedAt
            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired()
                .HasColumnType(GetDateTimeType(isSqlServer, isPostgreSql))
                .HasDefaultValueSql(GetUtcDateFunction(provider))
                .HasComment("Дата и время создания записи (UTC)");


            // Настройка UpdatedAt и DeletedAt
            builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
                .HasColumnType(GetDateTimeType(isSqlServer, isPostgreSql));

            builder.Property(e => e.DeletedAt)
            .HasColumnName("deleted_at")
                .HasColumnType(GetDateTimeType(isSqlServer, isPostgreSql));

            // Настройка IsDeleted
            builder.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted")
                .HasColumnType(GetBooleanType(provider))
                .HasDefaultValue(false);

            // Настройка RowVersion
            builder.Property(b => b.RowVersion)
                .HasColumnName("row_version")
                .IsRowVersion() // Помечает поле как версию строки
                .HasColumnType("bytea"); // Указывает, что это бинарное поле в PostgreSQL 



            // Связь с пользователем-создателем
            builder.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // Связь с пользователем-редактором
            builder.HasOne(e => e.UpdatedByUser)
                .WithMany()
                .HasForeignKey(e => e.UpdatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с пользователем-удалителем
            builder.HasOne(e => e.DeletedByUser)
                .WithMany()
                .HasForeignKey(e => e.DeletedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(a => a.IsDeleted)
           .HasDatabaseName($"IX_{builder.Metadata.GetTableName()}_IsDeleted")
           .HasFilter("\"is_deleted\" = false"); // PostgreSQL 


            // Индексы для внешних ключей
            builder.HasIndex(nameof(AuditableEntity<TEntity>.CreatedByUserId))
                .HasDatabaseName($"IX_{builder.Metadata.GetTableName()}_CreatedBy");

            builder.HasIndex(nameof(AuditableEntity<TEntity>.UpdatedByUserId))
                .HasDatabaseName($"IX_{builder.Metadata.GetTableName()}_UpdatedBy");

            builder.HasIndex(nameof(AuditableEntity<TEntity>.DeletedByUserId))
                .HasDatabaseName($"IX_{builder.Metadata.GetTableName()}_DeletedBy");
        }

      
         


        // Вспомогательные методы
        private  string GetDateTimeType(bool isSqlServer, bool isPostgreSql)
        {
            if (isSqlServer) return "datetime2";
            if (isPostgreSql) return "timestamp with time zone";
            return "datetime(6)"; // Для MySQL
        }

        private  string GetUtcDateFunction(string provider)
        {
            return provider switch
            {
                var p when p.Contains("SqlServer") => "GETUTCDATE()",
                var p when p.Contains("MySql") => "UTC_TIMESTAMP()",
                var p when p.Contains("PostgreSql") => "CURRENT_TIMESTAMP",
                _ => throw new NotSupportedException("Unsupported database provider.")
            };
        }

        private  string GetBooleanType(string provider)
        {
            return provider switch
            {
                var p when p.Contains("SqlServer") => "bit",
                var p when p.Contains("MySql") => "tinyint(1)",
                var p when p.Contains("PostgreSql") => "boolean",
                _ => throw new NotSupportedException("Unsupported database provider.")
            };
        }

        private  string GetRowVersionType(string provider)
        {
            return provider switch
            {
                var p when p.Contains("SqlServer") => "rowversion",
                var p when p.Contains("MySql") => "BLOB",
                var p when p.Contains("PostgreSql") => "bytea",
                _ => throw new NotSupportedException("Unsupported database provider.")
            };
        }

    }
}
