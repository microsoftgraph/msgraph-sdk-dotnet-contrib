using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(InterfaceConverter<SiteDesignRunCollectionPage>))]
  public interface ISiteDesignRunCollectionPage : ICollectionPage<SiteDesignRun>
  {
  }
}
