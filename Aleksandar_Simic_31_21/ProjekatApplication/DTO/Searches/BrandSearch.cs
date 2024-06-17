﻿using ProjekatApplication.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.DTO.Searches
{
    public class BrandSearch : PagedSearch
    {
        public string Name { get; set; }
        public OrderByBrandName? OrderByName { get; set; }
    }
    public enum OrderByBrandName
    {
        asc,
        desc
    }
}
