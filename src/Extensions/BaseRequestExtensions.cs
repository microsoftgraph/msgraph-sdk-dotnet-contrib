using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public static class BaseRequestExtensions
  {
    public static T WithImmutableId<T>(this T baseRequest) where T : IBaseRequest
    {
      baseRequest.Headers.Add(
        new HeaderOption(
          RequestExtensionsConstants.Headers.PreferHeaderName,
          RequestExtensionsConstants.Headers.PreferHeaderImmutableIdValue)
      );
      return baseRequest;
    }
  }
}
