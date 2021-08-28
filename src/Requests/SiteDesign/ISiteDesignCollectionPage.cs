using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(InterfaceConverter<SiteDesignCollectionPage>))]
  public interface ISiteDesignCollectionPage : ICollectionPage<SiteDesignMetadata>
  {
  }
}
