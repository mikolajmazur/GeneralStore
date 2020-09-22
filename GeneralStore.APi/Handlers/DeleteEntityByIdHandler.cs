using GeneralStore.Api.Commands;
using GeneralStore.Api.Context;
using GeneralStore.Api.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralStore.Api.Handlers
{
    public class DeleteEntityByIdHandler<T> : IRequestHandler<DeleteEntityByIdCommand<T>, bool>
        where T : BaseEntity
    {
        private readonly StoreContext _context;

        public DeleteEntityByIdHandler(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> Handle(DeleteEntityByIdCommand<T> request, CancellationToken cancellationToken)
        {
            var entityToDelete = await _context.FindAsync<T>(request.Id);
            entityToDelete.IsDeleted = true;
            if (await _context.SaveChangesAsync() >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
