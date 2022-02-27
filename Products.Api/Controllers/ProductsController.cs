using Microsoft.AspNetCore.Mvc;
using Products.Api.Authorization;
using Products.DataAccess.Interfaces;
using Products.Domain;

namespace Products.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductsRepository _repository;

        public ProductsController(IProductsRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets the list of all products.
        /// </summary>
        /// <returns>List of products.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _repository.GetAllAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log exception
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the product by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            { 
                var product = await _repository.GetAsync(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                // Log exception
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adds new product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductRequest product)
        {
            try
            {
                await _repository.AddAsync(product);
                return CreatedAtAction(nameof(Add), product);
            }
            catch (Exception ex)
            {
                // Log exception
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductRequest product) 
        {
            try
            {
                await _repository.UpdateAsync(id,product);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log exception
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes product by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log exception
                return BadRequest(ex.Message);
            }
        }
    }
}
