﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISiteDesignRequest : IBaseRequest
  {
    Task<SiteDesignMetadata> GetAsync();
    Task<SiteDesignMetadata> GetAsync(CancellationToken cancellationToken);

    Task<SiteDesignMetadata> UpdateAsync(SiteDesignMetadata updatedSiteDesignMetadata);
    Task<SiteDesignMetadata> UpdateAsync(SiteDesignMetadata updatedSiteDesignMetadata, CancellationToken cancellationToken);

    Task DeleteAsync();
    Task DeleteAsync(CancellationToken cancellationToken);
  }
}
