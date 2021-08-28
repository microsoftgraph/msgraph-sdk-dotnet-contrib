using Microsoft.Graph;

namespace Graph.Community
{
  public class SharePointServiceHandlerOption : IMiddlewareOption
  {
    public bool DisableTelemetry { get; set; }
    public string ResourceUri { get; set; }
  }
}
