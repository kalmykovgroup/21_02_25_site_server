using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.Common
{
    public class HistoryConf : IEntityTypeConfiguration<History>
    {
         
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.ToTable("histories");

            builder.HasKey(cl => cl.Id);

            builder.Property(cl => cl.TableName)
                .HasColumnName("table_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(cl => cl.RecordId)
                .HasColumnName("record_id")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(cl => cl.ChangeType)
                .HasColumnName("change_type")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(cl => cl.ChangeDate)
                .HasColumnName("change_date")
                .IsRequired();

            builder.Property(cl => cl.OldData)
                .HasColumnName("old_data")
                .HasColumnType("jsonb") // если PostgreSQL, иначе "nvarchar(max)" для MSSQL
                .IsRequired(false);

            builder.Property(cl => cl.NewData)
                .HasColumnName("new_data")
                .HasColumnType("jsonb")
                .IsRequired(false);

            builder.Property(cl => cl.UserId)
                .HasColumnName("user_id")
                .IsRequired(false);
        }
    }
}
