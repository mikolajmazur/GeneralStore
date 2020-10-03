using AutoMapper;
using GeneralStore.Api.Context;
using GeneralStore.Api.Dtos;
using GeneralStore.Api.Helpers;
using GeneralStore.Api.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralStore.Api.Handlers
{
    public class GetProductsForCategoryHandler : IRequestHandler<GetProductsForCategoryQuery,
        PagedList<ProductSimplifiedDto>>
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public GetProductsForCategoryHandler(StoreContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PagedList<ProductSimplifiedDto>> Handle(GetProductsForCategoryQuery request, CancellationToken cancellationToken)
        {
            var amountToSkip = (request.PageNumber - 1) * request.PageSize;
            var totalMatchingProducts = await _context.Products
                .Where(product => product.CategoryId == request.CategoryId
                                   && !product.IsDeleted)
                .CountAsync();
            var pageNumber = request.PageNumber;
            var totalPages = (int)(Math.Ceiling(totalMatchingProducts / (double)request.PageSize));

            if (amountToSkip >= totalMatchingProducts)
            {
                amountToSkip = (totalMatchingProducts - request.PageSize) > 0 ? (totalMatchingProducts - request.PageSize) : 0;
                pageNumber = totalPages;
            }

            var returnedProducts = await _context.Products
                .Where(product => product.CategoryId == request.CategoryId
                                && !product.IsDeleted)
                .OrderByString(request.OrderBy)
                .Include(product => product.Manufacturer)
                .Skip(amountToSkip)
                .Take(request.PageSize)
                .ToListAsync();
            var mappedProducts = _mapper.Map <IEnumerable<ProductSimplifiedDto>>(returnedProducts);

            var pagedResult = new PagedList<ProductSimplifiedDto>(mappedProducts, pageNumber, request.PageSize, totalMatchingProducts);

            return pagedResult; 
        }
    }
}
