using Microsoft.AspNetCore.Mvc;
using Pryaniky.TestTask.Domain.Dto;
using Pryaniky.TestTask.Domain.Repositories;

namespace Pryaniky.TertTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderGetResponse>> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new OrderGetRequest
            {
                Id = id,
            };
            var response = await _repository.GetById(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add([FromBody] OrderCreateRequest request, CancellationToken cancellationToken)
        {
            var response = await _repository.Add(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, response.Id);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] OrderUpdateRequest request, CancellationToken cancellationToken)
        {
            await _repository.Update(request, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new OrderDeleteRequest
            {
                Id = id
            };
            await _repository.Delete(request, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<OrderGetQueryResponse>> GetByQuery(
            [FromQuery] OrderGetQueryRequest request, 
            CancellationToken cancellationToken)
        {
            var response = await _repository.GetByQuery(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("{id}/add/{productId}")]
        public async Task<ActionResult> AddProduct(
            [FromRoute] Guid id,
            [FromRoute] Guid productId,
            CancellationToken cancellationToken)
        {
            await _repository.AddProduct(id, productId, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}/remove/{productId}")]
        public async Task<ActionResult> DeleteProduct(
            [FromRoute] Guid id,
            [FromRoute] Guid productId,
            CancellationToken cancellationToken)
        {
            await _repository.DeleteProduct(id, productId, cancellationToken);
            return Ok();
        }
    }
}
