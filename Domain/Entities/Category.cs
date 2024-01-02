﻿using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : BaseEntity   
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

    
}
