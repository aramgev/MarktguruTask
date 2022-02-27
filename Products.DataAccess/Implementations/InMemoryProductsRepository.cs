using Microsoft.EntityFrameworkCore;
using Products.DataAccess.Interfaces;
using Products.Domain;

namespace Products.DataAccess.Implementations
{
    public class InMemoryProductsRepository : IProductsRepository
    {
        private readonly ProductsDbContext _context;

        public InMemoryProductsRepository(ProductsDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(ProductRequest productRequest)
        {
            // in real life in SQL Name will have unique constraint
            ProductDetails p = _context.Products.Where(p => p.Name == productRequest.Name).FirstOrDefault();
            if (p == null)
            {
                ProductDetails product = new ProductDetails()
                {
                    Name = productRequest.Name,
                    Price = productRequest.Price,
                    Available = productRequest.Available,
                    Description = productRequest.Description
                };

                // in real life of corse the Id will be the identity in DB and I would not do this
                p = _context.Products.Last();
                product.Id = p.Id + 1;

                product.DateCreated = DateTime.Now;
                _context.Products.Add(product);
                return await _context.SaveChangesAsync();
            }
            throw new Exception("Product with such name already exists");
        }

        public async Task<int> DeleteAsync(int id)
        {
            ProductDetails p = _context.Products.Where(p => p.Id == id).FirstOrDefault();
            if (p != null)
            {
                _context.Products.Remove(p);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductDetails> GetAsync(int id)
        {
            return await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Available = p.Available,
                Price = p.Price
            }).ToListAsync();
        }

        public async Task<int> UpdateAsync(int id, ProductRequest product)
        {
            ProductDetails p = _context.Products.Where(p => p.Id == id).FirstOrDefault();
            if (p != null) 
            { 
                p.Name = product.Name;
                p.Available = product.Available;
                p.Price = product.Price;
                p.Description = product.Description;
                return await _context.SaveChangesAsync();
            }
            return 0; 
        }
    }
}
