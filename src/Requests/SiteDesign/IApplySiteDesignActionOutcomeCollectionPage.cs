using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(InterfaceConverter<ApplySiteDesignActionOutcomeCollectionPage>))]
  public interface IApplySiteDesignActionOutcomeCollectionPage : ICollectionPage<SiteScriptActionResult>
  {
  }
}
