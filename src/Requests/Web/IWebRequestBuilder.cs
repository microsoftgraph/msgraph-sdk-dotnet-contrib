using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public interface IWebRequestBuilder : IBaseRequestBuilder
  {
    IWebRequest Request();

    IListRequestBuilder Lists { get; }

    INavigationRequestBuilder Navigation { get; }

    ISiteUserCollectionRequestBuilder SiteUsers { get; }
  }
}
