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
    public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.Property(x => x.Quantity).IsRequired();

            builder.HasOne(x => x.Order).WithMany(x => x.ProductOrders)
                .HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Product).WithMany(x => x.ProductOrders)
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
