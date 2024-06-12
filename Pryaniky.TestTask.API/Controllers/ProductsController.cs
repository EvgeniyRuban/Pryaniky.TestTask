using Microsoft.AspNetCore.Mvc;
using Pryaniky.TestTask.Domain.Dto;
using Pryaniky.TestTask.Domain.Repositories;

namespace Pryaniky.TertTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductGetResponse>> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new ProductGetRequest
            {
                Id = id,
            };
            var response = await _repository.GetById(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add([FromBody] ProductCreateRequest request, CancellationToken cancellationToken)
        {
            var response = await _repository.Add(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, response.Id);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ProductUpdateRequest request, CancellationToken cancellationToken)
        {
            await _repository.Update(request, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new ProductDeleteRequest
            {
                Id = id
            };
            await _repository.Delete(request, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<ProductGetQueryResponse>> GetByQuery(
            [FromQuery] ProductGetQueryRequest request, 
            CancellationToken cancellationToken)
        {
            var response = await _repository.GetByQuery(request, cancellationToken); 
            return Ok(response);
        }
    }
}
