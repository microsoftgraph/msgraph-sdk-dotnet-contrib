using System.Net.Http.Headers;

namespace Graph.Community
{
  public class CommunityGraphClientOptions
  {
    /// <summary>
    /// Set to true to disable telemetry
    /// </summary>
    public bool DisableTelemetry { get; set; }
    /// <summary>
    /// The UserAgent to set on requests. Overridden by <see cref="UserAgentInfo"/>
    /// </summary>
    public string UserAgent { get; set; }
    /// <summary>
    /// The UserAgentInfo for decorating SharePoint traffice. Overrides <see cref="UserAgent"/>
    /// </summary>
    public SharePointThrottlingDecoration UserAgentInfo { get; set; }

    public CommunityGraphClientOptions() { }
    public CommunityGraphClientOptions(string companyName, string appName, string appVersion, bool isv)
    {
      this.UserAgentInfo = new SharePointThrottlingDecoration()
      {
        CompanyName = companyName,
        AppName = appName,
        AppVersion = appVersion,
        ISV = isv
      };
    }
  }

  public struct SharePointThrottlingDecoration
  {
    public string CompanyName { get; set; }
    public string AppName { get; set; }
    public string AppVersion { get; set; }
    public bool ISV { get; set; }

    public bool IsEmpty()
    {
      return string.IsNullOrEmpty(CompanyName) &&
             string.IsNullOrEmpty(AppName) &&
             string.IsNullOrEmpty(AppName) &&
             !ISV;
    }

    public ProductInfoHeaderValue ToUserAgent()
    {
      var isvDecoration = ISV ? "ISV" : "NONISV";
      var product = $"{isvDecoration}|{CompanyName}|{AppName}";
      return new ProductInfoHeaderValue(product, AppVersion);
    }
  }

}
