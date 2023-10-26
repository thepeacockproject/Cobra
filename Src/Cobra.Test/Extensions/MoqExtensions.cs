using System.Linq.Expressions;
using Moq;

namespace Cobra.Test.Extensions
{
    public static class MoqExtensions
    {
        public static Expression<Func<T, TResult>> Expression<T, TResult>(this Mock<T> mock, Expression<Func<T, TResult>> expression)
            where T : class
        {
            return expression;
        }
    }
}
