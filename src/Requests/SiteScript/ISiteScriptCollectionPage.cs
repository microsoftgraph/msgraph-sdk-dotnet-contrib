using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(InterfaceConverter<SiteScriptCollectionPage>))]
  public interface ISiteScriptCollectionPage : ICollectionPage<SiteScriptMetadata>
  {
  }
}
