﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDomain
{
    public class UserUseCase
    {
        public int UserId { get; set; }
        public int UseCaseId { get; set; }
        public virtual User User { get; set; }
    }
}
