using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListCollectionRequest : IBaseRequest
  {
    Task<IListCollectionPage> GetAsync();

    Task<IListCollectionPage> GetAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Adds the specified expand value to the request.
    /// </summary>
    /// <param name="value">The expand value.</param>
    /// <returns>The request object to send.</returns>
    IListCollectionRequest Expand(string value);

    /// <summary>
    /// Adds the specified expand value to the request.
    /// </summary>
    /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
    /// <returns>The request object to send.</returns>
    IListCollectionRequest Expand(Expression<Func<List, object>> expandExpression);
  }
}
