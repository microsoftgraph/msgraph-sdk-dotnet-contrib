using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public static class RequestExtensionsConstants
  {
#pragma warning disable CA1034 // Nested types should not be visible
#pragma warning disable CA1724 // Type names should not match namespaces

    public static class Headers
    {
      public const string PreferHeaderName = "Prefer";
      public const string PreferHeaderImmutableIdValue = "IdType=\"ImmutableId\"";
    }
  }
}
