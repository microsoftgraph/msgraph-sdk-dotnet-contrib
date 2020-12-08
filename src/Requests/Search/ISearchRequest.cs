using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface ISearchRequest : IBaseRequest
  {
    /// <summary>
    /// Executes a simple search query using an Http GET. This method supports only the queryText parameter. For more advanced search queries, use the <see cref="PostQueryAsync(SearchQuery)"/> method.
    /// </summary>
    /// <param name="queryText">The queryText parameter for the search</param>
    /// <returns>The <see cref="SearchResult"/> from SharePoint</returns>
    /// <remarks>This method supports only the queryText parameter. For more advanced search queries, use the <see cref="PostQueryAsync(SearchQuery)"/> method.</remarks>
    Task<Graph.Community.SearchResult> QueryAsync(string queryText);
    Task<Graph.Community.SearchResult> QueryAsync(string queryText, CancellationToken cancellationToken);
    

    Task<Graph.Community.SearchResult> PostQueryAsync(SearchQuery searchQuery);
    Task<Graph.Community.SearchResult> PostQueryAsync(SearchQuery searchQuery, CancellationToken cancellationToken);

  }
}
