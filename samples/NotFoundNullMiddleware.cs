using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
	//HttpProvider : IHttpProvider, IDisposable
	public class NotFoundNullProvider : IHttpProvider, IDisposable
	{
		private HttpProvider hp;

		public NotFoundNullProvider(ISerializer serializer = null)
		: this((HttpMessageHandler)null, true, serializer)
		{ }

		public NotFoundNullProvider(HttpClientHandler httpClientHandler, bool disposeHandler, ISerializer serializer = null)
		: this((HttpMessageHandler)httpClientHandler, disposeHandler, serializer)
		{ }

		public NotFoundNullProvider(HttpMessageHandler httpMessageHandler, bool disposeHandler, ISerializer serializer)
		{
			hp = new HttpProvider(httpMessageHandler, disposeHandler, serializer);
		}

		public ISerializer Serializer => hp.Serializer;

		public TimeSpan OverallTimeout { get => hp.OverallTimeout; set => hp.OverallTimeout = value; }

		public void Dispose()
		{
			hp.Dispose();
		}

		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
		{
			return this.SendAsync(request, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
		}

		public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
      // this is the magic...

      try
      {
				// await here and swallow the exception...
				return await hp.SendAsync(request, completionOption, cancellationToken);
			}
			catch (Exception ex)
      {
				return null;
			}
		}
	}

	public static class NotFoundNullMiddleware
	{
		public static async Task Run()
		{
			/////////////////
			//
			// Configuration
			//
			/////////////////

			AzureAdOptions azureAdOptions = new AzureAdOptions();

			var settingsFilename = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json");
			var builder = new ConfigurationBuilder()
													.AddJsonFile(settingsFilename, optional: false)
													.AddUserSecrets<Program>();
			var config = builder.Build();
			config.Bind("AzureAd", azureAdOptions);

			////////////////////////////
			//
			// Graph Client with Handler
			//
			////////////////////////////
			var logger = new StringBuilderHttpMessageLogger();

			var pca = PublicClientApplicationBuilder
									.Create(azureAdOptions.ClientId)
									.WithTenantId(azureAdOptions.TenantId)
									.Build();

			var scopes = new string[] { "https://graph.microsoft.com/Mail.Read" };
			IAuthenticationProvider ap = new DeviceCodeProvider(pca, scopes);

			using (LoggingMessageHandler loggingHandler = new LoggingMessageHandler(logger))
			using (NotFoundNullProvider hp = new NotFoundNullProvider(loggingHandler, false, new Serializer()))
			{
				GraphServiceClient graphServiceClient = new GraphServiceClient(ap, hp);


				////////////////////////////
				//
				// Setup is complete, run the sample
				//
				////////////////////////////

				try
				{

					var message =
						await graphServiceClient
										.Users["b548575c-a1d4-4a5e-ab42-09dfe5a03bb0"]
										.Request()
										.GetAsync();

					Console.WriteLine($"ID: {message.Id}");

				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}

				Console.WriteLine();

			}
		}

	}
}
