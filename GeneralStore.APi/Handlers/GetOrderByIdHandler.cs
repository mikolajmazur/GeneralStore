using AutoMapper;
using GeneralStore.Api.Context;
using GeneralStore.Api.Dtos;
using GeneralStore.Api.Entities;
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
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly ILogger<GetOrderByIdHandler> _logger;
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        public GetOrderByIdHandler(ILogger<GetOrderByIdHandler> logger, StoreContext context, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<Order, OrderDto>(order);
        }
    }
}
