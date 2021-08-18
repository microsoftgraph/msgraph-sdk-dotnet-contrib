using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISiteScriptCollectionRequestBuilder : IBaseRequestBuilder
  {
    ISiteScriptCollectionRequest Request();

    /// <summary>
    /// Gets a <see cref="ISiteScriptRequestBuilder"/> for the specified Site Script.
    /// </summary>
    /// <param name="id">The ID for the Site Script.</param>
    /// <returns>The <see cref="ISiteScriptRequestBuilder"/>.</returns>
    ISiteScriptRequestBuilder this[string id] { get; }
  }
}
