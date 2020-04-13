using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public interface INavigationRequestBuilder : IBaseRequestBuilder
  {
    INavigationRequest Request();

    INavigationNodeCollectionRequestBuilder QuickLaunch { get; }
    INavigationNodeCollectionRequestBuilder TopNavigationBar { get; }

    /// <summary>
    /// Gets a <see cref="INavigationNodeCollectionRequestBuilder"/> for the specified Site Design.
    /// </summary>
    /// <param name="id">The ID for the NavigationNode.</param>
    /// <returns>The <see cref="ISiteDesignRequestBuilder"/>.</returns>
    INavigationNodeRequestBuilder this[int id] { get; }
  }
}
