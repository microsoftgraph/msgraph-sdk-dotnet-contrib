using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public static class CommunityGraphConstants
	{
		public static class Library
		{
			/// The key for the SDK version header.
			internal static readonly string VersionHeaderName = CommunityGraphConstants.Headers.CommunityLibraryVersionHeaderName;

			/// The version for current assembly.
			internal static string AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(CommunityGraphConstants).Assembly.Location).FileVersion;

			/// The value for the SDK version header.
			internal static string VersionHeaderValue = $"dotnet-{AssemblyVersion}";
		}

		public static class Headers
		{
			/// Library Version header
			public const string CommunityLibraryVersionHeaderName = "CommunityLibraryVersion";

			/// Library Version header
			public const string CommunityLibraryVersionHeaderValueFormatString = "dotnet-{0}.{1}.{2}";
		}

		public static class TelemetryProperties
		{
			public const string ResourceUri = nameof(ResourceUri);
			public const string RequestMethod = nameof(RequestMethod);
			public const string ClientRequestId = nameof(ClientRequestId);
			public const string ResponseStatus = nameof(ResponseStatus);
			public const string RawErrorResponse = nameof(RawErrorResponse);
			public const string AuthenticationProvider = nameof(AuthenticationProvider);
			public const string LoggingHandler = nameof(LoggingHandler);

		}
	}
}
