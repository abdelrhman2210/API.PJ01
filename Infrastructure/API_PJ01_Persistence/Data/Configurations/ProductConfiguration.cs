using API_PJ01_Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_PJ01_Persistence.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).HasColumnType(typeName: "varchar").HasMaxLength(maxLength: 256);
            builder.Property(P => P.Description).HasColumnType(typeName: "varchar").HasMaxLength(maxLength: 512);
            builder.Property(P => P.PictureUrl).HasColumnType(typeName: "varchar").HasMaxLength(maxLength: 256);
            builder.Property(P => P.Price).HasColumnType(typeName: "decimal(18,2)");

            builder.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(P => P.BrandId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(P => P.Type)
                .WithMany()
                .HasForeignKey(P => P.TypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
