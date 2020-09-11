using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace PakGrocery.API.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> ApplySort<TEntity>(this IQueryable<TEntity> source, string orderByValues) where TEntity : class
          {
            string sortOrder = "";
            var orderPairs = orderByValues.Trim().Split(',');
            var type = typeof(TEntity);
            var props = type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                .Select(p=> p.Name)
                .ToList();

            foreach (var item in orderPairs)
            {
                string command = item.StartsWith("-") ? " Descending" : "";
                string fieldName = item.StartsWith("-") ? item.Remove(0, 1) : item;

                var propName = (from n in props
                                where string.Equals(n, fieldName, StringComparison.OrdinalIgnoreCase)
                                select n).FirstOrDefault();

                if (propName != null)
                sortOrder = sortOrder + ", " + propName + command;
            }


            if (sortOrder.StartsWith(", "))
            {
                sortOrder = sortOrder.Remove(0, 2);
            }

            if (sortOrder.EndsWith(", "))
            {
                sortOrder = sortOrder.Remove(sortOrder.LastIndexOf(","), 2);
                // remove first item
                //string newSearchForWords = orderByValues.ToString().Remove(0, orderByValues.ToString().IndexOf(',') + 1);
            }

            return source.OrderBy(sortOrder);

            //return returnValue;
        }
    }
}
