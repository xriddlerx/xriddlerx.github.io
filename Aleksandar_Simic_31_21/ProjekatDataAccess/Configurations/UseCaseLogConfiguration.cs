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
    public class UseCaseLogConfiguration : IEntityTypeConfiguration<UseCaseLog>
    {
        public void Configure(EntityTypeBuilder<UseCaseLog> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(20);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(20);
            builder.Property(x => x.UseCaseName).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => new { x.FirstName, x.LastName, x.UseCaseName, x.ExecutedAt })
                   .IncludeProperties(x => x.UseCaseData);
        }
    }
}
