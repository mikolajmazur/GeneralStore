using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GeneralStore.Api.Queries;
using GeneralStore.Api.Context;
using GeneralStore.Api.Dtos;
using GeneralStore.Api.Entities;
using MediatR;

namespace GeneralStore.Api.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public CreateProductHandler(StoreContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productToCreate = _mapper.Map<Product>(request);

            _context.Products.Add(productToCreate);
            await _context.SaveChangesAsync();

            _context.Entry(productToCreate)
                .Reference(p => p.Category)
                .Load();
            _context.Entry(productToCreate)
                .Reference(p => p.Manufacturer)
                .Load();

            var result = _mapper.Map<ProductDto>(productToCreate);
            return result;
        }
    }
}
