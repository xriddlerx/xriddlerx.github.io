using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDomain
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public ICollection<BrandCategory> BrandCategories { get; set; } = new HashSet<BrandCategory>();
    }
}
