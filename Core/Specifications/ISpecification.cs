using System.Linq.Expressions;

namespace Core.Specifications
{
    // This make a generic interface be able to include unique classes to database calls
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
    }
}