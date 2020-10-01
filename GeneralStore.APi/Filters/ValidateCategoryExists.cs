using GeneralStore.Api.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Filters
{
    public class ValidateCategoryExists : IAsyncActionFilter
    {
        private readonly StoreContext _storeContext;

        public ValidateCategoryExists(StoreContext context)
        {
            _storeContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.ContainsKey("id"))
            {
                var id = context.ActionArguments["id"] as Guid?;
                if (id.HasValue)
                {
                    if (! await _storeContext.Categories.AnyAsync(c => c.Id == id))
                    {
                        context.Result = new NotFoundObjectResult(id.Value);
                        return;
                    }
                }
            }
            await next();
        }
    }
}
