using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class GetSiteDesignCollectionResponse
  {
    public CollectionPage<SiteDesignMetadata> Value { get; }

    public GetSiteDesignCollectionResponse()
    {
      this.Value = new CollectionPage<SiteDesignMetadata>();
    }
  }
}
