using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {


        public ProductRepository(DbContext context) : base(context)
        {
            
        }
        public List<Product> GetProductsByName(string name)
        {
            var query = Entities
                .Where(p =>
                    String.IsNullOrEmpty(name) ? true : p.Name.Contains(name)
                );
            
            return query.ToList();
        }
        public List<Product> FindOutOfStockProducts()
        {
            return Entities.Where(p => p.IsOutOfStock == true).ToList();
        }
    }
}