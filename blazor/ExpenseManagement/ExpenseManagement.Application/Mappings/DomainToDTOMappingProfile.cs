﻿using AutoMapper;
using ExpenseManagement.Application.DTOs;
using ExpenseManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Application.Mappings
{
    public class DomainToDTOMappingProfile :Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Pagination, PaginationDTO>().ReverseMap();
        }
    }
}
