using Microsoft.Graph;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface ISiteScriptRequest : IBaseRequest
  {

    Task<ICollectionPage<SiteScriptMetadata>> GetAsync();
    Task<ICollectionPage<SiteScriptMetadata>> GetAsync(CancellationToken cancellationToken);

    //	"/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.CreateSiteScript
    Task<SiteScriptMetadata> CreateAsync(SiteScriptMetadata siteScriptMetadata);
    Task<SiteScriptMetadata> CreateAsync(SiteScriptMetadata siteScriptMetadata, CancellationToken cancellationToken);

  }
}
