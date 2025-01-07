using System;
using System.Linq.Expressions;
using SQLitePCL;

namespace Database.Services;

public static class GroupByService
{
    public static IQueryable<IGrouping<object, TSource>> GroupByDynamic<TSource>(this IQueryable<TSource> collection, IEnumerable<string> properties)
    {
        var parameter = Expression.Parameter(typeof(TSource), "x");

        var groupBy = Expression.NewArrayInit(
            typeof(object),
            properties.Select(property =>
                Expression.Convert(
                    Expression.PropertyOrField(parameter, property),
                    typeof(object))));

        var lamda = Expression.Lambda<Func<TSource, object>>(groupBy, parameter);

        return collection.GroupBy(lamda);
    }
}
