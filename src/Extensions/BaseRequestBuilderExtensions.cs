using Microsoft.Graph;
using System;
using System.Linq;

namespace Graph.Community.Extensions
{
    public static class BaseRequestBuilderExtensions
    {
        private const char slash = '/';
        private const int segmentsToSkip = 3; // [https, graph.microsoft.com, version]
        /// <summary>
        /// Returns the URL to use for the Resource property of Subscription object when creating a new subscription
        /// </summary>
        /// <param name="requestBuilder">Current request builder</param>
        /// <returns>URL to use for the Resource property of Subscription object when creating a new subscription</returns>
        public static string GetSubscriptionResourceUrl(this IBaseRequestBuilder requestBuilder) => 
            $"{slash}{requestBuilder.RequestUrl.Split(new char[] { slash }, StringSplitOptions.RemoveEmptyEntries).Skip(segmentsToSkip).Aggregate((x, y) => $"{x}{slash}{y}")}";
    }
}
