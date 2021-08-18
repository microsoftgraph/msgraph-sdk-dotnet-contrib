namespace Graph.Community
{
  public static class SharePointAPIRequestConstants
  {
    public static class Headers
    {
      public const string AcceptHeaderName = "Accept";
      public const string AcceptHeaderValue = "application/json; odata.metadata=minimal";

      public const string SearchContentTypeHeaderValue = "application/json; odata=verbose";
      public const string SearchAcceptHeaderValue = "application/json; odata=nometadata";

      public const string ODataVersionHeaderName = "ODATA-VERSION";
      public const string ODataVersionHeaderValue = "4.0";

      public const string XHTTPMethodHeaderName = "X-HTTP-Method";
      public const string XHTTPMethodHeaderMergeValue = "MERGE";
      public const string XHTTPMethodHeaderPutValue = "PUT";
    }
  }
}
