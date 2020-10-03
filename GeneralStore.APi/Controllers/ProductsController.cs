using GeneralStore.Api.Queries;
using GeneralStore.Api.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using GeneralStore.Api.Commands;
using GeneralStore.Api.Entities;
using Microsoft.AspNetCore.Authorization;

namespace GeneralStore.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _mediator;

        public ProductsController(ILogger<ProductsController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Authorize(Roles = "customer")]
        [HttpGet("{id}", Name = "GetProductAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductAsync(Guid id)
        {
            var query = new GetProductByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsAsync(
            int? pageNumber = 0, int? pageSize = 10)
        {
            var query = new GetProductsQuery
            {
                PageNumber = pageNumber.Value,
                PageSize = pageSize.Value
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ProductDto>> CreateProductAsync(
            [FromBody] CreateProductCommand createProductCommand)
        {
            var returnDto = await _mediator.Send(createProductCommand);
            return CreatedAtRoute("GetProductAsync", new { returnDto.Id }, returnDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            var request = new DeleteEntityByIdCommand<Product> { Id = id };
            var result = await _mediator.Send(request);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
