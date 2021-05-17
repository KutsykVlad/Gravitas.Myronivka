using System;
using System.Linq;
using System.Linq.Expressions;
using Gravitas.Model;

namespace Gravitas.DAL
{
    public static class EntityExtensions
    {
        public static IQueryable<TEntity> WhereIdEquals<TEntity, TKey>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, TKey>> keyExpression,
            TKey otherKeyValue)
            where TEntity : BaseEntity<TKey>
        {
            var memberExpression = (MemberExpression) keyExpression.Body;
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.Property(parameter, memberExpression.Member.Name);
            var equal = Expression.Equal(property, Expression.Constant(otherKeyValue));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, parameter);
            return source.Where(lambda);
        }
    }
}