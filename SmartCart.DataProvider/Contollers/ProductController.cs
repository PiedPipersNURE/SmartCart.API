using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCart.DataProvider.Models;
using SmartCart.DataProvider.Repositories;

namespace SmartCart.DataProvider.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("getAll")]
        public async Task<List<ProductDto>> GetProducts()
        {
            var products = await _productRepository.Retrieve();
            return products;
        }

        [Authorize]
        [HttpGet("getById/{productId}")]
        public async Task<ProductDto> GetProductById(Guid productId)
        {
            var product = await _productRepository.RetrieveByIdAsync(productId);
            return product;
        }

        [Authorize]
        [HttpGet("getByCartAndUser/{cartID}/{userID}")]
        public async Task<List<ProductDto>> GetProductByCartAndUser(Guid cartID, Guid userID)
        {
            var products = await _productRepository.RetrieveByCartAndUserAsync(cartID, userID);
            return products;
        }

        [Authorize]
        [HttpGet("getByName/{name}")]
        public async Task<List<ProductDto>> GetProductByName(string name)
        {
            var products = await _productRepository.RetrieveByNameAsync(name);
            return products;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<bool> AddProduct([FromBody] ProductDto product)
        {
            var result = await _productRepository.AddAsync(product);
            return result;
        }

        [Authorize]
        [HttpPost("addMultiple")]
        public async Task<bool> AddProducts([FromBody] List<ProductDto> products)
        {
            var result = await _productRepository.AddAsync(products);
            return result;
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<bool> UpdateProduct([FromBody] ProductDto product)
        {
            var result = await _productRepository.UpdateAsync(product);
            return result;
        }

        [Authorize]
        [HttpPut("updateMultiple")]
        public async Task<bool> UpdateProducts([FromBody] List<ProductDto> products)
        {
            var result = await _productRepository.UpdateAsync(products);
            return result;
        }

        [Authorize]
        [HttpDelete("delete/{productId}")]
        public async Task<bool> DeleteProduct(Guid productId)
        {
            var result = await _productRepository.DeleteAsync(productId);
            return result;
        }
    }
}
