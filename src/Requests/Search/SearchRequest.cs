using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class SearchRequest : BaseSharePointAPIRequest, ISearchRequest
  {
    public SearchRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("Search", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.SearchAcceptHeaderValue));
    }

    public async Task<SearchResult> PostQueryAsync(SearchQuery searchQuery)
    {
      return await PostQueryAsync(searchQuery, CancellationToken.None);
    }

    public async Task<SearchResult> PostQueryAsync(SearchQuery searchQuery, CancellationToken cancellationToken)
    {
      if (searchQuery == null)
      {
        throw new ArgumentNullException(nameof(searchQuery));
      }

      this.AppendSegmentToRequestUrl("postquery");
      this.Method = HttpMethods.POST;
      this.ContentType = SharePointAPIRequestConstants.Headers.SearchContentTypeHeaderValue;

      var response = await this.SendAsync<SearchResult>(searchQuery, cancellationToken).ConfigureAwait(false);
      return response;
    }

    public async Task<SearchResult> QueryAsync(string queryText)
    {
      return await QueryAsync(queryText, CancellationToken.None);
    }

    public async Task<SearchResult> QueryAsync(string queryText, CancellationToken cancellationToken)
    {
      if (string.IsNullOrEmpty(queryText))
      {
        throw new ArgumentNullException(nameof(queryText));
      }

      this.AppendSegmentToRequestUrl("query");
      this.Method = HttpMethods.GET;

      this.QueryOptions.Add(new QueryOption("queryText", queryText));
      var response = await this.SendAsync<SearchResult>(null, cancellationToken).ConfigureAwait(false);
      return response;
    }


    private async Task<HttpResponseMessage> SendSearchRequest(
      object serializableObject,
      CancellationToken cancellationToken,
      HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
    {
      if (string.IsNullOrEmpty(this.RequestUrl))
      {
        throw new ServiceException(
          new Error
          {
            Code = "invalidRequest", //ErrorConstants.Codes.InvalidRequest,
            Message = "Request URL is required to send a request." //ErrorConstants.Messages.RequestUrlMissing,
          });
      }

      using var request = this.GetHttpRequestMessage(cancellationToken);

      // We are going to assume the Client has an Auth Provider/Handler setup
      await this.Client.AuthenticationProvider.AuthenticateRequestAsync(request);

      if (serializableObject != null)
      {
        if (serializableObject is System.IO.Stream inputStream)
        {
          request.Content = new StreamContent(inputStream);
        }
        else
        {
          request.Content = new StringContent(this.Client.HttpProvider.Serializer.SerializeObject(serializableObject));
        }

        if (!string.IsNullOrEmpty(this.ContentType))
        {
          request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(this.ContentType);
        }
      }

      return await this.Client.HttpProvider.SendAsync(request, completionOption, cancellationToken).ConfigureAwait(false);
    }
  }
}
