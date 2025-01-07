using System.Data;
using System.Linq.Expressions;

namespace Database.Services;

public static class FilterService
{
    public static IQueryable<T> ApplyFieldFilter<T>(this IQueryable<T> collection, string comparisonField, Expression<Func<T, bool>> filter)
    {
        var propertyName = GetPropertyName(filter);
        
        if (comparisonField.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase))
        {
            return collection.Where(filter);
        }

        return collection;
    }

    private static string GetPropertyName<T>(Expression<Func<T, bool>> filter)
    {
        if (filter.Body is BinaryExpression binaryExpression)
        {
            if (binaryExpression.Left is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
        }
        throw new InvalidOperationException("Invalid expression format");
    }
}
