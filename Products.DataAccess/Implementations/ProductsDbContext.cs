using Microsoft.EntityFrameworkCore;
using Products.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.DataAccess.Implementations
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<ProductDetails> Products { get; set; }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        { 
            if(Products.Count() == 0)
                LoadProducts();
        }

        public void LoadProducts()
        {
            ProductDetails p = new ProductDetails() { Id = 1, Name ="Coca Cola", Price=2, Available=true, Description= "The Coca-Cola Company is an American multinational beverage corporation incorporated under Delaware's General Corporation Law[a] and headquartered in Atlanta, Georgia. The Coca-Cola Company has interests in the manufacturing, retailing, and marketing of non-alcoholic beverage concentrates and syrups, and alcoholic beverages.", DateCreated = DateTime.Now};
            Products.Add(p);
            p = new ProductDetails() { Id = 2, Name = "Sprite", Price = 1.8m, Available = false, DateCreated = DateTime.Now };
            Products.Add(p);
            p = new ProductDetails() { Id = 3, Name = "Snikers", Price = 2.2m, Available = true, Description="Tasty chocolade", DateCreated = DateTime.Now };
            Products.Add(p);
            this.SaveChanges();
        }

        
    }
}
