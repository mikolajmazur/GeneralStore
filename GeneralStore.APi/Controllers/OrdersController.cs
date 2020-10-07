using AutoMapper;
using GeneralStore.Api.Commands;
using GeneralStore.Api.Context;
using GeneralStore.Api.Dtos;
using GeneralStore.Api.Entities;
using GeneralStore.Api.Helpers;
using GeneralStore.Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IMediator _mediator;

        public OrdersController(ILogger<OrdersController> logger, IMediator mediator, StoreContext context, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] IEnumerable<OrderItemCreateDto> orderElements)
        {
            var orderCommand = new CreateOrderCommand() { Items = orderElements };
            var result = await _mediator.Send(orderCommand);
            if (result == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("GetOrderAsync", new { Id = result.Id }, result);
        }

        [HttpGet("{id}", Name = "GetOrderAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> GetOrderAsync(Guid id)
        {
            var query = new GetOrderByIdQuery{ Id = id };
            var result = await _mediator.Send(query);
            
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin, employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedList<OrderDto>>> GetAllOrdersAsync(int? pageSize, int? pageNumber,
            string orderBy)
        {
            var query = new GetOrdersQuery(pageNumber, pageSize) { OrderBy = orderBy };
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
