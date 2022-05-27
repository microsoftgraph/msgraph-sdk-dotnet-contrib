using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IWebRequest : IBaseRequest
  {
    Task<Web> GetAsync();
    Task<Web> GetAsync(CancellationToken cancellationToken);

    Task<IChangeLogCollectionPage> GetChangesAsync(ChangeQuery query);
    Task<IChangeLogCollectionPage> GetChangesAsync(ChangeQuery query, CancellationToken cancellationToken);

    Task<User> EnsureUserAsync(string logonName);
    Task<User> EnsureUserAsync(string logonName, CancellationToken cancellationToken);

    Task<Web> GetAssociatedGroupsAsync();
    Task<Web> GetAssociatedGroupsAsync(bool includeUsers);
    Task<Web> GetAssociatedGroupsAsync(bool includeUsers, CancellationToken cancellationToken);

    /// <summary>
    /// Adds the specified expand value to the request.
    /// </summary>
    /// <param name="value">The expand value.</param>
    /// <returns>The request object to send.</returns>
    IWebRequest Expand(string value);

    /// <summary>
    /// Adds the specified expand value to the request.
    /// </summary>
    /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
    /// <returns>The request object to send.</returns>
    IWebRequest Expand(Expression<Func<Group, object>> expandExpression);
  }
}
