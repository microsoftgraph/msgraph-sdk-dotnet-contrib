namespace Graph.Community
{
  public interface ITenantRequestBuilder
  {
    IAppCatalogUrlRequestBuilder AppCatalogUrl { get; }
    IStorageEntityCollectionRequestBuilder StorageEntities { get; }
  }
}
