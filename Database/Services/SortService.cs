using System;
using System.Linq.Expressions;
using Database.Enums;
using Database.Models;

namespace Database.Services;

public static class SortService
{
    public static IQueryable<T> ApplyFieldSort<T, Tkey>(this IQueryable<T> collection, string comparisonField, Expression<Func<T, Tkey>> expression, SortType sortType)
    {
        var propertyName = PropertyNameService.GetPropertyName(expression);

        if (comparisonField.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase))
        {
            return SortCollection(collection, expression, sortType);
        }
        
        return collection;
    }

    private static IQueryable<T> SortCollection<T, Tkey>(IQueryable<T> collection, Expression<Func<T, Tkey>> expression, SortType sortType)
    {
        if (sortType == SortType.DESCENDING)
        {
            return collection.OrderByDescending(expression);
        }
        else
        {
            return collection.OrderBy(expression);
        }
    }
}
