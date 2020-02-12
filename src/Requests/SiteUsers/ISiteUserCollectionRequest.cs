using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface ISiteUserCollectionRequest : IBaseRequest
  {
    Task<ICollectionPage<SPUser>> GetAsync();

    Task<ICollectionPage<SPUser>> GetAsync(CancellationToken cancellationToken);


    /// <summary>
    /// Adds the specified User to the collection via POST.
    /// </summary>
    /// <param name="user">The User to add.</param>
    /// <returns>The created User.</returns>
    //System.Threading.Tasks.Task<User> AddAsync(User user);

    /// <summary>
    /// Adds the specified User to the collection via POST.
    /// </summary>
    /// <param name="user">The User to add.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
    /// <returns>The created User.</returns>
    //System.Threading.Tasks.Task<User> AddAsync(User user, CancellationToken cancellationToken);


    /// <summary>
    /// Adds the specified expand value to the request.
    /// </summary>
    /// <param name="value">The expand value.</param>
    /// <returns>The request object to send.</returns>
    //IGraphServiceUsersCollectionRequest Expand(string value);

    /// <summary>
    /// Adds the specified expand value to the request.
    /// </summary>
    /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
    /// <returns>The request object to send.</returns>
    //IGraphServiceUsersCollectionRequest Expand(Expression<Func<User, object>> expandExpression);

    /// <summary>
    /// Adds the specified select value to the request.
    /// </summary>
    /// <param name="value">The select value.</param>
    /// <returns>The request object to send.</returns>
    //IGraphServiceUsersCollectionRequest Select(string value);

    /// <summary>
    /// Adds the specified select value to the request.
    /// </summary>
    /// <param name="selectExpression">The expression from which to calculate the select value.</param>
    /// <returns>The request object to send.</returns>
    //IGraphServiceUsersCollectionRequest Select(Expression<Func<User, object>> selectExpression);

    /// <summary>
    /// Adds the specified top value to the request.
    /// </summary>
    /// <param name="value">The top value.</param>
    /// <returns>The request object to send.</returns>
    //IGraphServiceUsersCollectionRequest Top(int value);

    /// <summary>
    /// Adds the specified filter value to the request.
    /// </summary>
    /// <param name="value">The filter value.</param>
    /// <returns>The request object to send.</returns>
    //IGraphServiceUsersCollectionRequest Filter(string value);

    /// <summary>
    /// Adds the specified skip value to the request.
    /// </summary>
    /// <param name="value">The skip value.</param>
    /// <returns>The request object to send.</returns>
    //IGraphServiceUsersCollectionRequest Skip(int value);

    /// <summary>
    /// Adds the specified orderby value to the request.
    /// </summary>
    /// <param name="value">The orderby value.</param>
    /// <returns>The request object to send.</returns>
    //IGraphServiceUsersCollectionRequest OrderBy(string value);

  }
}
