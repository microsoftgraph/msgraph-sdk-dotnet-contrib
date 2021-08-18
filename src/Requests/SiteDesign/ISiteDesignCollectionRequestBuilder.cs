using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISiteDesignCollectionRequestBuilder : IBaseRequestBuilder
  {
    ISiteDesignCollectionRequest Request();

    /// <summary>
    /// Gets a <see cref="ISiteDesignCollectionRequestBuilder"/> for the specified Site Design.
    /// </summary>
    /// <param name="id">The ID for the Site Design.</param>
    /// <returns>The <see cref="ISiteDesignCollectionRequestBuilder"/>.</returns>
    ISiteDesignRequestBuilder this[string id] { get; }
  }
}
