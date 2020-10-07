using GeneralStore.Api.Context;
using GeneralStore.Api.Entities;
using GeneralStore.Api.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralStore.Api.Handlers
{
    public class GetEntityByIdHandler<T> : IRequestHandler<GetEntityByIdBaseQuery<T>, T>
        where T: BaseEntity
    {
        private readonly StoreContext _context;
        private readonly ILogger _logger;

        public GetEntityByIdHandler(StoreContext context, ILogger logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<T> Handle(Queries.GetEntityByIdBaseQuery<T> request, CancellationToken cancellationToken)
        {
            var result = await _context.FindAsync<T>(request.Id);
            return result;
        }
    }
}
