using GeneralStore.Api.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Queries
{
    public class GetEntityByIdQuery<TResponse> : IRequest<TResponse>
    {
        public Guid Id { get; set; }
    }
}
