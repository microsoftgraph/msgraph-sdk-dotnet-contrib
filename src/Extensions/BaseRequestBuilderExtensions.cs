using Microsoft.Graph;
using System;
using System.Linq;

namespace Graph.Community.Extensions
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
    }
}
