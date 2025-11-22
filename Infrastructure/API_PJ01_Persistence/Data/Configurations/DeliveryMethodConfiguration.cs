using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_PJ01_Persistence.Data.Configurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(D => D.ShortName).HasColumnType(typeName: "varchar").HasMaxLength(maxLength: 128);
            builder.Property(D => D.Description).HasColumnType(typeName: "varchar").HasMaxLength(maxLength: 256);
            builder.Property(D => D.DeliveryTime).HasColumnType(typeName: "varchar").HasMaxLength(maxLength: 128);
            builder.Property(D => D.Price).HasColumnType(typeName: "decimal(18,2)");
        }
    }
}
