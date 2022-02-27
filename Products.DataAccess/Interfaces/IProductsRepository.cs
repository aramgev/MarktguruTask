using Products.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.DataAccess.Interfaces
{
    public interface IProductsRepository
    {
        public Task<List<Product>> GetAllAsync();
        public Task<ProductDetails> GetAsync(int id);
        public Task<int> AddAsync(ProductRequest product);
        public Task<int> UpdateAsync(int id, ProductRequest products);
        
        public Task<int> DeleteAsync(int id);

    }
}
