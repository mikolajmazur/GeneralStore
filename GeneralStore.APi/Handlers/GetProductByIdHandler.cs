using AutoMapper;
using GeneralStore.Api.Context;
using GeneralStore.Api.Dtos;
using GeneralStore.Api.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralStore.Api.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public GetProductByIdHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productEntity = await _context.Products
                .Include(product => product.Manufacturer)
                .Include(product => product.Category)
                .SingleOrDefaultAsync(product => product.Id == request.Id
                                      && !product.IsDeleted);
            return _mapper.Map<ProductDto>(productEntity);
        }
    }
}
