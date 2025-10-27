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
    public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
    {    
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
                builder.Property(B => B.Name).HasColumnType(typeName: "varchar").HasMaxLength(maxLength: 256);
        }
    }
}
