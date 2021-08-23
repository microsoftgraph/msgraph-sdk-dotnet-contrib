using Graph.Community.Diagnostics;
using Microsoft.Graph;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
	public class SharePointServiceHandler : DelegatingHandler
	{
		/// <summary>
		/// SharePointServiceHandlerOption property
		/// </summary>
		internal SharePointServiceHandlerOption SharePointServiceHandlerOption { get; set; }

		/// <summary>
		/// Constructs a new <see cref="SharePointServiceHandler"/> 
		/// </summary>
		/// <param name="sharepointServiceHandlerOption">An OPTIONAL <see cref="Microsoft.Graph.SharePointServiceHandlerOption"/> to configure <see cref="SharePointServiceHandler"/></param>
		public SharePointServiceHandler(SharePointServiceHandlerOption sharepointServiceHandlerOption = null)
		{
			SharePointServiceHandlerOption = sharepointServiceHandlerOption ?? new SharePointServiceHandlerOption();
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var disableTelemetry = CommunityGraphClientFactory.TelemetryDisabled;
			string resourceUri = null;

			SharePointServiceHandlerOption = request.GetMiddlewareOption<SharePointServiceHandlerOption>();

			if (SharePointServiceHandlerOption == null)
			{
				// This is not a request to SharePoint
				var segments = request.RequestUri.Segments;

				if (segments?.Length > 2)
				{
					resourceUri = $"{segments[1]}{segments[2]}";
				}
			}
			else
			{
				disableTelemetry = SharePointServiceHandlerOption.DisableTelemetry;
				resourceUri = SharePointServiceHandlerOption.ResourceUri;
			}

			var context = request.GetRequestContext();


			GraphCommunityEventSource.Singleton.SharePointServiceHandlerPreprocess(resourceUri, context);

			var response = await base.SendAsync(request, cancellationToken);

			GraphCommunityEventSource.Singleton.SharePointServiceHandlerPostprocess(resourceUri, context, response.StatusCode);


			if (SharePointServiceHandlerOption != null && !response.IsSuccessStatusCode)
			{
				using (response)
				{
					var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
					GraphCommunityEventSource.Singleton.SharePointServiceHandlerNonsuccess(resourceUri, context, responseContent);

					CommunityGraphTelemetry.LogServiceRequest(resourceUri, context.ClientRequestId, request.Method, response.StatusCode, responseContent);

					var errorResponse = this.ConvertErrorResponseAsync(responseContent);
					Error error = null;

					if (errorResponse == null || errorResponse.Error == null)
					{
						// we couldn't parse the error, so return generic message
						if (response != null && response.StatusCode == HttpStatusCode.NotFound)
						{
							error = new Error { Code = "itemNotFound" };
						}
						else
						{
							error = new Error
							{
								Code = "generalException",
								Message = "Unexpected exception returned from the service."
							};
						}
					}
					else
					{
						error = errorResponse.Error;
					}

					// If the error has a json body, include it in the exception.
					if (response.Content?.Headers.ContentType?.MediaType == "application/json")
					{
						string rawResponseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

						throw new ServiceException(error,
																			 response.Headers,
																			 response.StatusCode,
																			 rawResponseBody);
					}
					else
					{
						// Pass through the response headers and status code to the ServiceException.
						// System.Net.HttpStatusCode does not support RFC 6585, Additional HTTP Status Codes.
						// Throttling status code 429 is in RFC 6586. The status code 429 will be passed through.
						throw new ServiceException(error, response.Headers, response.StatusCode);
					}
				}
			}
			else
			{
				CommunityGraphTelemetry.LogServiceRequest(resourceUri, context.ClientRequestId, request.Method, response.StatusCode, null);
			}

			return response;

		}

		/// <summary>
		/// Converts the <see cref="HttpRequestException"/> into an <see cref="ErrorResponse"/> object;
		/// </summary>
		/// <param name="response">The <see cref="HttpResponseMessage"/> to convert.</param>
		/// <returns>The <see cref="ErrorResponse"/> object.</returns>
		private ErrorResponse ConvertErrorResponseAsync(string responseContent)
		{
			try
			{
				// try our best to provide a helpful message...
				var responseObject = JsonDocument.Parse(responseContent).RootElement;
				var message = responseObject.TryGetProperty("error_description", out var errorDescription)
					? errorDescription.ToString()
					: responseContent;

				var error = new ErrorResponse()
				{
					Error = new Error()
					{
						Code = "SPError",
						Message = message
					}
				};

				return error;

			}
			catch (Exception)
			{
				// If there's an exception deserializing the error response,
				// return null and throw a generic ServiceException later.
				return null;
			}
		}


	}
}
