using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class UserMailboxSettingsRequestBuilder : BaseRequestBuilder, IUserMailboxSettingsRequestBuilder
  {
    private IEnumerable<Option> options;

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
