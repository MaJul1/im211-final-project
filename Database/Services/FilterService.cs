using System.Data;
using System.Linq.Expressions;

namespace Database.Services;

public static class FilterService
{
    public static IQueryable<T> ApplyFieldFilter<T>(this IQueryable<T> collection, string comparisonField, Expression<Func<T, bool>> filter)
    {
        var propertyName = PropertyNameService.GetPropertyName(filter);
        
        if (comparisonField.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase))
        {
            return collection.Where(filter);
        }

        return collection;
    }
}
