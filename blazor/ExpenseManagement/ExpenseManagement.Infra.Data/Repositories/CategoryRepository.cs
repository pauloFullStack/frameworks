﻿using ExpenseManagement.Domain.Entities;
using ExpenseManagement.Domain.Interfaces;
using ExpenseManagement.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ExpenseManagement.Infra.Data.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        IDbContextFactory<ApplicationDbContextObjects> _categoryContext;
        public CategoryRepository(IDbContextFactory<ApplicationDbContextObjects> context)
        {
            _categoryContext = context;
        }

        private async Task<ApplicationDbContextObjects> CreateDbContextAsync()
        {
            return await _categoryContext.CreateDbContextAsync();
        }

        public async Task<ActionResult<Category>> CreateAsync(Category category)
        {
            using var context = await CreateDbContextAsync();
            context.Add(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> GetByIdAsync(int? id)
        {
            using var context = await CreateDbContextAsync();
            return await context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            try
            {
                using var context = await CreateDbContextAsync();
                return await context.Categories.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Category>> GetPaginationAndSearch(Pagination pagination, HttpContext context)
        {
            try
            {
                using var contextDb = await CreateDbContextAsync();
                var queryable = contextDb.Categories.AsQueryable();

                if (!string.IsNullOrEmpty(pagination.nomeFiltro))
                    queryable = queryable.Where(x => x.Name.Contains(pagination.nomeFiltro));

                await context.InserirParametroEmPageResponse(queryable, pagination.QuantidadePorPagina);
                return await queryable.Page(pagination).ToListAsync();

            }
            catch
            {
                throw;
            }
        }


        public async Task<Category> RemoveAsync(Category category)
        {
            using var contextDb = await CreateDbContextAsync();
            contextDb.Remove(category);
            await contextDb.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            using var contextDb = await CreateDbContextAsync();
            contextDb.Update(category);
            await contextDb.SaveChangesAsync();
            return category;
        }
    }
}
