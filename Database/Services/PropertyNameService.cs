using System;
using System.Linq.Expressions;

namespace Database.Services;

public class PropertyNameService
{
    public static string GetPropertyName<T>(Expression<Func<T, bool>> filterExpression)
    {
        if (filterExpression.Body is BinaryExpression binaryExpression)
        {
            if (binaryExpression.Left is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
        }
        throw new InvalidOperationException("Invalid expression format");
    }

public static string GetPropertyName<T, TKey>(Expression<Func<T, TKey>> sortExpression)
{
    if (sortExpression.Body is MemberExpression memberExpression)
    {
        return memberExpression.Member.Name;
    }
    else if (sortExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
    {
        return operand.Member.Name;
    }
    throw new InvalidOperationException("Invalid expression format");
}
}
