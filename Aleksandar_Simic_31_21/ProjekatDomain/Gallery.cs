using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDomain
{
    public class Gallery : Entity
    {
        public string PathName { get; set; }
        public Product Product { get; set; }
    }
}
