using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDomain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<UserUseCase> UseCases { get; set; } = new HashSet<UserUseCase>();
    }
}
