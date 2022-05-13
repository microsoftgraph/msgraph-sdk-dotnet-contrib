using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class UserMailboxSettingsRequestBuilder : BaseRequestBuilder, IUserMailboxSettingsRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public UserMailboxSettingsRequestBuilder(
        string requestUrl,
        IBaseClient client,
        IEnumerable<Option> options = null)
        : base(requestUrl, client)
    {
      this.options = options;
    }

    public IUserMailboxSettingsRequest Request()
    {
      return this.Request(this.options);
    }

    public IUserMailboxSettingsRequest Request(IEnumerable<Option> options)
    {
      return new UserMailboxSettingsRequest(this.RequestUrl, this.Client, options);
    }

  }
}
