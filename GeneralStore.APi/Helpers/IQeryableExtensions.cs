using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace GeneralStore.Api.Helpers
{
    public static class IQeryableExtensions
    {
        // query string can contain multiple properties to order by, separated by ',',
        // with order direction added as sufix (either "_desc" or "_asc")
        // default is ascending
        public static IQueryable<T> OrderByString<T>(this IQueryable<T> originalQuery, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return originalQuery;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var availableProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var queryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }

                var propertyName = param.Split("_")[0];
                var property = availableProperties.FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
                if (property == null)
                {
                    continue;
                }

                var direction = param.EndsWith("_desc") ? "descending" : "ascending";

                queryBuilder.Append($"{property.Name} {direction}, ");
            }

            var queryString = queryBuilder.ToString().TrimEnd(',', ' ');

            return originalQuery.OrderBy(queryString);
        }
    }
}
