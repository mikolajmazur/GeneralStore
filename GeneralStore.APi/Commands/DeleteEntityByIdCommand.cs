using GeneralStore.Api.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Commands
{
    public class DeleteEntityByIdCommand<T> : IRequest<bool>
        where T: BaseEntity
    {
        public Guid Id { get; set; }
    }
}
