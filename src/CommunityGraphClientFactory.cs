using Azure.Core;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("Graph.Community.Test")]

namespace Graph.Community
{
  /// <summary>
  /// CommunityGraphClientFactory class to create the HTTP client configured to support Community-created requests 
  /// </summary>
  public static class CommunityGraphClientFactory
	{
		private static readonly object telemetryFlagLock = new object();
		internal static bool telemetryDisabled;
		internal static bool TelemetryDisabled
		{
			get
			{
				lock (telemetryFlagLock)
				{
					return telemetryDisabled;
				}
			}
			set
			{
				lock (telemetryFlagLock)
				{
					telemetryDisabled = value;
				}
			}
		}


		private static SharePointThrottlingDecoration defaultDecoration = new SharePointThrottlingDecoration()
		{
			CompanyName = "GraphCommunity",
			AppName = "CommunityGraphClient",
			AppVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(CommunityGraphClientFactory).Assembly.Location).FileVersion
		};

		/// <summary>
		/// Creates a new <see cref="GraphServiceClient"/> instance configured with to use the provided <see cref="TokenCredential"/>.
		/// </summary>
		/// <param name="options">The <see cref="CommunityGraphClientOptions"/> to use.</param>
		/// <param name="tokenCredential">The <see cref="TokenCredential"/> to use for acquiring tokens.</param>
		/// <param name="version">The graph version to use.</param>
		/// <param name="nationalCloud">The national cloud endpoint to use.</param>
		/// <param name="proxy">The proxy to be used with created client.</param>
		/// <param name="finalHandler">The last HttpMessageHandler to HTTP calls.
		/// <returns>A GraphServiceClient instance with the SharePoint handler configured.</returns>
		public static GraphServiceClient Create(CommunityGraphClientOptions options, TokenCredential tokenCredential, string version = "v1.0", string nationalCloud = "Global", IWebProxy proxy = null, HttpMessageHandler finalHandler = null)
		{
			LoggingOptions logOptions = (options.DisableTelemetry != true)
														? new Community.LoggingOptions() { TokenCredential = tokenCredential.GetType().FullName }
														: null;

			var handlers = GraphClientFactory.CreateDefaultHandlers(new TokenCredentialAuthProvider(tokenCredential));
			return Create(logOptions, options, handlers, version, nationalCloud, proxy, finalHandler);
		}

		/// <summary>
		/// Creates a new <see cref="GraphServiceClient"/> instance configured to use the provided <see cref="IAuthenticationProvider"/>.
		/// </summary>
		/// <param name="options">The <see cref="CommunityGraphClientOptions"/> to use.</param>
		/// <param name="authenticationProvider">The <see cref="IAuthenticationProvider"/> to authenticate requests.</param>
		/// <param name="version">The graph version to use.</param>
		/// <param name="nationalCloud">The national cloud endpoint to use.</param>
		/// <param name="proxy">The proxy to be used with created client.</param>
		/// <param name="finalHandler">The last HttpMessageHandler to HTTP calls.
		/// <returns>A GraphServiceClient instance with the SharePoint handler configured.</returns>
		public static GraphServiceClient Create(CommunityGraphClientOptions options, IAuthenticationProvider authenticationProvider, string version = "v1.0", string nationalCloud = "Global", IWebProxy proxy = null, HttpMessageHandler finalHandler = null)
		{
			LoggingOptions logOptions = (options.DisableTelemetry != true)
														? new Community.LoggingOptions() { AuthenticationProvider = authenticationProvider.GetType().FullName }
														: null;

			return Create(logOptions, options, GraphClientFactory.CreateDefaultHandlers(authenticationProvider), version, nationalCloud, proxy, finalHandler);
		}

		/// <summary>
		/// Creates a new <see cref="GraphServiceClient"/> instance configured to use the <see cref="LoggingMessageHandler"/>.
		/// </summary>
		/// <param name="options">The <see cref="CommunityGraphClientOptions"/> to use.</param>
		/// <param name="messageLogger">An <see cref="IHttpMessageLogger"/> instance to insert into the Http pipeline.</param>
		/// <param name="authenticationProvider">The <see cref="IAuthenticationProvider"/> to authenticate requests.</param>
		/// <param name="version">The graph version to use.</param>
		/// <param name="nationalCloud">The national cloud endpoint to use.</param>
		/// <param name="proxy">The proxy to be used with created client.</param>
		/// <param name="finalHandler">The last HttpMessageHandler to HTTP calls.
		/// <returns>A GraphServiceClient instance with the SharePoint handler configured.</returns>
		public static GraphServiceClient Create(CommunityGraphClientOptions options, IHttpMessageLogger messageLogger, IAuthenticationProvider authenticationProvider, string version = "v1.0", string nationalCloud = "Global", IWebProxy proxy = null, HttpMessageHandler finalHandler = null)
		{
			LoggingOptions logOptions = (options.DisableTelemetry != true)
											? new Community.LoggingOptions() { AuthenticationProvider = authenticationProvider.GetType().FullName, LoggingHandler = true }
											: null;

			var handlers = AddLoggingHandlerToGraphDefaults(messageLogger, authenticationProvider);

			return Create(logOptions, options, handlers, version, nationalCloud, proxy, finalHandler);
		}

		/// <summary>
		/// Creates a new <see cref="GraphServiceClient"/> instance configured to use the <see cref="LoggingMessageHandler"/>.
		/// </summary>
		/// <param name="options">The <see cref="CommunityGraphClientOptions"/> to use.</param>
		/// <param name="messageLogger">An <see cref="IHttpMessageLogger"/> instance to insert into the Http pipeline.</param>
		/// <param name="tokenCredential">The <see cref="TokenCredential"/> to use for acquiring tokens.</param>
		/// <param name="version">The graph version to use.</param>
		/// <param name="nationalCloud">The national cloud endpoint to use.</param>
		/// <param name="proxy">The proxy to be used with created client.</param>
		/// <param name="finalHandler">The last HttpMessageHandler to HTTP calls.
		/// <returns>A GraphServiceClient instance with the SharePoint handler configured.</returns>
		public static GraphServiceClient Create(CommunityGraphClientOptions options, IHttpMessageLogger messageLogger, TokenCredential tokenCredential, string version = "v1.0", string nationalCloud = "Global", IWebProxy proxy = null, HttpMessageHandler finalHandler = null)
    {
			LoggingOptions logOptions = (options.DisableTelemetry != true)
											? new Community.LoggingOptions() { TokenCredential = tokenCredential.GetType().FullName, LoggingHandler = true }
											: null;

			var handlers = AddLoggingHandlerToGraphDefaults(messageLogger, new TokenCredentialAuthProvider(tokenCredential));

			return Create(logOptions, options, handlers, version, nationalCloud, proxy, finalHandler);
		}

		/// <summary>
		/// Creates a new System.Net.Http.HttpClient instance configured with the Graph.Community middleware plus the handlers provided.
		/// </summary>
		/// <param name="options">The <see cref="CommunityGraphClientOptions"/> to use.</param>
		/// <param name="handlers">An ordered list of System.Net.Http.DelegatingHandler instances to be invoked</param>
		/// <param name="version">The graph version to use.</param>
		/// <param name="nationalCloud">The national cloud endpoint to use.</param>
		/// <param name="proxy">The proxy to be used with created client.</param>
		/// <param name="finalHandler">The last HttpMessageHandler to HTTP calls.</param>
		/// <returns>A GraphServiceClient instance with the configured handlers.</returns>
		private static GraphServiceClient Create(LoggingOptions logOptions, CommunityGraphClientOptions options, IList<DelegatingHandler> handlers, string version = "v1.0", string nationalCloud = "Global", IWebProxy proxy = null, HttpMessageHandler finalHandler = null)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}

      if (logOptions !=null)
      {
				CommunityGraphTelemetry.LogFactoryMethod(logOptions);
			}

			ProductInfoHeaderValue defaultUserAgent = defaultDecoration.ToUserAgent();
			ProductInfoHeaderValue specifiedUserAgent = default;

			if (!options.UserAgentInfo.IsEmpty())
			{
				specifiedUserAgent = options.UserAgentInfo.ToUserAgent();
			}
			else
			{
				// if we got a user agent string, validate it
				if (!string.IsNullOrEmpty(options.UserAgent))
				{
					if (!ProductInfoHeaderValue.TryParse(options.UserAgent, out specifiedUserAgent))
					{
						throw new ArgumentOutOfRangeException("CommunityGraphClientOptions.UserAgent", "Cannot parse UserAgent string");
					}
				}
			}


			handlers.Insert(0, new SharePointServiceHandler());

			var httpClient = GraphClientFactory.Create(handlers, version, nationalCloud, proxy, finalHandler);

			if (specifiedUserAgent != null)
			{
				httpClient.DefaultRequestHeaders.UserAgent.Add(specifiedUserAgent);
			}

			// if the provided string does not have the SharePoint throttling decoration, add the library user agent to the default.
			//     https://docs.microsoft.com/en-us/sharepoint/dev/general-development/how-to-avoid-getting-throttled-or-blocked-in-sharepoint-online#how-to-decorate-your-http-traffic-to-avoid-throttling
			if (!specifiedUserAgent.ToString().Contains("ISV"))
			{
				httpClient.DefaultRequestHeaders.UserAgent.Add(defaultUserAgent);
			}

			httpClient.DefaultRequestHeaders.Add(CommunityGraphConstants.Library.VersionHeaderName, CommunityGraphConstants.Library.VersionHeaderValue);


			var graphServiceClient = new GraphServiceClient(httpClient);

			return graphServiceClient;
		}

		private static IList<DelegatingHandler> AddLoggingHandlerToGraphDefaults(IHttpMessageLogger messageLogger, IAuthenticationProvider authenticationProvider)
    {
			LoggingMessageHandler loggingHandler = new LoggingMessageHandler(messageLogger);

			var handlers = GraphClientFactory.CreateDefaultHandlers(authenticationProvider);

			var compressionHandlerIndex = handlers.ToList().FindIndex(h => h is CompressionHandler);
			if (compressionHandlerIndex > -1)
			{
				handlers.Insert(compressionHandlerIndex, loggingHandler);
			}
			else
			{
				handlers.Add(loggingHandler);
			}

			return handlers;
		}

	}

	internal class LoggingOptions
	{
		public string TokenCredential { get; set; }
		public string AuthenticationProvider { get; set; }
		public bool LoggingHandler { get; set; }
		public LoggingOptions() { }
	}

}
