using GeneralStore.Api.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Queries
{
    public class GetEntityByIdQuery<T> : IRequest<T> where T: BaseEntity
    {
        public Guid Id { get; set; }
    }
}
