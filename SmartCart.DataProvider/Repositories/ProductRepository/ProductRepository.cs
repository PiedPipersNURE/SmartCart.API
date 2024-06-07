using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartCart.DataProvider.DatabaseContext;
using SmartCart.DataProvider.Models;

namespace SmartCart.DataProvider.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(ProductDto product)
        {
            if(product == null)
            {
                return false;
            }

            var productEntity = _mapper.Map<Product>(product);

            await _context.Products.AddAsync(productEntity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AddAsync(List<ProductDto> products)
        {
            if(products.Count == 0)
            {
                return false;
            }

            var productEntities = _mapper.Map<List<Product>>(products);

            await _context.Products.AddRangeAsync(productEntities);
            var result = await _context.SaveChangesAsync();
           
            return result > 0;
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            var product = _context.Products.Find(productId);

            _context.Products.Remove(product);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<ProductDto>> Retrieve()
        {
            var products = await _context.Products.ToListAsync();

            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> RetrieveByCartAsync(Guid cartID)
        {
            var products = await _context.Products.Where(p => p.CartID == cartID).ToListAsync();

            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> RetrieveByIdAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<List<ProductDto>> RetrieveByNameAsync(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                return new List<ProductDto>();
            }

            var products = await _context.Products.Where(p => p.ProductName == name).ToListAsync();

            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> RetrieveByUserAsync(Guid UserID)
        {
            var products = _context.Products.Where(p => p.BuyerID == UserID).ToList();

            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<bool> UpdateAsync(ProductDto product)
        {
            if(product == null)
            {
                return false;
            }

            var productEntity = _mapper.Map<Product>(product);

            _context.Products.Update(productEntity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateAsync(List<ProductDto> products)
        {
            if(products.Count == 0)
            {
                return false;
            }

            var productEntities = _mapper.Map<List<Product>>(products);

            _context.Products.UpdateRange(productEntities);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
