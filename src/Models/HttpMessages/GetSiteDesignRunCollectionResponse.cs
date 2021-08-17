using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class GetSiteDesignRunCollectionResponse
  {
    public CollectionPage<SiteDesignRun> Value { get; }

    public GetSiteDesignRunCollectionResponse()
    {
      this.Value = new CollectionPage<SiteDesignRun>();
    }
  }
}
