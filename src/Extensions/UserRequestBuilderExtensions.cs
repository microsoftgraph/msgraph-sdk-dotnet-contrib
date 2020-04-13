using Graph.Community;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public static class UserRequestBuilderExtensions
  {
    public static IUserMailboxSettingsRequestBuilder MailboxSettings(this IUserRequestBuilder builder)
    {
      return new UserMailboxSettingsRequestBuilder(builder.AppendSegmentToRequestUrl("mailboxSettings"), builder.Client);
    }
  }
}
