﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Domain.Exceptions
{
    public class CategoryExceptions : Exception
    {
        public CategoryExceptions(string message) : base(message) { }     
    }
}
