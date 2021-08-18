using Microsoft.Graph;

namespace Graph.Community
{
  public static class BaseRequestExtensions
  {
    /// <summary>
    /// Adds the <b>Prefer</b> header to include Immutable Ids in the response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseRequest">The <see cref="BaseRequest"/> for the request.</param>
    /// <returns></returns>
    public static T WithImmutableId<T>(this T baseRequest) where T : IBaseRequest
    {
      baseRequest.Headers.Add(
        new HeaderOption(
          RequestExtensionsConstants.Headers.PreferHeaderName,
          RequestExtensionsConstants.Headers.PreferHeaderImmutableIdValue)
      );
      return baseRequest;
    }

    /// <summary>
    /// Applies the <b>ConsistencyLevel</b> header required for certain DirectoryObject queries.
    /// (https://developer.microsoft.com/en-us/office/blogs/microsoft-graph-advanced-queries-for-directory-objects-are-now-generally-available/)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseRequest">The <see cref="BaseRequest"/> for the request.</param>
    /// <returns></returns>
    public static T WithEventualConsistency<T>(this T baseRequest) where T : IBaseRequest
    {
      baseRequest.Headers.Add(
        new HeaderOption(
          RequestExtensionsConstants.Headers.ConsistencyLevelHeaderName,
          RequestExtensionsConstants.Headers.ConsistencyLevelEventualValue)
      );
      return baseRequest;
    }
  }
}
