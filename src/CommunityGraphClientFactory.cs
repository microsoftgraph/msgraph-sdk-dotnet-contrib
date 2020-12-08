using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

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


		private static SharePointThrottlingDecoration defaultDecoration = new SharePointThrottlingDecoration
		{
			CompanyName = "GraphCommunity",
			AppName = "CommunityGraphClient",
			AppVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(CommunityGraphClientFactory).Assembly.Location).ProductVersion
		};

		/// <summary>
		/// Creates a new <see cref="HttpClient"/> instance configured with the handlers provided.
		/// </summary>
		/// <param name="options">The <see cref="CommunityGraphClientOptions"/> to use.</param>
		/// <param name="authenticationProvider">The <see cref="IAuthenticationProvider"/> to authenticate requests.</param>
		/// <param name="version">The graph version to use.</param>
		/// <param name="nationalCloud">The national cloud endpoint to use.</param>
		/// <param name="proxy">The proxy to be used with created client.</param>
		/// <param name="finalHandler">The last HttpMessageHandler to HTTP calls.
		/// The default implementation creates a new instance of <see cref="HttpClientHandler"/> for each HttpClient.</param>
		/// <returns>A GraphServiceClient instance with the SharePoint handler configured.</returns>
		public static GraphServiceClient Create(CommunityGraphClientOptions options, IAuthenticationProvider authenticationProvider, string version = "v1.0", string nationalCloud = "Global", IWebProxy proxy = null, HttpMessageHandler finalHandler = null)
		{
			if (options.DisableTelemetry != true)
			{
				if (options.DisableTelemetry != true)
				{
					LogFactoryMethod(authenticationProvider.GetType().Name, false);
				}
			}

			return Create(options, GraphClientFactory.CreateDefaultHandlers(authenticationProvider), version, nationalCloud, proxy, finalHandler);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="options">The <see cref="CommunityGraphClientOptions"/> to use.</param>
		/// <param name="messageLogger">An <see cref="IHttpMessageLogger"/> instance to insert into the Http pipeline.</param>
		/// <param name="authenticationProvider">The <see cref="IAuthenticationProvider"/> to authenticate requests.</param>
		/// <param name="version">The graph version to use.</param>
		/// <param name="nationalCloud">The national cloud endpoint to use.</param>
		/// <param name="proxy">The proxy to be used with created client.</param>
		/// <param name="finalHandler">The last HttpMessageHandler to HTTP calls.
		/// The default implementation creates a new instance of <see cref="HttpClientHandler"/> for each HttpClient.</param>
		/// <returns>A GraphServiceClient instance with the SharePoint handler configured.</returns>
		public static GraphServiceClient Create(CommunityGraphClientOptions options, IHttpMessageLogger messageLogger, IAuthenticationProvider authenticationProvider, string version = "v1.0", string nationalCloud = "Global", IWebProxy proxy = null, HttpMessageHandler finalHandler = null)
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

			if (options.DisableTelemetry != true)
			{
				LogFactoryMethod(authenticationProvider.GetType().Name, true);
			}

			return Create(options, handlers, version, nationalCloud, proxy, finalHandler);
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
		public static GraphServiceClient Create(CommunityGraphClientOptions options, IList<DelegatingHandler> handlers, string version = "v1.0", string nationalCloud = "Global", IWebProxy proxy = null, HttpMessageHandler finalHandler = null)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
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

      if (specifiedUserAgent !=null)
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

		private static void LogFactoryMethod(string authenticationProvider, bool loggingHandler)
    {
			var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
			telemetryConfiguration.InstrumentationKey = "d882bd7a-a378-4117-bd7c-71fc95a44cd1";
			var telemetryClient = new TelemetryClient(telemetryConfiguration);

			Dictionary<string, string> properties = new Dictionary<string, string>(10)
				{
					{ CommunityGraphConstants.Headers.CommunityLibraryVersionHeaderName, CommunityGraphConstants.Library.AssemblyVersion },
					{ CommunityGraphConstants.TelemetryProperties.AuthenticationProvider, authenticationProvider },
					{ CommunityGraphConstants.TelemetryProperties.LoggingHandler, loggingHandler.ToString() }
				};
			telemetryClient.TrackEvent("CommunityGraphClientFactory", properties);
			telemetryClient.Flush();

		}
	}
}
