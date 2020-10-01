using AutoMapper;
using GeneralStore.Api.Context;
using GeneralStore.Api.Dtos;
using GeneralStore.Api.Entities;
using GeneralStore.Api.Helpers;
using GeneralStore.Api.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralStore.Api.Handlers
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, PagedList<OrderDto>>
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetOrdersQuery> _logger;

        public GetOrdersHandler(StoreContext storeContext, IMapper mapper, ILogger<GetOrdersQuery> logger)
        {
            _context = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PagedList<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var ordersCount = await _context.Orders.CountAsync();
            var totalPages = (int)(Math.Ceiling(ordersCount / (double)request.PageSize));
            var pageNumber = request.PageNumber > totalPages ? totalPages : request.PageNumber;
            var amountToSkip = (pageNumber - 1) * request.PageSize;

            var orders = await _context.Orders
                .Skip(amountToSkip)
                .Take(request.PageSize)
                .ToListAsync();

            var mappedOrders = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);
            var pagedResult = new PagedList<OrderDto>(mappedOrders, pageNumber, request.PageSize, ordersCount);
            return pagedResult;
        }
    }
}
