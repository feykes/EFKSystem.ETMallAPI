using EFKSystem.Application.Abstractions;
using EFKSystem.Application.Repositories.Product;
using EFKSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFKSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepositoryy)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepositoryy;
        }
        [HttpGet]
        public async Task Get()
        {
            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new() { Id = Guid.NewGuid(), Name = "Product-1", Price = 100, CreatedDate = DateTime.UtcNow, Stock = 10, },
            //    new() { Id = Guid.NewGuid(), Name = "Product-2", Price = 200, CreatedDate = DateTime.UtcNow, Stock = 20, },
            //    new() { Id = Guid.NewGuid(), Name = "Product-3", Price = 300, CreatedDate = DateTime.UtcNow, Stock = 30, },
            //    new() { Id = Guid.NewGuid(), Name = "Product-4", Price = 400, CreatedDate = DateTime.UtcNow, Stock = 40, },
            //    new() { Id = Guid.NewGuid(), Name = "Product-5", Price = 500, CreatedDate = DateTime.UtcNow, Stock = 50, },
            //});
            //await _productWriteRepository.SaveAsync();
            Products product = await _productReadRepository.GetByIdAsync("fafd62f5-3457-45c6-8957-4d8182b3e7cc");
            product.Name = "Emre";
            await _productWriteRepository.SaveAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Products product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}