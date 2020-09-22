﻿using AutoMapper;
using GeneralStore.Api.Context;
using GeneralStore.Api.Dtos;
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
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public GetProductsHandler(StoreContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var amountToSkip = request.PageSize * (request.PageNumber - 1);
            var result = await _context.Products
                .Where(product => !product.IsDeleted)
                .Include(product => product.Manufacturer)
                .Skip(amountToSkip)
                .Take(request.PageSize)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(result);
        }
    }
}
