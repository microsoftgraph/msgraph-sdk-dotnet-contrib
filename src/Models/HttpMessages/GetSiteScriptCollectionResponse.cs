using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class GetSiteScriptCollectionResponse
  {
    public CollectionPage<SiteScriptMetadata> Value { get; }

    public GetSiteScriptCollectionResponse()
    {
      this.Value = new CollectionPage<SiteScriptMetadata>();
    }
  }
}
