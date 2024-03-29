using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IAppTileCollectionRequest : IBaseRequest
  {
    Task<IAppTileCollectionPage> GetAsync();

    Task<IAppTileCollectionPage> GetAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Adds the specified select value to the request.
    /// </summary>
    /// <param name="value">The select value.</param>
    /// <returns>The request object to send.</returns>
    IAppTileCollectionRequest Select(string value);

    /// <summary>
    /// Adds the specified select value to the request.
    /// </summary>
    /// <param name="selectExpression">The expression from which to calculate the select value.</param>
    /// <returns>The request object to send.</returns>
    IAppTileCollectionRequest Select(Expression<Func<AppTile, object>> selectExpression);

    /// <summary>
    /// Adds the specified top value to the request.
    /// </summary>
    /// <param name="value">The top value.</param>
    /// <returns>The request object to send.</returns>
    IAppTileCollectionRequest Top(int value);


    /// <summary>
    /// Adds the specified skip value to the request.
    /// </summary>
    /// <param name="value">The skip value.</param>
    /// <returns>The request object to send.</returns>
    IAppTileCollectionRequest Skip(int value);

    /// <summary>
    /// Adds the specified orderby value to the request.
    /// </summary>
    /// <param name="value">The orderby value.</param>
    /// <returns>The request object to send.</returns>
    IAppTileCollectionRequest OrderBy(string value);

  }
}
