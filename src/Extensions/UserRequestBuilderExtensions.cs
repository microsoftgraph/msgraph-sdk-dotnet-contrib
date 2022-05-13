using System;
using Microsoft.Graph;

namespace Graph.Community
{
  public static class UserRequestBuilderExtensions
  {
    public static IUserMailboxSettingsRequestBuilder MailboxSettings(this IUserRequestBuilder builder)
    {
      if (builder is null)
      {
        throw new ArgumentNullException(nameof(builder));
      }

      return new UserMailboxSettingsRequestBuilder(builder.AppendSegmentToRequestUrl("mailboxSettings"), builder.Client);
    }
  }
}
