using GeneralStore.Api.Dtos;
using GeneralStore.Api.Filters;
using GeneralStore.Api.Helpers;
using GeneralStore.Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(IMediator mediator, ILogger<CategoriesController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        
        [HttpGet("{id}/products")]
        [TypeFilter(typeof(ValidateCategoryExists))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<ProductSimplifiedDto>>> GetProductsForCategory(Guid id,
            int? pageNumber, int? pageSize)
        {
            var query = new GetProductsForCategoryQuery(pageNumber, pageSize) { CategoryId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        /*
        public CategoryDto GetCategoryAsync(Guid id)
        {
            throw new NotImplementedException();
        }*/

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategoriesAsync()
        {
            var query = new GetCategoriesQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        //public async Task<A>
    }
}
