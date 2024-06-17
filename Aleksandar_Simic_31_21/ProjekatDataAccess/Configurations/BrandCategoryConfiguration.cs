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
    public class BrandCategoryConfiguration : IEntityTypeConfiguration<BrandCategory>
    {
        public void Configure(EntityTypeBuilder<BrandCategory> builder)
        {
            builder.HasOne(x => x.Category)
                    .WithMany(x => x.BrandCategories).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.Brand)
                    .WithMany(x => x.BrandCategories).HasForeignKey(x => x.BrandId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Products)
                    .WithOne(x => x.BrandCategory).HasForeignKey(x => x.BrandCategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
