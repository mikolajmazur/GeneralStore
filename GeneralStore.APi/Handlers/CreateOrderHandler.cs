using AutoMapper;
using GeneralStore.Api.Commands;
using GeneralStore.Api.Context;
using GeneralStore.Api.Dtos;
using GeneralStore.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralStore.Api.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateOrderHandler> _logger;

        public CreateOrderHandler(StoreContext context, IMapper mapper, ILogger<CreateOrderHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderedIds = request.Items.Select(x => x.ItemId).ToList();
            var orderedProducts = await _context.Products
                .Where(x => orderedIds.Contains(x.Id))
                .ToListAsync();

            var orderEntity = new Order();

            foreach (var requestedItem in request.Items)
            {
                var item = orderedProducts.FirstOrDefault(x => x.Id == requestedItem.ItemId);
                if (item == null || requestedItem.Amount > item.AvailableUnits)
                {
                    _logger.LogInformation($"Attemped to order non-existent item or too much of it. Requested GUID: {requestedItem.ItemId}, amount: {requestedItem.Amount}");
                    return null;
                }

                var orderItem = new OrderItem
                {
                    ItemId = item.Id,
                    Price = item.Price,
                    ItemName = item.Name,
                    Amount = requestedItem.Amount
                };
                orderEntity.Items.Add(orderItem);
                orderEntity.TotalValue += orderItem.Amount * orderItem.Price;

                // remove ordered amount from products available units
                item.AvailableUnits -= orderItem.Amount;
            }

            _context.Orders.Add(orderEntity);
            // TODO: handle failure
            await _context.SaveChangesAsync();

            var result = _mapper.Map<Order, OrderDto>(orderEntity);
            _logger.LogInformation($"Saved order with id: {result.Id}");

            return result;
        }
    }
}
