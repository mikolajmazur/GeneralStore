using GeneralStore.Api.Context;
using GeneralStore.Api.Dtos;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Filters
{
    public class ValidateOrderedItems : IAsyncActionFilter
    {
        private readonly StoreContext _storeContext;

        public ValidateOrderedItems(StoreContext storeContext)
        {
            _storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.ContainsKey("items"))
            {
                var items = context.ActionArguments["items"] as IEnumerable<OrderItemCreateDto>;
            }

            await next();
        }
    }
}
