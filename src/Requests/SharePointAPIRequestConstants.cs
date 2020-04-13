using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public static class SharePointAPIRequestConstants
  {
#pragma warning disable CA1034 // Nested types should not be visible
#pragma warning disable CA1724 // Type names should not match namespaces

    public static class Headers
    {
      public const string AcceptHeaderName = "Accept";
      public const string AcceptHeaderValue = "application/json; odata.metadata=minimal";

      public const string ODataVersionHeaderName = "ODATA-VERSION";
      public const string ODataVersionHeaderValue = "4.0";

      public const string XHTTPMethodHeaderName = "X-HTTP-Method";
      public const string XHTTPMethodHeaderMergeValue = "MERGE";
      public const string XHTTPMethodHeaderPutValue = "PUT";
    }
  }
#pragma warning restore CA1724 // Type names should not match namespaces
#pragma warning restore CA1034 // Nested types should not be visible
}
