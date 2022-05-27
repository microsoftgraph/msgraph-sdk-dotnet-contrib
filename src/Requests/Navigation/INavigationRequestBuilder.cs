using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface INavigationRequestBuilder : IBaseRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    INavigationRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    INavigationRequest Request(IEnumerable<Option> options);

    INavigationNodeCollectionRequestBuilder QuickLaunch { get; }
    INavigationNodeCollectionRequestBuilder TopNavigationBar { get; }

    /// <summary>
    /// Gets a <see cref="INavigationNodeCollectionRequestBuilder"/> for the specified Site Design.
    /// </summary>
    /// <param name="id">The ID for the NavigationNode.</param>
    /// <returns>The <see cref="ISiteDesignCollectionRequestBuilder"/>.</returns>
    INavigationNodeRequestBuilder this[int id] { get; }
  }
}
