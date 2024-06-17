using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDataAccess.Configurations
{
    public class ProductCartConfiguration : IEntityTypeConfiguration<ProductCart>
    {
        public void Configure(EntityTypeBuilder<ProductCart> builder)
        {
            builder.Property(x => x.Quantity).IsRequired();

            builder.HasOne(x => x.Cart).WithMany(x => x.ProductCarts)
                .HasForeignKey(x=>x.CartId).OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.Product).WithMany(x => x.ProductCarts)
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
