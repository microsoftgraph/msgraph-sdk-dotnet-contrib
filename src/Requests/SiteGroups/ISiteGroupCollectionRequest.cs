using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
	public interface ISiteGroupCollectionRequest : IBaseRequest
	{
		Task<ICollectionPage<Group>> GetAsync();

		Task<ICollectionPage<Group>> GetAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Adds the specified expand value to the request.
		/// </summary>
		/// <param name="value">The expand value.</param>
		/// <returns>The request object to send.</returns>
		ISiteGroupCollectionRequest Expand(string value);

		/// <summary>
		/// Adds the specified expand value to the request.
		/// </summary>
		/// <param name="expandExpression">The expression from which to calculate the expand value.</param>
		/// <returns>The request object to send.</returns>
		ISiteGroupCollectionRequest Expand(Expression<Func<Group, object>> expandExpression);
	}
}
