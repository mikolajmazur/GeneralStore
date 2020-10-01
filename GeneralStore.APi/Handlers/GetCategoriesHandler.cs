using AutoMapper;
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
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public GetCategoriesHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories
                .Include(category => category.SubCategories)
                .Where(category => category.ParentCategoryId == null)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
    }
}
