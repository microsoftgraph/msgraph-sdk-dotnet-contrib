using Microsoft.Graph;
using System;
using System.Linq;

namespace Graph.Community
{
	public static class BaseRequestBuilderExtensions
	{
		/// <summary>
		/// Returns the Path to use for the Resource property of Subscription object when creating a new subscription
		/// </summary>
		/// <param name="requestBuilder">Current request builder</param>
		/// <returns>URL to use for the Resource property of Subscription object when creating a new subscription</returns>
		public static string GetResourceSubscriptionPath(this IBaseRequestBuilder requestBuilder)
		{
			var pathAndQuery = new Uri(requestBuilder.RequestUrl).PathAndQuery;
			return pathAndQuery.Substring(pathAndQuery.IndexOf('/', 1)); //skips first / to ignore the version
		}

		/// <summary>
		/// Applies an OData cast filter to the returned collection.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="requestBuilder">Current request builder</param>
		/// <param name="oDataCast">The OData type name</param>
		/// <returns>Request builder with OData cast filter applied</returns>
		public static T WithODataCast<T>(this T requestBuilder, string oDataCast) where T : IBaseRequestBuilder
    {
			var updatedUrl = requestBuilder.AppendSegmentToRequestUrl(oDataCast);
			var updatedBuilder = (T)Activator.CreateInstance(requestBuilder.GetType(), updatedUrl, requestBuilder.Client);

			return updatedBuilder;
		}
	}
}
