using System.Linq.Expressions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListRequest : IBaseRequest
  {
    Task<List> GetAsync();
    Task<List> GetAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Adds the specified expand value to the request.
    /// </summary>
    /// <param name="value">The expand value.</param>
    /// <returns>The request object to send.</returns>
    IListRequest Expand(string value);

    /// <summary>
    /// Adds the specified expand value to the request.
    /// </summary>
    /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
    /// <returns>The request object to send.</returns>
    IListRequest Expand(Expression<Func<List, object>> expandExpression);


    Task<IChangeLogCollectionPage> GetChangesAsync(ChangeQuery query);
    Task<IChangeLogCollectionPage> GetChangesAsync(ChangeQuery query, CancellationToken cancellationToken);
  }
}
